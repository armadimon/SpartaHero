using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    [SerializeField] private static int Difficulty = 0;

    public int[] NeedExp = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
    public int CurrentExp;
    public int Level = 0;

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private static int currentWaveIndex = 0;

    private EnemyManager enemyManager;
    private StageManager stageManager;
    private UiManager uiManager;
    private GameDataManager gameDataManager;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            
        }
        
        player = FindObjectOfType<PlayerController>();
        player.Init(this);

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);

        stageManager = GetComponentInChildren<StageManager>();
        stageManager.Init(this);

        uiManager = GetComponentInChildren<UiManager>();
        uiManager.Init(this);
        
    }


    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        StartNextWave();
    }

    void StartNextWave()
    {
        Difficulty++;
        Debug.Log("diff " + Difficulty);
        Stage stageLevel = stageManager.SetStageLevel(Difficulty);

        enemyManager.StartWave(stageLevel);
    }

    public void EndOfWave()
    {
        stageManager.StageClear(Difficulty);
        //StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
    }

    public void SkillSelectActive()
    {
        uiManager.setActve();
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
