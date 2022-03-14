using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SM_B3_S2_1 : MonoBehaviour
{
    private bool IsStop = true;
    private Rigidbody2D rb;
    private bool _dir = true;
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.transform.position.y > 8.5f)
        {
            _dir = true;
            IsStop = true;
        }
        else if (gameObject.transform.position.x < -8.5f)
        {
            _dir = false;
            IsStop = true;
        }
        if (IsStop)
        {
            if (_dir)
            {
                rb.velocity = new Vector3(0.0f, -5.0f * (this.transform.position.y - 8.0f), 0.0f);
            }
            else
            {
                rb.velocity = new Vector3(-5.0f * (this.transform.position.x + 8.0f), 0.0f, 0.0f);
            }
        }
        else
        {
            Debug.Log("update");
            if (_dir)
            {
                rb.AddForce(new Vector3(0.0f, -2.0f, 0.0f));
            }
            else
            {
                rb.AddForce(new Vector3(2.0f, 0.0f, 0.0f));
            }
        }
    }

    public void Activate()
    {
        IsStop = false;
    }
}
