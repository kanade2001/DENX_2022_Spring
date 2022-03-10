using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Bullets : MonoBehaviour
{
    private ObjectPool script;
    public GameObject shot_1;
    

    void Start()
    {
        script = GameObject.Find("ShotManager").GetComponent<ObjectPool>();
    }


    private float Pi = Mathf.Acos(-1);
    public void radiation(int bullet_type, float scale, Vector3 Pos, int way, float speed, float spread=0.0f, float direction=-1)
    {
        //bullet_type: 弾の種類
        //scale: 弾の大きさ
        //Pos: 発射位置
        //way: Nway弾
        //speed: 発射速度
        //spread: 拡散角(degree)
        //direction: 発射方向(0: 真下, <0: 自機狙い)

        //degree->rad
        spread = spread * Pi / 180.0f;
        direction = direction * Pi / 180.0f;

        //自機狙い
        if(direction < 0)
        {
            direction = GetAngle(Pos);
        }

        //
        if(way==1)
        {
            float dir = direction;
            var bullet = script.Pool.Get();
            bullet.transform.position = Pos;
            //GameObject bullet = Instantiate(shot_1, Pos, Quaternion.identity);
            Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector3(-Mathf.Sin(dir)*speed, -Mathf.Cos(dir)*speed,0.0f);
        }else{
            for(int i=0; i<way; i++){
                float dir = direction - spread/2.0f + spread/(float)(way-1)*(float)i;
                var bullet = script.Pool.Get();
                bullet.transform.position = Pos;
                //GameObject bullet = Instantiate(shot_1, Pos, Quaternion.identity);
                Rigidbody2D rb = bullet.transform.GetComponent<Rigidbody2D>();
                rb.velocity = new Vector3(-Mathf.Sin(dir)*speed, -Mathf.Cos(dir)*speed,0.0f);
            }
        }
    }

    private float GetAngle(Vector3 Enemy_pos)
    {
        float Enemy_x = Enemy_pos.x;
        float Enemy_y = Enemy_pos.y;

        Vector3 Player_pos = GameObject.Find("Player").transform.position;
        float Player_x = Player_pos.x;
        float Player_y = Player_pos.y;

        return Mathf.Atan2(Enemy_x-Player_x,Enemy_y-Player_y);
    }
}
