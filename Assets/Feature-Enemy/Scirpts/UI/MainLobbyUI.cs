using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyUI : MonoBehaviour
{
    Text goldText;
    private void Start()
    {
        UIManager.Instance.RegisterPanel("GoldUI", gameObject);
        goldText = GetComponentInChildren<Text>();
        Debug.Log("Gold : "  + goldText.text);
        Debug.Log("Gold : "  + GameDataManager.Instance.GetGold().ToString());
        goldText.text = GameDataManager.Instance.GetGold().ToString();
    }
}
