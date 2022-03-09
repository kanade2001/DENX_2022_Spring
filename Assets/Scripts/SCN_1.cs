using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCN_1 : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemy_0;
    void Start()
    {
        Debug.Log("CreateEnemy");
        Instantiate(enemy_0, new Vector3(0.0f,6.0f,0.0f), Quaternion.identity);
        Debug.Log("Complete");
    }
}
