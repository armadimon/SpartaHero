using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StageManager : MonoBehaviour
{
    GameManager gameManager;
    DoorController doorController;

    private bool isClear = false;       // temp?

    List<Stage> stages = new List<Stage>() {    // 스테이지 리스트 생성 (1 ~ 10)
        new Stage(1, 100, 100, 1, 3),
        new Stage(2, 100, 100, 2, 4),
        new Stage(3, 100, 100, 3, 5),
        new Stage(4, 100, 100, 4, 6),
        new Stage(5, 1000, 1000, 1, 2),
        new Stage(6, 100, 100, 4, 6),
        new Stage(7, 100, 100, 5, 7),
        new Stage(8, 100, 100, 6, 8),
        new Stage(9, 100, 100, 7, 9),
        new Stage(10, 1000, 1000, 1, 2),
        //     LEVEL, EXP, GOLD, MIN, MAX
    };


    private void Awake()
    {
        doorController = FindObjectOfType<DoorController>();
    }


    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }


    private void Start()
    {
        if (doorController == null)
            Debug.Log("DoorHandler null");
        else
            doorController.CloseDoor();
    }


    public Stage SetStageLevel(int diff)    // 게임 시작 전 스테이지 레벨 세팅
    {
        return stages[diff - 1];
    }


    public void StageClear(int diff)        // 스테이지 클리어 시
    {
        isClear = true;
        if(GameManager.Instance.uiManager.BossUi.gameObject.activeSelf == true)
        {
            GameManager.Instance.uiManager.BossUi.gameObject.SetActive(false);
        }

        doorController.OpenDoor();
        //player.gold += stageClearGold;    // 플레이어에게 클리어 골드 지급
        //player.exp += stageClearExp;      // 플레이어에게 클리어 경험치 지급
    }


    public void NextStage()     // 씬 재로드
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

