using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Script : MonoBehaviour
{
    [SerializeField] int life = 20; //体力
    [SerializeField] int score = 100;
    [SerializeField] int p_item = 0;
    [SerializeField] int s_item = 0;
    [SerializeField] int o_item = 0;

    private ItemManager IM;
    private CountManager CM;

    void Start()
    {
        IM = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        CM = GameObject.Find("CountManager").GetComponent<CountManager>();
    }

    void FixedUpdate()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        if(System.Math.Abs(x)>7 || System.Math.Abs(y)>7)
        {
            Destroy(gameObject);
        }
            if(life <= 0)
        {
            shootdown();
        }
    }
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        Destroy(coll.gameObject);
        life --;
        Debug.Log(life);
    }

    private void shootdown()
    {
        IM.MakeItem(this.transform.position,p_item,s_item,o_item);
        CM.Score = score;
        Destroy(gameObject);
    }
}
