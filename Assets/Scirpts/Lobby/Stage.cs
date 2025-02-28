using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage
{
    public int Level { get; set; }
    public int StageClearExp { get; set; }
    public int StageClearGold { get; set; }
    public int MonstersMin { get; set; }
    public int MonstersMax { get; set; }



    public Stage (int level, int exp, int gold, int min, int max)
    {
        Level = level;
        StageClearExp = exp;
        StageClearGold = gold;
        MonstersMin = min;
        MonstersMax = max;
    }
}
