using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerDataTool : MonoBehaviour
{
    [MenuItem("Tool/데이터초기화")]
    private static void ResetData()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("데이터가 사라졌습니다.");
    }

    [MenuItem("Tool/돈 치트")]
    private static void ShowMeTheMoney()
    {
        GameDataManager.Instance.AddGold(1000);
    }
}
