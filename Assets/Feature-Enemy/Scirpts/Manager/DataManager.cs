using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    private int totalGold = 0;
    public List<ItemSet> itemSets = new List<ItemSet>();
    public event Action<int> OnGoldChanged;

    public int TotalGold
    {
        get { return totalGold; }
        set
        {
            if (totalGold != value)
            {
                totalGold = value;
                OnGoldChanged?.Invoke(totalGold);
            }
        }
    }

    private List<string> achievements = new List<string>();
    public List<string> Inventory = new List<string>();

    public int StageClearNum = 0;
    public int BossKillNum = 0;
    public int UsePotionNum = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGameDatas();
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void SaveGameData<T>(string key, T value)
    {
        string json = JsonConvert.SerializeObject(value);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }

    public T LoadGameData<T>(string key, T defaultValue)
    {
        if (PlayerPrefs.HasKey(key))
        {
            Debug.Log(PlayerPrefs.GetString(key));
            string json = PlayerPrefs.GetString(key);
            return JsonConvert.DeserializeObject<T>(json);
        }

        return defaultValue;
    }

    private void LoadGameDatas()
    {
        TotalGold = LoadGameData("Gold", 0);
        achievements = LoadGameData("Achievements", new List<string>());
        Inventory = LoadGameData("Inventory", new List<string>());
    }

    public void AddGold(int amount)
    {
        TotalGold += amount;
        SaveGameData("Gold", totalGold);
    }

    public void TakeGold(int amount)
    {
        TotalGold -= amount;
        SaveGameData("Gold", totalGold);
    }

    public int GetGold()
    {
        return totalGold;
    }

    public void AddItemToInventory(string ItemName)
    {
        if (Inventory.Contains(ItemName))
            return;
        Inventory.Add(ItemName);
    }

    public void AddAchievement(string achievement)
    {
        if (!achievements.Contains(achievement))
        {
            achievements.Add(achievement);
            SaveGameData("Achievements", achievements);
        }
    }

    public int GetStageClearNum()
    {
        return StageClearNum++;
    }

    public int GetBossKillNum()
    {
        return BossKillNum++;
    }

    public int GetUsePotionNum()
    {
        return UsePotionNum++;
    }
}

[System.Serializable]
public class PlayerData
{
    public int gold;
    public List<string> inventory;
    public Dictionary<string, int> achievements;

    public PlayerData()
    {
        gold = 0;
        inventory = new List<string>();
        achievements = new Dictionary<string, int>();
    }
}