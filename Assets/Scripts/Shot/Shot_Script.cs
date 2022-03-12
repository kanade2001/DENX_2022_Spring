using UnityEngine.Pool;
using UnityEngine;

public class Shot_Script : MonoBehaviour
{
    private ObjectPool OP;
    void Start()
    {
        OP = GameObject.Find("ShotManager").GetComponent<ObjectPool>();
    }

    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if (System.Math.Abs(x) > 7 || System.Math.Abs(y) > 7)
        {
            OP.Release(gameObject);
        }
    }
}