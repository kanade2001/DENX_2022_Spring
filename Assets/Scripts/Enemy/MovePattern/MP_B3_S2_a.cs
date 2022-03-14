using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MP_B3_S2_a : MonoBehaviour
{
    GameObject _player;
    Rigidbody2D rb;
    private int _IsOut = 0;
    void Start()
    {
        _player = GameObject.Find("Player");
        rb = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(_IsOut<2)
        {
            var diff = _player.transform.position - this.transform.position;
            if(_IsOut==0 && diff.magnitude < 2.5f)
            {
                _IsOut  = 1;
            }else if(_IsOut == 1 && diff.magnitude>2.5f)
            {
                _IsOut = 2;
            }
            rb.AddForce(diff.normalized * 2.0f);
            var speed_updated = rb.velocity.normalized * 3.0f;
            rb.velocity = speed_updated;
        }

    }
}
