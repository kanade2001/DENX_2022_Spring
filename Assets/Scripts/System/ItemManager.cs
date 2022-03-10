using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public List<GameObject> item = new List<GameObject>(4);

    private ItemPool _itempool_script;
    void start()
    {
        _itempool_script = GameObject.Find("ItemManager").GetComponent<ItemPool>();
    }
    public void MakeItem(Vector3 pos, int p_item_num, int s_item_num, int o_item_num)
    {
        //p_item : power item
        //s_item : score item
        //o_item : 1->Extend 2->BomExtend

        //itemの個数が1個のみの場合は敵の位置にアイテムを出現させる
        if(o_item_num == 1){
            PopItem(pos,3);
        }else if(o_item_num == 2){
            PopItem(pos,4);
        }else if(p_item_num == 1){
            PopItem(pos,1);
            p_item_num --;
        }else if(s_item_num == 1){
            PopItem(pos,2);
            s_item_num --;
        }
        
        while(p_item_num>0){
            PopItem(RandomPopPoint(pos),1);
            p_item_num --;
        }
        while(s_item_num>0){
            PopItem(RandomPopPoint(pos),2);
            s_item_num --;
        }
    }


    private Vector3 RandomPopPoint(Vector3 pos)
    {
        float rad = Random.Range(0.0f,3.1f);
        Vector3 _pos = new Vector3(pos.x+0.5f*Mathf.Cos(rad)+pos.y+0.5f*Mathf.Sin(rad),0.0f);
        return _pos;
    }

    private void PopItem(Vector3 pos, int identifer)
    {
        GameObject obj = _itempool_script.Create(identifer);
        obj.transform.position = pos;
    }
}
