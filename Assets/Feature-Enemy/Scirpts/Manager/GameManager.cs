using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    [SerializeField] private static int Difficulty = 0;

    public PlayerController player { get; private set; }
    private ResourceController _playerResourceController;

    [SerializeField] private static int currentWaveIndex = 0;

    private EnemyManager enemyManager;
    private StageManager stageManager;


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

        // DontDestroyOnLoad(this);
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
        enemyManager.StartWave(Difficulty);
    }

    public void EndOfWave()
    {
        stageManager.StageClear();
        //StartNextWave();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartGame();
        //}
    }
}
