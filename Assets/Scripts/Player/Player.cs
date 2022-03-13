using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    private int shot_time = 0;//射撃間隔用
    private Rigidbody2D rb;
    public bool IsDead = false;
    private bool IsInvincible = false;
    private int IsInvincibleCountMax = 240;
    private CountManager CM;
    private ItemManager IM;
    private ShotGenerator SG;

    void Start()
    {
        Application.targetFrameRate = 60;
        rb = this.GetComponent<Rigidbody2D>();
        CM = GameObject.Find("CountManager").GetComponent<CountManager>();
        IM = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        SG = GameObject.Find("ShotManager").GetComponent<ShotGenerator>();
    }

    void FixedUpdate()
    {
        if (!IsDead)
        { //非死亡時
            PlayerMove();
            PlayerShot();
            PlayerItemCollect();
        }
        if(!IsInvincible)
        {
            PlayerBomStart();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Item")
        {
            coll.gameObject.GetComponent<ItemMove>().ItemCollect();
        }

        if(!IsInvincible)
        {
            IsDead = true;
            StartCoroutine(Respawn());
        }
    }

    private void PlayerMove()
    {
        float movespeed = 6.0f;
        const float playerPosXclamp = 5.0f;
        const float playerPosYclamp = 5.0f;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movespeed *= (float)0.5;
        }
        if (x != 0 && y != 0)
        {
            movespeed *= (float)0.8;
        }
        this.transform.position += new Vector3(x, y, 0) * Time.deltaTime * movespeed;

        var _pos = this.transform.position;
        _pos.x = Mathf.Clamp(_pos.x, -playerPosXclamp, playerPosXclamp);
        _pos.y = Mathf.Clamp(_pos.y, -playerPosYclamp, playerPosYclamp);
        this.transform.position = _pos;
    }

    private void PlayerShot()
    {
        //ショット
        if (Input.GetKey(KeyCode.Z))
        {
            int _power = CM.Power;
            shot_time++;
            shot_time %= 60;

            if (_power < 300)
            {
                if (shot_time % 12 == 0)
                {
                    SG.Radiation(
                        "player_shot_1",
                        1.0f,
                        this.gameObject.transform.position,
                        2,
                        5.0f,
                        5.0f,
                        180.0f
                    );
                }
            }
            else
            {
                if (shot_time % 12 == 0)
                {
                    SG.Radiation(
                        "player_shot_1",
                        1.0f,
                        this.gameObject.transform.position,
                        4,
                        5.0f,
                        10.0f,
                        180.0f
                    );
                }
            }
            if (_power >= 200)
            {
                int shot_rate = 30;
                if (_power == 400) { shot_rate /= 2; }
                if (shot_time % shot_rate == 0)
                {
                    List<GameObject> _list = new List<GameObject>();
                    _list = SG.Radiation(
                        "player_shot_2",
                        1.0f,
                        this.gameObject.transform.position,
                        2,
                        4.0f,
                        120.0f,
                        180.0f
                    );
                    foreach (GameObject _obj in _list)
                    {
                        _obj.GetComponent<PlayerShot_Homing>().Activate();
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            shot_time = 0;
        }
    }

    private void PlayerItemCollect()
    {
        if (this.gameObject.transform.position.y > 3.5f)
            {
                IM.AutoItemCollect();
            }
    }

    private void PlayerBomStart()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Invincible());
            StartCoroutine(Bom());
        }
    }

    private IEnumerator Invincible()
    {
        IsInvincible = true;
        for(int i=0; i<IsInvincibleCountMax;i++){
            var _color = this.gameObject.GetComponent<SpriteRenderer>().color;
            _color.a = 0.5f+Mathf.Cos(i*8*2*Mathf.Acos(-1)/IsInvincibleCountMax)/2;
            this.gameObject.GetComponent<SpriteRenderer>().color = _color;
            yield return null;
        }
        var __color = this.gameObject.GetComponent<SpriteRenderer>().color;
        __color.a = 1;
        this.gameObject.GetComponent<SpriteRenderer>().color = __color;
        IsInvincible = false;
    }

    private IEnumerator Respawn()
    {
        CM.Life = -1;
        int _life = CM.Life;
        if (_life < 0)
        {
            gameover();
        }
        this.transform.position = new Vector3(0, -7, 0);  //画面外に移動
        StartCoroutine(Invincible());
        rb.velocity = new Vector3(0, 2, 0);
        while (this.transform.position.y < -3.0)
        {
            yield return null;
        }
        rb.velocity = new Vector3(0, 0, 0);
        //judge = 0;
        //this.desObj.SetActive(true);
        IsDead = false;
    }

    private void gameover()
    {

    }

    private IEnumerator Bom()
    {
        for (int i=0;i < 18*4 ; i++)
        {
            Debug.Log(i);
            SG.Radiation(
                "player_shot_3",
                1.0f,
                this.gameObject.transform.position,
                1,
                5.0f,
                10.0f,
                i * 20.0f
            );
            yield return null;
        }
    }
}