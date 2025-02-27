using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    // 미리 정의된 업적 목록
    private List<Achievement> allAchievements = new List<Achievement>();

    [System.Serializable]
    public class Achievement
    {
        public string Name;
        public string Description;
        public string ImagePath;
        public string Condition;
        public bool IsUnlocked;

        public Achievement(string name, string description, string imagePath)
        {
            Name = name;
            Description = description;
            ImagePath = imagePath;
            IsUnlocked = false;  // 초기에는 잠김 상태
        }
    }

    void Start()
    {
        DefineAchievements();
        LoadAchievements();
    }

    void DefineAchievements()
    {
        allAchievements.Add(new Achievement("BossKill", "Kill your first Boss", "Achievements/BossKill"));
        allAchievements.Add(new Achievement("RichMan", "Collect 1000 gold", "Achievements/RichMan"));
        allAchievements.Add(new Achievement("TutorialMaster", "You passed the tutorial!", "Achievements/TutorialMaster"));
    }

    void LoadAchievements()
    {
        var savedAchievements = GameDataManager.Instance.GetAchievements();
        foreach (var achievement in allAchievements)
        {
            achievement.IsUnlocked = savedAchievements.Contains(achievement.Name);  // PlayerPrefs에 저장된 정보로 업적 상태 설정
        }
    }

    public void UnlockAchievement(string achievementName)
    {
        var achievement = allAchievements.Find(a => a.Name == achievementName);
        if (achievement != null && !achievement.IsUnlocked)
        {
            achievement.IsUnlocked = true;
            GameDataManager.Instance.AddAchievement(achievementName);  // GameDataManager에 업적 추가
        }
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

    // 업적의 Condition을 처리하는 로직
    bool CheckCondition(Achievement achievement)
    {
        switch (achievement.Condition)
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
        // 플레이어가 1000 골드를 모았는지 확인하는 로직
        return PlayerHasCollectedGold();
    }

    bool PlayerHasFirstBossKill()
    {
        // 실제 게임에서 플레이어의 첫 번째 킬을 추적하는 코드
        return true;  // 예시로 true 리턴
    }

    bool PlayerHasCollectedGold()
    {
        // 실제 게임에서 플레이어의 골드 보유량을 추적하는 코드
        return true;  // 예시로 true 리턴
    }

    bool PlayerHasTutorialPass()
    {
        return true;
    }

}
