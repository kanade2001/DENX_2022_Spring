using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EM_DownStop : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0.0f,-3.0f,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.y<0)
        {
            var _force = new Vector3(0.0f,2.0f,0.0f);
            rb.AddForce(_force);
        }
    }
}
