using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> PrefabList = new List<GameObject>();
    public Dictionary<string,Stack<GameObject>> ObjPoolDic = new Dictionary<string,Stack<GameObject>>();
    public List<GameObject> EnableObjList = new List<GameObject>();

    private int _popFirst = 50;
    private int _popSeq = 5;

    public void Register(string name){//登録
        GameObject regist = PrefabList.Find(x => x.name == name);
        if(!ObjPoolDic.ContainsKey(regist.name)){
            ObjPoolDic.Add(regist.name,new Stack<GameObject>());
        }
        for(int a = 0;a<_popFirst;a++){
            GameObject pObj = Instantiate(regist);
            pObj.SetActive(false);
            pObj.name = name;
            ObjPoolDic[regist.name].Push(pObj);
            }
    }

    public GameObject Create(string name){//生成
        if(!ObjPoolDic.ContainsKey(name)){
            Register(name);
        }
        var _stack = ObjPoolDic[name];
        if(_stack.Count == 0){
            GameObject regist = PrefabList.Find(x => x.name == name);
            for(int a = 0;a<_popSeq;a++){
                GameObject pObj = Instantiate(regist);
                pObj.SetActive(false);
                pObj.name = name;
                _stack.Push(pObj);
            }
        }
        GameObject returnobj = _stack.Pop();
        returnobj.SetActive(true);
        EnableObjList.Add(returnobj);
        return returnobj;
    }

    public void Release(GameObject sobj){//使い終わった後の処理
        sobj.SetActive(false);
        EnableObjList.Remove(sobj);
        ObjPoolDic[sobj.name].Push(sobj);
    }
}