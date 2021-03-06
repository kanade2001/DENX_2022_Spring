using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class ShotGenerator : MonoBehaviour
{
    private ObjectPool script_pool;
    private float Pi = Mathf.Acos(-1);
    void Start()
    {
        script_pool = GameObject.Find("ShotManager").GetComponent<ObjectPool>();
    }

    public List<GameObject> Radiation(string shot_type, float scale, Vector3 Pos, int way, float speed, float spread = 0.0f, float direction = -1)
    {
        //bullet_type: 弾の種類
        //scale: 弾の大きさ
        //Pos: 発射位置
        //way: Nway弾
        //speed: 発射速度
        //spread: 拡散角(degree)
        //direction: 発射方向(0: 真下, <0: 自機狙い)

        List<GameObject> _list = new List<GameObject>();

        //degree->rad
        spread = spread * Pi / 180.0f;
        direction = direction * Pi / 180.0f;

        //自機狙い
        if (direction < 0)
        {
            direction = GetAngle(Pos);
        }

        //
        if (way == 1)
        {
            float dir = direction;
            var bullet = script_pool.Create(shot_type);
            _list.Add(bullet);
            bullet.transform.position = Pos;
            Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(-Mathf.Sin(dir) * speed, -Mathf.Cos(dir) * speed, 0.0f);
        }
        else
        {
            for (int i = 0; i < way; i++)
            {
                float dir = direction - spread / 2.0f + spread / (float)(way - 1) * (float)i;
                var bullet = script_pool.Create(shot_type);
                _list.Add(bullet);
                bullet.transform.position = Pos;
                Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector3(-Mathf.Sin(dir) * speed, -Mathf.Cos(dir) * speed, 0.0f);
            }
        }

        return _list;
    }

    public GameObject Single(string shot_type, float scale, Vector3 Pos, float speed, float direction = -1)
    {
        direction = direction * Pi / 180.0f;

        if (direction < 0)
        {
            direction = GetAngle(Pos);
        }

        float dir = direction;
        var bullet = script_pool.Create(shot_type);
        bullet.transform.position = Pos;
        Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(-Mathf.Sin(dir) * speed, -Mathf.Cos(dir) * speed, 0.0f);
        return bullet;
    }

    private float GetAngle(Vector3 Enemy_pos)
    {
        float Enemy_x = Enemy_pos.x;
        float Enemy_y = Enemy_pos.y;

        Vector3 Player_pos = GameObject.Find("Player").transform.position;
        float Player_x = Player_pos.x;
        float Player_y = Player_pos.y;

        return Mathf.Atan2(Enemy_x - Player_x, Enemy_y - Player_y);
    }
}
