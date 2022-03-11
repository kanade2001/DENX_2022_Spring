using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountManager : MonoBehaviour
{

    private int _life = 3;
    private int _bom = 3;
    private int _power = 100;
    private int _score = 0;


    public int Life
    {
        get => _life;
        set => _life = _life + value;
    }

    public int Bom
    {
        get => _bom;
        set => _bom = _bom + value;
    }

    public int Power
    {
        get => _power;
        set => _power = Mathf.Max(_power + value,100);
    }

    public int Score
    {
        set => _score = _score + value;
    }
}
