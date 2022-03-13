using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Script : MonoBehaviour
{
    [SerializeField] int IsDircheck = 0;
    private ObjectPool OP;
    void Start()
    {
        OP = GameObject.Find("ShotManager").GetComponent<ObjectPool>();
    }

    void Update()
    {
        IsOutofRange();
        if (IsDircheck == 1)
        {
            var _v = this.GetComponent<Rigidbody2D>().velocity;
            var _dir = new Vector3(_v.x, _v.y, 0.0f);
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, _dir);
        }
    }

    private void IsOutofRange()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (System.Math.Abs(x) > 7 || System.Math.Abs(y) > 7)
        {
            OP.Release(gameObject);
        }
    }
}