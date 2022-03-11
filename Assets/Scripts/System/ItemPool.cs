using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPool : MonoBehaviour
{
    private List<GameObject> ItemList = new List<GameObject>();  //prefab用List
    private List<string> ItemName = new List<string>{"e_item","b_item","p_item","s_item"};  //prefab <-> index変換用
    private List<Stack<GameObject>> _itempool = new List<Stack<GameObject>>();  //ObjectPool
    private List<GameObject> _activeitem = new List<GameObject>();  //Activeなitem
    private int _popSeq = 5;  //足りない場合に一度にInstantiateする個数

    void start()
    {
        ItemList = GameObject.Find("ItemManager").GetComponent<ItemManager>().item;
    }

    public GameObject Create(int identifer)  //生成
    {
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
        _activeitem.Add(returnobj);
        return returnobj;
    }

    public void Release(GameObject sObj)  //使い終わった後の処理
    {
        sObj.SetActive(false);
        _activeitem.Remove(sObj);
        int identifer=0;
        for(int i=0;i<4;i++){
            if(sObj.name == ItemName[i]){
                identifer = i;
                break;
            }
        }
        _itempool[identifer].Push(sObj);
    }

    public void AllCollect(Vector3 pos)
    {
        while(_activeitem.Count>0)
        {
            GameObject cObj = _activeitem[_activeitem.Count - 1];
            Release(cObj);
        }
    }
}