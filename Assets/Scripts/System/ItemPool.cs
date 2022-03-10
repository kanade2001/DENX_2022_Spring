using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private List<GameObject> ItemList = new List<GameObject>();
    private List<string> ItemName = new List<string>{"e_item","b_item","p_item","s_item"};
    private List<Stack<GameObject>> _itempool = new List<Stack<GameObject>>();
    private int _popSeq = 5;

    void start()
    {
        ItemList = GameObject.Find("ItemManager").GetComponent<ItemManager>().item;
    }

    public GameObject Create(int identifer){//生成

        var _stack = _itempool[identifer];
        if(_stack.Count == 0){
            for(int a = 0;a<_popSeq;a++){
                GameObject pObj = Instantiate(ItemList[identifer]);
                pObj.SetActive(false);
                pObj.name = ItemName[identifer];
                _stack.Push(pObj);
            }
        }
        GameObject returnobj = _stack.Pop();
        returnobj.SetActive(true);
        return returnobj;
    }

    public async void Release(GameObject sobj){//使い終わった後の処理
        sobj.SetActive(false);
        int identifer=0;
        for(int i=0;i<4;i++){
            if(sobj.name == ItemName[i]){
                identifer = i;
                break;
            }
        }
        _itempool[identifer].Push(sobj);
    }
}
