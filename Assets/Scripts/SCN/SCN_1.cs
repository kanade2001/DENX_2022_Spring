using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCN_1 : MonoBehaviour
{
    public GameObject enemy_1_0;
    void Start()
    {
        StartCoroutine(wave1());
    }

    private IEnumerator wave1()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 29; j++)
            {
                yield return null;
            }
            Instantiate(enemy_1_0, new Vector3(-4.2f + 1.4f * (float)i, 6.0f, 0.0f), Quaternion.identity);
        }
    }
}
