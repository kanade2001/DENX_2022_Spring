using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_B3_S2 : MonoBehaviour
{
    private ShotGenerator SG;
    public GameObject _enemy;

    void Start()
    {
        SG = GameObject.Find("ShotManager").GetComponent<ShotGenerator>();
        StartCoroutine(Main());
    }

    private IEnumerator Main()
    {
        for(int i=0;i<30;i++){
            yield return null;
        }
        while(true)
        {
            StartCoroutine(Column());
            for(int i=0;i<180;i++)
            {
                yield return null;
            }
            StartCoroutine(Row());
            for(int i=0;i<180;i++)
            {
                yield return null;
            }
        }
    }


    private IEnumerator Column()//縦
    {
        Queue<GameObject> _que = new Queue<GameObject>();
        for (int i = 0; i <= 50; i++)
        {
            if (i % 5 == 0)
            {
                var bullet = SG.Single(
                    "shot_B3_S2_1",
                    1.0f,
                    new Vector3(-5.0f + (float)(i / 5), 9.0f, 0.0f),
                    3.0f,
                    0.0f
                );
                Instantiate(_enemy, new Vector3(-5.0f + (float)(i / 5),5.2f, 0.0f), Quaternion.identity);
                _que.Enqueue(bullet);
            }
            yield return null;
        }
        for (int i = 0; i < 30; i++)
        {
            yield return null;
        }
        while (_que.Count != 0)
        {
            var bullet = _que.Dequeue();
            bullet.GetComponent<SM_B3_S2_1>().Activate();
        }
    }

    private IEnumerator Row()//横
    {
        Queue<GameObject> _que = new Queue<GameObject>();
        for (int i = 0; i <= 50; i++)
        {
            if (i % 5 == 0)
            {
                var bullet = SG.Single(
                    "shot_B3_S2_1",
                    1.0f,
                    new Vector3(-9.0f, 5.0f - (float)(i / 5), 0.0f),
                    3.0f,
                    90.0f
                );
                Instantiate(_enemy, new Vector3(-5.2f, 5.0f - (float)(i / 5), 0.0f), Quaternion.identity);
                _que.Enqueue(bullet);
            }
            yield return null;
        }
        for (int i = 0; i < 30; i++)
        {
            yield return null;
        }
        while (_que.Count != 0)
        {
            var bullet = _que.Dequeue();
            bullet.GetComponent<SM_B3_S2_1>().Activate();
        }
    }
}