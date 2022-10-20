using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float platNum;

    [SerializeField]
    private Sprite[] m_colors;

    [SerializeField]
    private GameObject[] Snowmen;

    void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (sr != null)
        {
            int ColorsIndex = Random.Range(0, m_colors.Length);
            sr.sprite = m_colors[ColorsIndex];
            int chancesOfSnowman = Random.Range(0, 4);

            if (chancesOfSnowman == 1)
            {
                int SnowmenIndex = Random.Range(0, Snowmen.Length);
                SnowmenBehaviour(Snowmen[SnowmenIndex]);
            }

        }
    }

    private void SnowmenBehaviour(GameObject snowman)
    {
        float newSnowmanX = Random.Range(-1.0f, 1.0f);

        GameObject newSnowman = Instantiate(snowman);
        newSnowman.transform.SetParent(transform);

        float platformPosition = transform.position.y + 0.6f;
        Vector3 newSnowmanPosition = new Vector3(transform.position.x + newSnowmanX, platformPosition, 0);
        newSnowman.transform.position = newSnowmanPosition;
    }
}
