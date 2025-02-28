using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClearUI : MonoBehaviour
{
    public TMPro.TMP_Text text;
    void Start()
    {
        text.text = GameManager.Instance.stageManager.CurrentGold.ToString();
    }
    
}
