using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    private Coroutine waveRoutine;
        
    [SerializeField]
    private List<GameObject> enemyPrefabs; // 생성할 적 프리팹 리스트
    [SerializeField]
    private List<GameObject> BossPrefabs; // 생성할 보스 프리팹 리스트
    [SerializeField]
    private List<GameObject> PosionPrefabs; // 생성할 보스 프리팹 리스트

    [SerializeField]
    private GameObject Level;
    [SerializeField]
    private List<GirdID> GirdIDs;

    [SerializeField]
    private List<Rect> spawnAreas; // 적을 생성할 영역 리스트

    [SerializeField]
    private Color gizmoColor = new Color(1, 0, 0, 0.3f); // 기즈모 색상

    [SerializeField]
    private List<EnemyController> activeEnemies = new List<EnemyController>(); // 현재 활성화된 적들
    
    [SerializeField]
    private List<BossController> activeBoss = new List<BossController>(); // 현재 활성화된 적들

    private bool enemySpawnComplete;

    public int WhatMap;
    
    [SerializeField] private float timeBetweenSpawns = 0.2f;
    [SerializeField] private float timeBetweenWaves = 1f;

    GameManager gameManager;
    PlayerController player;
    ItemController ItemController;

    private void Start()
    {
        ItemController = GetComponent<ItemController>();
        for(int i = 0; i< Level.transform.childCount; i++)
        {
            if(Level.transform.GetChild(i).GetComponent<GirdID>() != null)
            {
                GirdIDs.Add(Level.transform.GetChild(i).GetComponent<GirdID>());
            }
        }
    }


    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("No player found");
        }
    }
    public void StartWave(Stage stage)
    {
        int Rand = Random.Range(1, GirdIDs.Count);
        WhatMap = Rand;
        Debug.Log("Starting wave " + stage.Level);
        if (stage.Level <= 0)
        {
            gameManager.EndOfWave();
            return;
        }

        if (stage.Level != 5)
        {
            if(waveRoutine != null)
                StopCoroutine(waveRoutine);
            int randomNum = Random.Range(stage.MonstersMin, stage.MonstersMax);
            waveRoutine =  StartCoroutine(SpawnWave(randomNum, Rand));

            StopToMAp();
            GirdIDs[Rand].gameObject.SetActive(true);
            
        }
        else
        {
            StartBossStage();

            StopToMAp();
            GirdIDs[0].gameObject.SetActive(true);
            UIManager.Instance.ShowPanel("BossUI");

        }
    }

    public void StartBossStage()
    {

        // Rect 영역 내부의 랜덤 위치 계산
        Vector2 randomPosition = CreatSpawnPosition(0);
        GameObject randomBoss = BossPrefabs[Random.Range(0, BossPrefabs.Count)];
        GameObject spawnedBoss = Instantiate(randomBoss, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        BossController bossController = spawnedBoss.GetComponent<BossController>();
        bossController.Init(this, gameManager.player.transform);
        activeBoss.Add(bossController);
    }
    
    public void StopWave()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnWave(int waveCount, int Rand)
    {
        enemySpawnComplete = false;
        yield return new WaitForSeconds(timeBetweenWaves);
        for (int i = 0; i < waveCount; i++)
        {
            yield return new WaitForSeconds(timeBetweenSpawns); 
            SpawnRandomEnemy( Rand);
        }

        enemySpawnComplete = true;
    }

    private void SpawnRandomEnemy(int Rand)
    {
        if (enemyPrefabs.Count == 0 || spawnAreas.Count == 0)
        {
            Debug.LogWarning("Enemy Prefabs 또는 Spawn Areas가 설정되지 않았습니다.");
            return;
        }

        // 랜덤한 적 프리팹 선택
        GameObject randomPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        Vector2 randomPosition =CreatSpawnPosition(Rand);

        // 적 생성 및 리스트에 추가
        GameObject spawnedEnemy = Instantiate(randomPrefab, new Vector3(randomPosition.x, randomPosition.y), Quaternion.identity);
        EnemyController enemyController = spawnedEnemy.GetComponent<EnemyController>();

        GameObject Healthbar = Instantiate(UIManager.Instance.Prefabs[0]);
        Healthbar.transform.SetParent(spawnedEnemy.transform, false);

        Follow Health = Healthbar.GetComponentInChildren<Follow>();
        Health.SetTarget(spawnedEnemy.transform);

        enemyController.Init(this, gameManager.player.transform);
        activeEnemies.Add(enemyController);
    }

    // 기즈모를 그려 영역을 시각화 (선택된 경우에만 표시)
    private void OnDrawGizmosSelected()
    {
        if (spawnAreas == null) return;

        Gizmos.color = gizmoColor;
        foreach (var area in spawnAreas)
        {
            Vector3 center = new Vector3(area.x + area.width / 2, area.y + area.height / 2);
            Vector3 size = new Vector3(area.width, area.height);
            Gizmos.DrawCube(center, size);
        }
    }

    public void RemoveEnemyOnDeath(EnemyController enemy)
    {
        SpawnRandomItem(enemy);
        activeEnemies.Remove(enemy);
        StatHandler enemyStat = enemy.GetComponent<StatHandler>();
        ResourceController playerResource = player.GetComponent<ResourceController>();
        
        playerResource.GetExp(enemyStat.Exp);
        gameManager.stageManager.CurrentGold += enemyStat.Gold;
        GameDataManager.Instance.AddGold(enemyStat.Gold);
        if (enemySpawnComplete &&  activeEnemies.Count == 0)
            gameManager.EndOfWave();
    }
    
    
    public void RemoveBossOnDeath(BossController boss)
    {
        activeBoss.Remove(boss);
        
        StatHandler enemyStat = boss.GetComponent<StatHandler>();
        ResourceController playerResource = player.GetComponent<ResourceController>();
        
        // playerResource.GetExp(enemyStat.Exp);
        gameManager.stageManager.CurrentGold += enemyStat.Gold;
        GameDataManager.Instance.AddGold(enemyStat.Gold);
        if (activeBoss.Count == 0)
            gameManager.EndOfWave();
    }

    public void SpawnRandomItem(EnemyController enemy)
    {
        GameObject randomPrefab = PosionPrefabs[Random.Range(0, PosionPrefabs.Count)];
        Vector3 enemyDeathPosition = enemy.transform.position;


        //랜덤 생성
        bool Droprate = Random.Range(0, 100) > 30? false: true;
        if (Droprate)
        {
            //아이템 생성   
            GameObject spawnedPosion = Instantiate(randomPrefab, enemyDeathPosition, Quaternion.identity);
            ItemController itemController = spawnedPosion.GetComponent<ItemController>();
            itemController.Init(itemController, this.transform);
        }
    }


    public void StopToMAp()
    {
        foreach (var grid in GirdIDs)
        {
            grid.gameObject.SetActive(false);
        }
    }

    public Vector2 CreatSpawnPosition(int Ran)
    {
        switch (Ran)
        {
            case 0:
                spawnAreas = new List<Rect>
            {
                new Rect(-7.5f, -4.5f, 4f, 8f),
                new Rect(3.5f, -4.5f, 4f, 8f)
            };
                break;
            case 1:
                spawnAreas = new List<Rect>
            {
                new Rect(-6f, 13f, 12f, 3f),
                new Rect(-6f, 19f, 12f, 3f)
            };
                break;
            case 2:
                spawnAreas = new List<Rect>
            {
                new Rect(13f, -1f, 2f, 8f),
                new Rect(24f, -1f, 2f, 8f),
                new Rect(8f, 8.5f, 2f, 1f),
                new Rect(8f, -3.5f, 2f, 1f),
                new Rect(18.5f, -3.5f, 2f, 1f),
                new Rect(18.5f, 8.5f, 2f, 1f)
            };
                break;
            case 3:
                spawnAreas = new List<Rect>
            {
                new Rect(-10f, 13f, 21f, 1f),
                new Rect(-10f, 14f, 21f, 1f)
            };
                break;
            default:
                return new Vector2(1, 2);
        }

        Rect randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
        return new Vector2(
            Random.Range(randomArea.xMin, randomArea.xMax),
            Random.Range(randomArea.yMin, randomArea.yMax)
        );
    }
}