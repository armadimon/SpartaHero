using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Door�� �޾��� ��ũ��Ʈ
    // 1. ó���� closeddoor�� Ȱ��ȭ
    // 2. ���� �� �׾��� �� closeddoor ��Ȱ��ȭ, opendoor Ȱ��ȭ
    // 3. opendoor���� �ݶ��̴� �߰�

    StageManager stageManager;

    public GameObject openedDoor;
    public GameObject closedDoor;


    public void Init(StageManager stageManager)
    {
        this.stageManager = stageManager;
    }

    private void Awake()
    {
        stageManager = FindObjectOfType<StageManager>();
    }


    
    public void OpenDoor()    // �� Ȱ��ȭ
    {
        closedDoor.SetActive(false);
        openedDoor.SetActive(true);
    }

    public void CloseDoor()    // �� �� Ȱ��ȭ
    {
        closedDoor.SetActive(true);
        openedDoor.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)     // ���� ��������. ���� �� Collider + �θ� ������ Rigidbody
    {
        
        if (stageManager == null)
            Debug.Log("stageManager null");
        stageManager.NextStage();
    }
}
