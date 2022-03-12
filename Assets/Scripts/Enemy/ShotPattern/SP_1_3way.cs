using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_1_3way : MonoBehaviour
{
    private int _count = 0;

    private ShotGenerator SG;
    void Start()
    {
        SG = GameObject.Find("ShotManager").GetComponent<ShotGenerator>();
        StartCoroutine(wait());
    }

    private IEnumerator wait()
    {
        for (int i = 0; i < 120; i++)
        {
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if (_count % 30 == 0)
        {
            SG.Radiation(
                "shot_1",
                1.0f,
                this.transform.position,
                3,
                2.0f,
                30.0f,
                -1.0f
            );
        }
        _count++;
        _count %= 30;
    }
}
