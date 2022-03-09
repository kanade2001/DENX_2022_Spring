using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sample : MonoBehaviour
{
    private ObjectPool script_pool;
    private Bullets script_shoot;
    private int framecount;

    void Start()
    {
        GameObject gb = GameObject.Find("BulletsScriptObj");
        script_pool = gb.GetComponent<ObjectPool>();
        script_shoot = gb.GetComponent<Bullets>();
    }

    void Update()
    {
        if(framecount%60==0){
            Debug.Log("shot");
            script_shoot.radiation(
                bullet_type: 1,
                scale: 1.0f,
                Pos:this.transform.position,
                way:8,
                speed: 1.0f,
                spread: 360.0f,
                direction: -1.0f
            );
        }
        framecount ++;
    }
}