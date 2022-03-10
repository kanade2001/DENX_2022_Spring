using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_1_3way : MonoBehaviour
{
    // Start is called before the first frame update

    private int _count=0;
    private Bullets bullets;
    void Start()
    {
        bullets = GameObject.Find("ShotManager").GetComponent<Bullets>();
        StartCoroutine(wait());
    }

    // Update is called once per frame
    private IEnumerator wait()
    {
        for(int i=0;i<120;i++)
        {
            yield return null;
        }
    }

    void FixedUpdate()
    {
        if(_count%30==0){
            bullets.radiation(
                "shot_1",
                1.0f,
                this.transform.position,
                3,
                2.0f,
                30.0f,
                -1.0f
            );
        }
        _count ++;
    }


}
