using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance { get; private set; }

    public List<MyAchievement> allAchievements = new List<MyAchievement>();

    [System.Serializable]
    public class MyAchievement
    {
        public string Name;
        public string Description;
        public string ImagePath;
        public string Condition;
        public bool IsUnlocked;

        public MyAchievement(string name, string description, string imagePath)
        {
            Name = name;
            Description = description;
            ImagePath = imagePath;
            IsUnlocked = false;
        }
    }

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

        DefineAchievements();
    }

    private void Start()
    {
        LoadAchievements();
    }

    void DefineAchievements()
    {
        allAchievements.Add(new MyAchievement("BossKill", "Kill your first Boss", "Achievements/BossKill"));
        allAchievements.Add(new MyAchievement("RichMan", "Collect 1000 gold", "Achievements/RichMan"));
        allAchievements.Add(new MyAchievement("TutorialMaster", "You passed the tutorial!", "Achievements/TutorialMaster"));
    }

    void LoadAchievements()
    {
        var savedAchievements = GameDataManager.Instance.GetAchievements();
        if (savedAchievements == null)
            return;
        foreach (var achievement in allAchievements)
        {
            achievement.IsUnlocked = savedAchievements.Contains(achievement.Name);
        }
    }

    public void UnlockAchievement(string achievementName)
    {
        var achievement = allAchievements.Find(a => a.Name == achievementName);
        if (achievement != null && !achievement.IsUnlocked)
        {
            achievement.IsUnlocked = true;
            UIManager.Instance.ShowAchievement(achievementName);
            GameDataManager.Instance.AddAchievement(achievementName);
        }
    }
 
    public MyAchievement GetAchievementByName(string achievementName)
    {
        var achievement = allAchievements.Find(a => a.Name == achievementName);
        return achievement;
    }
    
    public bool IsAchievementUnlocked(string achievementName)
    {
        var achievement = allAchievements.Find(a => a.Name == achievementName);
        return achievement != null && achievement.IsUnlocked;
    }

    public void PrintAchievements()
    {
        foreach (var achievement in allAchievements)
        {
            Debug.Log($"{achievement.Name} - {(achievement.IsUnlocked ? "Unlocked" : "Locked")}");
        }
    }

    // 도전과제의 Condition을 처리하는 로직
    public bool CheckCondition(string condition)
    {
        switch (condition)
        {
            case "BossKill":
                return CheckFirstBossKill();
            case "RichMan":
                return CheckGoldCollection();
            case "TutorialMaster":
                return CheckTutorialMaster();
            default:
                return false;
        }
    }

    bool CheckTutorialMaster()
    {
        return PlayerHasTutorialPass();
    }
    bool CheckFirstBossKill()
    {
        // 플레이어가 첫 번째 적을 처치했는지 여부를 확인하는 로직
        return PlayerHasFirstBossKill();
    }

    bool CheckGoldCollection()
    {
        int currentGold = GameDataManager.Instance.GetGold();
        if (currentGold >= 10)
            return true;
        return false;
    }

    bool PlayerHasFirstBossKill()
    {
        // 실제 게임에서 플레이어의 첫 번째 킬을 추적하는 코드
        return true;  // 예시로 true 리턴
    }

    bool PlayerHasTutorialPass()
    {
        return true;
    }

}
