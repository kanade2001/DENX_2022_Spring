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

    public ObjectPool<ParticleSystem> Pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<ParticleSystem>(OnCreatePoolObject, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, false, DefaultCapacity, MaxSize);
            }
            return _pool;
        }
    }

    ParticleSystem OnCreatePoolObject()
    {
        Debug.Log("Called CreatePooledItem");

        // プールするパーティクルシステムの作成
        //var go = new GameObject($"Pooled Particle System: {_nextId++}");
        var go = Instantiate(shot_1,transform.position, Quaternion.identity);
        var ps = go.AddComponent<ParticleSystem>();
        // パーティクルの終了挙動をエミッター停止 & エミッションのクリアとする
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // パーティクルを1秒のワンショット再生とする
        // (ので約1秒後にパーティクルは停止する)
        var main = ps.main;
        main.duration = 1f;
        main.startLifetime = 1f;
        main.loop = false;

        // パーティクルが終了したらプールに返却するための
        // 挙動を実装したコンポーネントをアタッチ
        var returnToPool = go.AddComponent<ReturnToPool>();
        returnToPool.Pool = Pool;

        Debug.Log($"Created {ps.gameObject.name}");

        return ps;
    }

    void OnTakeFromPool(ParticleSystem ps)
    {
        Debug.Log($"Called OnTakeFromPool: ({ps.gameObject.name})");

        // プールからパーティクルシステムを借りるときに
        // そのオブジェクトのアクティブをONにする
        ps.gameObject.SetActive(true);
    }

    void OnReturnedToPool(ParticleSystem ps)
    {
        Debug.Log($"Called OnReturnedToPool: ({ps.gameObject.name})");

        // 逆にプールにパーティクルシステムを返却するときに
        // そのオブジェクトのアクティブをOFFにする
        ps.gameObject.SetActive(false);
    }

    void OnDestroyPoolObject(ParticleSystem ps)
    {
        Debug.Log($"Called OnDestroyPoolObject: ({ps.gameObject.name})");

        // プールされたパーティクルの削除が要求されているので、
        // オブジェクトを破棄する。
        //
        // OnCreatePoolObjectでオブジェクトを生成しているので
        // ここで破棄する責務があるという解釈
        Destroy(ps.gameObject);
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
    
    private ObjectPool<ParticleSystem> _pool = null;
}