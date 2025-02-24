using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int Level { get; set; }
    public int Exp { get; set; }
    public int MonstersNum { get; set; }



    public Stage (int level, int exp, int min, int max)
    {
        Level = level;
        Exp = exp;
        MonstersNum = Random.Range(min, max);
    }

}
