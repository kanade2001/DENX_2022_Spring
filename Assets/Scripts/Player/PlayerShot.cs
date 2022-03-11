using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{


    void FixedUpdate()
    {
        this.transform.Translate(0,0.1f,0); //直線移動
    }
}