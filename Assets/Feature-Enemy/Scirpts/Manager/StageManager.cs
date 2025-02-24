using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum StageLevel
{
    one,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
    ten
}



public class StageManager : MonoBehaviour
{
    GameManager gameManager;
    DoorController doorController;

    

    private bool isClear = false;

    List<int> stageLevels = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };   // 스테이지 레벨

    //private List<EnemyController> activeEnemies = new List<EnemyController>(); // 현재 활성화된 적들 (EM에 있음)


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
            Debug.Log("DOORhandler x");
        else
            doorController.CloseDoor();
    }


    private void SetStageLevel()
    {
        // 1. 갖고 있는 난이도 값을 받아 계산하여 랜덤한 갯수와 적절한 강함을 가진 몬스터를 먼저 아래에 넣어주고 관리
        // 랜덤 몬스터, 스테이지 세팅

        //    if ()
        //    {

        //    }


        //    else if ()
        //    {
        //        // 보스 스테이지
        //    }

        //    switch 

    }


    private void Update()
    {
        // 2. active 몬스터가 다 사라지면 스테이지 매니저에 클리어에 관한 정보 전달. (적들 잡은 경험치 포함)
    }


    public void StageClear()
    {
        isClear = true;
        doorController.OpenDoor();     // 3. 스테이지 클리어 후 스테이지 전진할 문 활성화
        //player.exp += monsters.exp;
        // 아니면 스테이지 자체 exp를 구현? 이상하긴 한데 궁전은 스테이지를 다 깨야 exp가 플러스되니까 ㄱㅊ을 것 같기도
    }


    public void NextStage()
    {
        // 4. 문을 통과하면 임시로 만들어놓은 난이도 값을 많이 올려서 난이도 조절 기능이 작동하는지 테스트
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}

