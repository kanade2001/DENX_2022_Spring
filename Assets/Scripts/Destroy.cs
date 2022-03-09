using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Destroy : MonoBehaviour
{
    void FixedUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if(System.Math.Abs(x)>7 || System.Math.Abs(y)>7)
        {
            Destroy(gameObject);
        }
    }
}