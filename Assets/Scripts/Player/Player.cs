using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject shot_prefab;
    private Vector2 playerPos;
    private int shot_time = 0;//射撃間隔用
    private CountManager CM;
    private ItemManager IM;
    private ShotGenerator SG;
    Rigidbody2D rb;
    public bool IsDead = false;

    private bool IsDebugmode = true;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        rb = this.GetComponent<Rigidbody2D>();
        CM = GameObject.Find("CountManager").GetComponent<CountManager>();
        IM = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        SG = GameObject.Find("ShotManager").GetComponent<ShotGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDead == false){ //非死亡時
            PlayerMove();
            PlayerShot();
        }
        if(this.gameObject.transform.position.y > 3.5f)
        {
            IM.AutoItemCollect();
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(IsDead == false){
            if(coll.gameObject.tag == "Item")
            {
                coll.gameObject.GetComponent<ItemMove>().ItemCollect();
            }
            else
            {
                if(!IsDebugmode)
                {
                    IsDead = true;
                    StartCoroutine(Respawn());
                }
            }
        }
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
                        5.0f,
                        5.0f,
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
                        5.0f,
                        10.0f,
                        180.0f
                    );
                }
            }
            if(_power>=200)
            {
                int shot_rate = 30;
                if(_power == 400){shot_rate /= 2;}
                if(shot_time%shot_rate == 0)
                {
                    List<GameObject> _list = new List<GameObject>();
                    _list = SG.Radiation(
                        "player_shot_2",
                        1.0f,
                        this.playerPos,
                        2,
                        4.0f,
                        120.0f,
                        180.0f
                    );
                    foreach(GameObject _obj in _list){
                        _obj.GetComponent<PlayerShot_Homing>().Activate();
                    }
                }
            }
        }
        if(Input.GetKeyUp(KeyCode.Z))
        {
            shot_time = 0;
        }
    }

    private IEnumerator Respawn()
    {
        CM.Life = -1;
        int _life = CM.Life;
        if(_life<0)
        {
            gameover();
        }
        this.transform.position = new Vector2(0, -7);  //画面外に移動
        rb.velocity = new Vector3(0,2,0);
        while(this.transform.position.y < -3.0)
        {
            yield return null;
        }
        rb.velocity = new Vector3(0,0,0);
        //judge = 0;
        //this.desObj.SetActive(true);
        IsDead = false;
    }

    private void gameover()
    {

    }
}