using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject platform;

    void Start()
    {
        float thisHeight = transform.position.y;
        for (int i = 0; i < 8; i++)
        {
            GameObject newPlatform = Instantiate(platform);
            float platformY = thisHeight + ((i + 1) * 2);
            Vector3 platformPosition = new Vector3(Random.Range(-3, 3), platformY, 0);
            newPlatform.transform.position = platformPosition;
            newPlatform.transform.SetParent(transform);
            newPlatform.GetComponent<Platform>().platNum = platformY /2;
        }
    }
}
