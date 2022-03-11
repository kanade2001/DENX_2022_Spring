using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{

    private ObjectPool OP;

    void Start()
    {
        OP = GameObject.Find("ShotManager").GetComponent<ObjectPool>();
    }
    void FixedUpdate()
    {
        
    }
}