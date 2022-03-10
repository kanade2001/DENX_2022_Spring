//https://github.com/yucchiy/unity-sandbox/blob/main/Assets/ObjectPoolCheck/Scripts/ReturnToPool.cs
using UnityEngine.Pool;
using UnityEngine;

public class ReturnToPool : MonoBehaviour
{
    private ObjectPool script_pool;
    void Start()
    {
        script_pool = GameObject.Find("ShotManager").GetComponent<ObjectPool>();
    }

    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if(System.Math.Abs(x)>7 || System.Math.Abs(y)>7)
        {
            script_pool.Pool.Release(gameObject);
        }
    }
}