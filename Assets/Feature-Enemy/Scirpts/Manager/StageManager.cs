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

    List<int> stageLevels = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };   // �������� ����

    //private List<EnemyController> activeEnemies = new List<EnemyController>(); // ���� Ȱ��ȭ�� ���� (EM�� ����)


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
        // 1. ���� �ִ� ���̵� ���� �޾� ����Ͽ� ������ ������ ������ ������ ���� ���͸� ���� �Ʒ��� �־��ְ� ����
        // ���� ����, �������� ����

        //    if ()
        //    {

        //    }


        //    else if ()
        //    {
        //        // ���� ��������
        //    }

        //    switch 

    }


    private void Update()
    {
        // 2. active ���Ͱ� �� ������� �������� �Ŵ����� Ŭ��� ���� ���� ����. (���� ���� ����ġ ����)
    }


    public void StageClear()
    {
        isClear = true;
        doorController.OpenDoor();     // 3. �������� Ŭ���� �� �������� ������ �� Ȱ��ȭ
        //player.exp += monsters.exp;
        // �ƴϸ� �������� ��ü exp�� ����? �̻��ϱ� �ѵ� ������ ���������� �� ���� exp�� �÷����Ǵϱ� ������ �� ���⵵
    }


    public void NextStage()
    {
        // 4. ���� ����ϸ� �ӽ÷� �������� ���̵� ���� ���� �÷��� ���̵� ���� ����� �۵��ϴ��� �׽�Ʈ
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}

