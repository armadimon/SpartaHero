using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    [SerializeField] private static int Difficulty = 0;
    
    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private static int currentWaveIndex = 0;

    private EnemyManager enemyManager;
    public StageManager stageManager;
    private GameDataManager gameDataManager;
    public StageUI stageUI;



    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;

        }

        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        enemyManager = GetComponentInChildren<EnemyManager>();
        if (enemyManager != null)
            enemyManager.Init(this);

        stageManager = GetComponentInChildren<StageManager>();
        if (stageManager != null)
            stageManager.Init(this);

        stageUI = GetComponentInChildren<StageUI>();

        gameDataManager = FindObjectOfType<GameDataManager>();
    }


    private void Start()
    {
        if (stageUI != null)
            stageUI.Init();
        if (enemyManager != null)
            StartGame();
    }

    public void StartGame()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        Difficulty++;
        Stage stageLevel = stageManager.SetStageLevel(Difficulty);
        enemyManager.StartWave(stageLevel);
    }

    public void EndOfWave()
    {
        Debug.Log("DIff : "+ Difficulty);
        stageManager.StageClear(Difficulty);
        UIManager.Instance.ShowPanel("StageClear");
        if (Difficulty < 5)
            stageUI.SwitchingClearPanel();
        gameDataManager.GetStageClearNum();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
    }

    public void SkillSelectActive()
    {
        UIManager.Instance.ShowPanel("SkillSelectActive");
        Time.timeScale = 0f;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartGame();
        //}
    }
}