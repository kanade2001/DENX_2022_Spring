using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private ItemPool IP;
    private CountManager CM;
    private bool IsCollect = false;
    private GameObject _player;
    private Player _playercs;
    void Start()
    {
        rb = this.transform.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector3(0.0f,2.0f,0.0f);
        IP = GameObject.Find("ItemManager").GetComponent<ItemPool>();
        CM = GameObject.Find("CountManager").GetComponent<CountManager>();
        _player = GameObject.Find("Player");
        _playercs = _player.GetComponent<Player>();
    }
    void FixedUpdate()
    {
        if(IsCollect == false)
        {
            if(rb.velocity.y > -2.0f)
            {
                var _force = new Vector3(0.0f,-1.5f,0.0f);
                rb.AddForce(_force);
            }
            if(this.transform.position.y<-6.0f)
            {
                IP.Release(gameObject);
            }
        }
        else
        {
            if(_playercs.IsDead == false)
            {
                var diff = (_player.transform.position - this.transform.position).normalized;
                if(Vector3.Distance(_player.transform.position,this.transform.position) < 0.3f)
                {
                    IP.Release(gameObject);
                }
                rb.AddForce(diff * 80.0f);
                var speed_updated = rb.velocity.normalized*8.0f;
                rb.velocity = speed_updated;
            }
            else
            {
                IP.Release(gameObject);
            }
        }
    }
    public void Activate()
    {
        IsCollect = false;
    }
    public void ItemCollect()
    {
        AddScoreItem();
        IsCollect = true;
    }
    private void AddScoreItem()
    {
        int _score = 10000 + (int)((this.gameObject.transform.position.y + 5) * 1000);
        CM.Score = _score;
        Debug.Log(_score);
    }
}
