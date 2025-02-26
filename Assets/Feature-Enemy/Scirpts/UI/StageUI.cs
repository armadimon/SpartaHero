using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageUI : MonoBehaviour
{
    public GameObject panel;
    private void Start()
    {
        UIManager.Instance.RegisterPanel("SkillSelectActive", panel);
    }
}
