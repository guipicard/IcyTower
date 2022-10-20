using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float obstacleRayDistance;

    public GameObject PlayerRay;

    public float highestPlatform = 0.0f;

    public int Score = 0;

    private Animator m_Anim;
    private Rigidbody2D m_rigidbody2D;
    private SpriteRenderer m_SpriteRenderer;

    private float m_Direction;
    private bool m_OnGround;
    private bool m_IsMoving;
    private bool m_CanMove;
    private Vector2 boundaries = new Vector2(-4.33f, 4.33f);

    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private float m_jumpForce;

    private float rayKillTimer;

    void Start()
    {
        rayKillTimer = 0;
        m_Direction = 1.0f;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_Anim = GetComponent<Animator>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_CanMove = true;
    }

    void Update()
    {


        Attack();
        MovePlayer();
        Jump();
    }


    private void MovePlayer()
    {
        if (m_CanMove)
        {
            float playerHorizontalInput = Input.GetAxis("Horizontal");
            Vector2 calculatedVelocity = m_rigidbody2D.velocity;
            calculatedVelocity.x = playerHorizontalInput * m_Speed;
            m_rigidbody2D.velocity = calculatedVelocity;
        }

        if (m_rigidbody2D.velocity.x != 0)
        {
            m_IsMoving = true;
        }
        else
        {
            m_IsMoving = false;
        }

        if (m_IsMoving)
        {
            FlipPlayer();
            m_Anim.SetBool("Move", true);
        }
        else
        {
            m_Anim.SetBool("Move", false);
        }
        CheckBoudaries();
    }
    private void FlipPlayer()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(m_rigidbody2D.velocity.x);
        m_Direction = Mathf.Sign(m_rigidbody2D.velocity.x);
        transform.localScale = scale;
    }

    private void CheckBoudaries()
    {
        if (transform.position.x < boundaries.x)
        {
            transform.position = new Vector2(boundaries.x, transform.position.y);
        }
        else if (transform.position.x > boundaries.y)
        {
            transform.position = new Vector2(boundaries.y, transform.position.y);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_OnGround)
        {
            m_rigidbody2D.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            m_OnGround = false;
            m_Anim.SetBool("Jump", true);
        }
        JumpThrewPlatforms();
    }

    private void JumpThrewPlatforms()
    {
        if (m_rigidbody2D.velocity.y > 0)
        {
            if (!m_CanMove)
            {
                gameObject.layer = 10;
            }
            else
            {
                gameObject.layer = 6;
            }
        }
        else if (!m_CanMove)
        {
            gameObject.layer = 9;
        }
        else
        {
            gameObject.layer = 0;
        }
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.RightShift))
        {
            RaycastHit2D hitObstacle = Physics2D.Raycast(transform.position, Vector2.right * new Vector2(m_Direction, 0.0f), obstacleRayDistance);
            if (hitObstacle.collider != null)
            {
                if (hitObstacle.collider.CompareTag("Snowman"))
                {
                    rayKillTimer += Time.deltaTime;
                    if (rayKillTimer >= 0.5f)
                    {
                        Destroy(hitObstacle.collider.gameObject);
                        Score++;
                        if (rayKillTimer >= 1.0f)
                        {
                            rayKillTimer = 0;
                        }
                    }

                }
                Debug.DrawRay(transform.position, Vector2.right * hitObstacle.distance * new Vector2(m_Direction, 0.0f), Color.red);
            }
            m_CanMove = false;
            m_Anim.SetBool("Fire", true);
            m_SpriteRenderer.size = new Vector2(3, 1);
        }
        else
        {
            rayKillTimer = 0;
            m_Anim.SetBool("Fire", false);
            m_SpriteRenderer.size = new Vector2(1, 1);
            m_CanMove = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_Anim.SetBool("Jump", false);
        m_OnGround = true;
        if (collision.gameObject.layer == 3)
        {
            float colPlatNum = collision.GetComponent<Platform>().platNum;
            if (colPlatNum > highestPlatform)
            {
                highestPlatform = colPlatNum;
            }
        }
    }

}
