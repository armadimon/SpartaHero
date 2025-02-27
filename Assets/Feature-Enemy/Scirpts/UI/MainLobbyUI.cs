using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainLobbyUI : MonoBehaviour
{
    public GameDataManager dataManager;
    Text goldText;
    private void Start()
    {
        UIManager.Instance.RegisterPanel("GoldUI", gameObject);
        goldText = GetComponentInChildren<Text>();
        dataManager = GameDataManager.Instance;
        goldText.text = dataManager.GetGold().ToString();
    }
    
    private void OnEnable()
    {
        dataManager.OnGoldChanged += UpdateScoreUI; // 이벤트 구독
    }

    private void OnDisable()
    {
        dataManager.OnGoldChanged -= UpdateScoreUI; // 이벤트 구독 해제
    }

    private void UpdateScoreUI(int Gold)
    {
        goldText.text = Gold.ToString();
    }
}
