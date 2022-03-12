using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> item = new List<GameObject>(4);
    private ItemPool IP;
    private ItemMove IM;
    private CountManager CM;

    void Start()
    {
        IP = GameObject.Find("ItemManager").GetComponent<ItemPool>();
        CM = GameObject.Find("CountManager").GetComponent<CountManager>();
    }
    public void MakeItem(Vector3 pos, int p_item_num, int s_item_num, int o_item_num)
    {
        //p_item : power item
        //s_item : score item
        //o_item : 1->Extend 2->BomExtend

        if (CM.Power == 400)
        {
            s_item_num += p_item_num;
            p_item_num = 0;
        }

        //itemの個数が1個のみの場合は敵の位置にアイテムを出現させる
        if (o_item_num == 1)
        {
            PopItem(pos, 2);
        }
        else if (o_item_num == 2)
        {
            PopItem(pos, 3);
        }
        else if (p_item_num == 1)
        {
            PopItem(pos, 0);
            p_item_num--;
        }
        else if (s_item_num == 1)
        {
            PopItem(pos, 1);
            s_item_num--;
        }

        while (p_item_num > 0)
        {
            PopItem(RandomPopPoint(pos), 0);
            p_item_num--;
        }
        while (s_item_num > 0)
        {
            PopItem(RandomPopPoint(pos), 1);
            s_item_num--;
        }
    }
    private Vector3 RandomPopPoint(Vector3 pos)
    {
        float rad = Random.Range(0.0f, 3.1f);
        Vector3 _pos = new Vector3(
            Mathf.Clamp(pos.x + 0.5f * Mathf.Cos(rad), -5.0f, 5.0f),
            pos.y + 0.5f * Mathf.Sin(rad));
        return _pos;
    }
    private void PopItem(Vector3 pos, int identifer)
    {
        GameObject obj = IP.Create(identifer);
        IM = obj.GetComponent<ItemMove>();
        obj.transform.position = new Vector3(
            Mathf.Clamp(pos.x, -5.0f, 5.0f),
            Mathf.Clamp(pos.y, -5.0f, 5.0f)
        );
        IM.Activate();
    }
    public void AutoItemCollect()
    {
        IP.AutoItemCollect();
    }
}
