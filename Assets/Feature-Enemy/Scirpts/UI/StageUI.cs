using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    public GameObject skillSelectPanel;
    public GameObject stageResultPanel;
    public GameObject BossUI;
    public void Init()
    {
        UIManager.Instance.RegisterPanel("SkillSelectActive", skillSelectPanel);
        UIManager.Instance.RegisterPanel("StageClear", stageResultPanel);
        UIManager.Instance.RegisterPanel("BossUI", BossUI);
        
    }
    
    public void HidePanel(string name)
    {
        UIManager.Instance.HidePanel(name);
    }
}
