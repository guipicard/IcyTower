using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{
    private Player p;
    private Transform m_Target;
    float lastLvlIncrement;
    private float m_Speed = 1.0f;


    [SerializeField]
    private Transform[] repeats;

    private int repeatIndex;


    private void Start()
    {
        lastLvlIncrement = 0;
        m_Speed = 1.0f + (1.0f * (lastLvlIncrement / 10));
        repeatIndex = 1;
        p = FindObjectOfType<Player>();
        if (p != null)
        {
            m_Target = p.transform;
        }
    }


    private void Update()
    {
        CameraFollow();
        BackGroundRepeat();
        EndGame();
    }

    private void CameraFollow()
    {
        transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
        // increase speed each 50 platforms
        if (p.highestPlatform % 50 == 0 && p.highestPlatform >= 50)
        {
            lastLvlIncrement = p.highestPlatform;
            m_Speed = m_Speed + (0.8f * (lastLvlIncrement / 50));
        }

        Vector3 position = m_Target.position;
        if (position.y - transform.position.y > 4.0f)
        {
            position.x = transform.position.x;
            position.z = transform.position.z;
            if (transform.position.y != position.y)
            {
                m_Speed = 5.0f + (0.8f * (lastLvlIncrement / 50));
            }
        }
        else
        {
            m_Speed = 1.0f + (0.8f * (lastLvlIncrement / 50));
        }
    }

    private void BackGroundRepeat()
    {
        if (transform.position.y > repeats[repeatIndex].position.y)
        {
            repeatIndex--;
            if (repeatIndex == -1)
            {
                repeatIndex = 1;
            }
            Vector3 lowerBgNewPosition = new Vector3(0, repeats[repeatIndex].position.y + 43.0f, 1);
            repeats[repeatIndex].position = lowerBgNewPosition;
        }
    }

    private void EndGame()
    {
        if (transform.position.y - m_Target.position.y > 7.0f)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
