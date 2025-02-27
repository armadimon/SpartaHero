using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager Instance { get; private set; }

    private int totalGold = 0;
    
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
        Debug.Log("check");
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


    // public static GameDataManager Instance { get; private set; }
    // private IDataSystem stageDataSystem;
    //
    // private void Awake()
    // {
    //     if (Instance == null)
    //     {
    //         Instance = this;
    //         DontDestroyOnLoad(gameObject);
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }
    //
    // public void SetScoreSystem(IDataSystem newSystem)
    // {
    //     stageDataSystem = newSystem;
    // }
    //
    // public int GetData(string key)
    // {
    //     return stageDataSystem?.GetData(key) ?? 0;
    // }
    //
    // public void SetData(string key, int score)
    // {
    //     stageDataSystem?.SetData(key, score);
    // }
    //
    // public void SaveDatas(string gameName)
    // {
    //     var allScores = stageDataSystem?.GetAllDatas();
    //     if (allScores != null)
    //     {
    //         string json = JsonConvert.SerializeObject(allScores);
    //         PlayerPrefs.SetString(gameName, json);
    //         PlayerPrefs.Save();
    //     }
    // }
    //
    // public void LoadData(string gameName)
    // {
    //     if (PlayerPrefs.HasKey(gameName))
    //     {
    //         string json = PlayerPrefs.GetString(gameName);
    //         var loadedScores = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
    //         foreach (var kvp in loadedScores)
    //         {
    //             stageDataSystem?.SetData(kvp.Key, kvp.Value);
    //         }
    //     }
    // }
    //

//     public static GameDataManager Instance { get; private set; }
//     private string savePath;
//     public PlayerData playerData { get; private set; }
//
//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//             savePath = Application.persistentDataPath + "/player_data.json";
//             LoadGame();
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }
//
//     public void SaveGame()
//     {
//         string json = JsonConvert.SerializeObject(playerData, Formatting.Indented);
//         File.WriteAllText(savePath, json);
//         Debug.Log("Game Saved!");
//     }
//
//     public void LoadGame()
//     {
//         if (File.Exists(savePath))
//         {
//             string json = File.ReadAllText(savePath);
//             playerData = JsonConvert.DeserializeObject<PlayerData>(json);
//         }
//         else
//         {
//             playerData = new PlayerData();
//         }
//
//         Debug.Log("Game Loaded!");
//     }
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