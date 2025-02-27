using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    public GameObject skillSelectPanel;
    public GameObject stageResultPanel;
    private void Start()
    {
        UIManager.Instance.RegisterPanel("SkillSelectActive", skillSelectPanel);
        UIManager.Instance.RegisterPanel("StageClear", stageResultPanel);
    }
}
