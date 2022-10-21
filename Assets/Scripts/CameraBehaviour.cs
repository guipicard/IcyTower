using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_SpeedIncrements;
    [SerializeField]
    private int m_LvlIncrement;
    [SerializeField]
    private float m_Speed;

    private Player p;
    private Transform m_Target;
    float lastLvlIncrement;
    


    [SerializeField]
    private Transform[] repeats;

    private int repeatIndex;


    private void Start()
    {
        lastLvlIncrement = 0;
        m_Speed = 1.0f + (m_Speed * (lastLvlIncrement / m_LvlIncrement));
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
        // increase speed each 10 platforms
        if (p.highestPlatform % 10 == 0 && p.highestPlatform >= 10)
        {
            lastLvlIncrement = p.highestPlatform;
            m_Speed += m_SpeedIncrements;
        }

        Vector3 position = m_Target.position;
        if (position.y - transform.position.y > 4.0f)
        {
            if (transform.position.y != position.y)
            {
                m_Speed = 5.0f + (m_SpeedIncrements * (lastLvlIncrement / m_LvlIncrement));
            }
        }
        else
        {
            m_Speed = 1.0f + (m_SpeedIncrements * (lastLvlIncrement / m_LvlIncrement));
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
