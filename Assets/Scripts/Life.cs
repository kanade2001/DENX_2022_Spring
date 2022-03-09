using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    [SerializeField] int life = 20; //体力
    void FixedUpdate()
    {
        if(life <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
        life --;
        Debug.Log(life);
    }
}
