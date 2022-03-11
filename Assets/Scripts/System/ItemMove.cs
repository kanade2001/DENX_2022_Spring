using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    Rigidbody2D rb;
    ItemPool IP;
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0.0f,2.0f,0.0f);
        IP = GameObject.Find("ItemManager").GetComponent<ItemPool>();
    }
    void FixedUpdate()
    {
        if(rb.velocity.y > -2.0f)
        {
            var _force = new Vector3(0.0f,-1.5f,0.0f);
            rb.AddForce(_force);
        }
        if(this.transform.position.y<-6.0f)
        {
            IP.Release(gameObject);
        }
    }
}
