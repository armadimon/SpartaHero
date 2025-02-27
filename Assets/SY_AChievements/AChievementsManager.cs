using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AChievementsManager : MonoBehaviour
{
    AChievementsManager Instance;
    GameManager gameManager;
    GameDataManager gameDataManager;
    public PlayerController player { get; private set; }

    bool StageClearChk = false;
    bool BossKillChk = false;
    bool WeaponChangeChk = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameDataManager = FindObjectOfType<GameDataManager>();
        gameDataManager.AddAchievement("StageClear");
        gameDataManager.AddAchievement("BossKill");
        gameDataManager.AddAchievement("UsePotion");
    }
}
