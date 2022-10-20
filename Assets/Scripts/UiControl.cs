using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiControl : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private GameObject Height;
    private GameObject Score;
    private GameObject Level;
    private Player p;

    private float lvl = 1.0f;

    void Start()
    {
        Height = GameObject.Find("HeightTxt");
        Score = GameObject.Find("ScoreTxt");
        Level = GameObject.Find("LevelTxt");
        p = FindObjectOfType<Player>();
    }

    void Update()
    {
        if (p.highestPlatform % 50.0f == 0)
        {
            lvl = (p.highestPlatform / 50.0f) + 1.0f;
        }
        Height.GetComponent<Text>().text = "Height:\n" + p.highestPlatform;
        Score.GetComponent<Text>().text = "Score:\n" + p.Score;
        Level.GetComponent<Text>().text = "Level:\n" + lvl; 
    }

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void CameraDistance(float camZ)
    {
        Vector3 camDistance = cam.transform.position;
        camDistance.z = camZ;
        cam.transform.position = camDistance;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
