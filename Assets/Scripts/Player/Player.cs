using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject shot_prefab;
    private Vector2 playerPos;
    private int shot_time = 0;//射撃間隔用
    private int shot_rate = 10;
    private CountManager CM;
    private ShotGenerator SG;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        CM = GameObject.Find("CountManager").GetComponent<CountManager>();
        SG = GameObject.Find("ShotManager").GetComponent<ShotGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        PlayerShot();
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
        Destroy(gameObject);
    }

    private void PlayerMove()
    {
        float movespeed = 6.0f;
        const float playerPosXclamp = 5.0f;
        const float playerPosYclamp = 5.0f;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if(Input.GetKey(KeyCode.LeftShift))
        {
            movespeed *= (float)0.5;
        }
        if(x != 0 && y != 0)
        {
            movespeed *= (float)0.8;
        }
        this.transform.position += new Vector3(x,y,0)*Time.deltaTime*movespeed;

        this.playerPos = transform.position;
        this.playerPos.x = Mathf.Clamp(this.playerPos.x,-playerPosXclamp,playerPosXclamp);
        this.playerPos.y = Mathf.Clamp(this.playerPos.y,-playerPosYclamp,playerPosYclamp);
        this.transform.position = new Vector2(this.playerPos.x,this.playerPos.y);
    }

    private void PlayerShot()
    {
        //ショット
        if(Input.GetKey(KeyCode.Z))
        {
            int _power = CM.Power;
            shot_time ++;
            shot_time %= 60;

            if(_power<300)
            {
                if(shot_time%12==0)
                {
                    SG.Radiation(
                        "player_shot_1",
                        1.0f,
                        this.playerPos,
                        2,
                        1.5f,
                        10.0f,
                        180.0f
                    );
                }
            }else{
                if(shot_time%12==0)
                {
                    SG.Radiation(
                        "player_shot_1",
                        1.0f,
                        this.playerPos,
                        4,
                        1.5f,
                        20.0f,
                        180.0f
                    );
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.Z))
        {
            shot_time = 0;
        }
    }
}