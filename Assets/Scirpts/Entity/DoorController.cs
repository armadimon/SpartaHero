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
    EnemyManager enemyManager;

    public GameObject openedDoor;
    public GameObject closedDoor;

    public AudioClip openClip;


    public void Init(StageManager stageManager)
    {
        this.stageManager = stageManager;
    }

    private void Awake()
    {
        stageManager = FindObjectOfType<StageManager>();
        enemyManager = FindObjectOfType<EnemyManager>();
    }
    private void Start()
    {
        if(enemyManager.WhatMap == 0)
        {
            transform.position = new Vector3(0f,4.94f,0f);
        }
        else if (enemyManager.WhatMap == 1)
        {
            transform.position = new Vector3(0f, 24f, 0f);
        }
        else if(enemyManager.WhatMap == 2)
        {
            transform.position = new Vector3(27f, 10.94f, 0f);
        }
        else if (enemyManager.WhatMap == 3)
        {
            transform.position = new Vector3(0f, 17f, 0f);
        }
    }



    public void OpenDoor()    // �� Ȱ��ȭ
    {
        closedDoor.SetActive(false);
        SoundManager.PlayClip(openClip);
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
