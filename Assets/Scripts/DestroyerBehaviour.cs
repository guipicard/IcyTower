using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyerBehaviour : MonoBehaviour
{
    private CameraBehaviour cameraSpeed;
    private Player p;
    private UiControl Ui;
    private GameObject Hs;

    private void Start()
    {
        cameraSpeed = FindObjectOfType<CameraBehaviour>();
        p = FindObjectOfType<Player>();
        Ui = FindObjectOfType<UiControl>();
        Hs = GameObject.Find("HighScore1");
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cameraSpeed.m_Speed = 0;
            if (p.highestPlatform > PlayerPrefs.GetFloat("HighScore", 0))
            {
                PlayerPrefs.SetFloat("HighScore", p.highestPlatform);
            }
            SceneManager.LoadScene("MainMenu");
        }
        Destroy(collision.collider.gameObject);
    }
}
