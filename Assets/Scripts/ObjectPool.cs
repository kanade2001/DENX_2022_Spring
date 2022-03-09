//https://blog.yucchiy.com/2021/04/objectpool-in-unity-2021/
//https://github.com/yucchiy/unity-sandbox/blob/main/Assets/ObjectPoolCheck/Scripts/ObjectPoolExample.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    public GameObject shot_1;


    // 初期のプールサイズ
    public int DefaultCapacity = 10;
    // プールサイズを最大どれだけ大きくするか
    public int MaxSize = 100;

    public ObjectPool<GameObject> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<GameObject>(
                    OnCreatePoolObject,
                    OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject,
                    false,
                    DefaultCapacity,
                    MaxSize);
            }
            return _pool;
        }
    }

    GameObject OnCreatePoolObject()
    {
        Debug.Log("Called CreatePooledItem");

        // プールするパーティクルシステムの作成
        //var go = new GameObject($"Pooled Particle System: {_nextId++}");
        var obj = Instantiate(shot_1, transform.position, Quaternion.identity);
        return obj;
    }

    void OnTakeFromPool(GameObject obj)
    {
        Debug.Log($"Called OnTakeFromPool");
        obj.SetActive(true);
    }

    void OnReturnedToPool(GameObject obj)
    {
        Debug.Log($"Called OnReturnedToPool");

        // 逆にプールにパーティクルシステムを返却するときに
        // そのオブジェクトのアクティブをOFFにする
        obj.SetActive(false);
    }

    void OnDestroyPoolObject(GameObject obj)
    {
        Destroy(obj);
    }

    void ClearPool()
    {
        // プールを破棄する
        if (_pool != null)
        {
            _pool.Clear();
            _pool = null;
        }
    }
    
    private ObjectPool<GameObject> _pool = null;
}