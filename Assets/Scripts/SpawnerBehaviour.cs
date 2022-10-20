using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject Walls;
    private float m_spawnHeight = 16.0f;

    void Update()
    {
        if (transform.position.y >= m_spawnHeight)
        {
            GameObject newWalls = Instantiate(Walls);
            newWalls.transform.position = new Vector3(0, m_spawnHeight, 0);
            m_spawnHeight += 16.0f;
        }
    }
}
