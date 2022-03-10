using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    [SerializeField] int life = 20; //体力
    [SerializeField] int score = 100;
    [SerializeField] int item = 0;

    void FixedUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if(System.Math.Abs(x)>7 || System.Math.Abs(y)>7)
        {
            Destroy(gameObject);
        }
            if(life <= 0)
        {
            shootdown();
        }
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
        life --;
        Debug.Log(life);
    }

    private void shootdown()
    {
        Destroy(gameObject);
    }
}
