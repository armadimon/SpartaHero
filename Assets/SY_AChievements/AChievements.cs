using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AChievements : MonoBehaviour
{
    private List<AChievementsList> AChievementslist = new List<AChievementsList>();

    private void AChievementCreate()
    {
        AChievementslist.Add(new AChievementsList("StageClear", false, () => StageClear()));
        AChievementslist.Add(new AChievementsList("BossKill", false, () => BossKill()));
        AChievementslist.Add(new AChievementsList("WeaponChange", false, () => WeaponChange()));
    }

    public void StageClear()
    {

    }
    public void BossKill()
    {

    }
    public void WeaponChange()
    {

    }


    private class AChievementsList
    {
        public string Name {  get; private set; }
        public bool Clear {  get; private set; }
        public Action Action;

        public AChievementsList(string name, bool clear = false, Action action = null) 
        {
            Name = name;
            Clear = clear;
            Action = action;
        }
    }


}
