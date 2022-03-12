//https://jp.gamesindustry.biz/article/1805/18050901/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot_Homing : MonoBehaviour
{
    private GameObject target = null;
    private Rigidbody2D rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Activate();
    }

    public void Activate()
    {
        float tmpdis = 0;
        float mindis = 0;

        GameObject[] _elist = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] _blist = GameObject.FindGameObjectsWithTag("Boss");
        if (_elist.Length + _blist.Length == 0)
        {
            target = null;
        }
        else
        {
            foreach (GameObject _obj in _elist)
            {
                ClosestTarget(_obj);
            }
            foreach (GameObject _obj in _blist)
            {
                ClosestTarget(_obj);
            }
        }

        void ClosestTarget(GameObject _obj)
        {
            tmpdis = Vector3.Distance(this.transform.position, _obj.transform.position);
            if (tmpdis < mindis || mindis == 0)
            {
                mindis = tmpdis;
                target = _obj;
            }
        }
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            var diff = (target.transform.position - this.transform.position).normalized;
            rb.AddForce(diff * 4.0f);
            var speed_updated = rb.velocity.normalized * 3.0f;
            rb.velocity = speed_updated;
        }
    }


}
