using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    public GameObject skillSelectPanel;
    public GameObject stageResultPanel;
    public GameObject BossUI;
    public GameObject AchievementsBtn;
    public void Init()
    {
        UIManager.Instance.RegisterPanel("SkillSelectActive", skillSelectPanel);
        UIManager.Instance.RegisterPanel("StageClear", stageResultPanel);
        UIManager.Instance.RegisterPanel("BossUI", BossUI);

    }

    public void SwitchingClearPanel()
    {
        ReturnBtn temp = stageResultPanel.GetComponentInChildren<ReturnBtn>();
        temp.gameObject.SetActive(false);
    }
    
    public void HidePanel(string name)
    {
        UIManager.Instance.HidePanel(name);
    }
}
