using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AChievements : MonoBehaviour
{
    GameDataManager gameDataManager;
    private List<AChievementsList> aChievementsList = new List<AChievementsList>();

    public int StageClearNum = 0;
    public int BossKillNum = 0;
    public int WeaponChangeNum = 0;

    private void AChievementCreate()
    {
        aChievementsList.Add(new AChievementsList("StageClear", () => StageClear(3)));
        aChievementsList.Add(new AChievementsList("BossKill", () => BossKill(1)));
        aChievementsList.Add(new AChievementsList("WeaponChange", () => WeaponChange(1)));
    }

    public void StageClear(int ClearNum)
    {
        if (WeaponChangeNum > ClearNum)
            gameDataManager.AddAchievement($"{aChievementsList[0].Name}");
    }
    public void BossKill(int KillNum)
    {
        if (WeaponChangeNum > KillNum)
            gameDataManager.AddAchievement($"{aChievementsList[1].Name}");
    }
    public void WeaponChange(int ChangeNum)
    {
        if(WeaponChangeNum > ChangeNum)
            gameDataManager.AddAchievement($"{aChievementsList[2].Name}");
    }


    private class AChievementsList
    {
        public string Name {  get; set; }
        public Action Action;

        public AChievementsList(string name,Action action = null) 
        {
            Name = name;
            Action = action;
        }
    }


}
