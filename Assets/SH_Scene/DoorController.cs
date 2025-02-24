using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Door에 달아줄 스크립트
    // 1. 처음엔 closeddoor만 활성화
    // 2. 적이 다 죽었을 때 closeddoor 비활성화, opendoor 활성화
    // 3. opendoor에만 콜라이더 추가

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




    public void OpenDoor()    // 문 활성화
    {
        closedDoor.SetActive(false);
        openedDoor.SetActive(true);
    }

    public void CloseDoor()    // 문 비활성화
    {
        closedDoor.SetActive(true);
        openedDoor.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)     // 다음 스테이지. 오픈 문 Collider + 부모 옵젝에 Rigidbody
    {
        if (stageManager == null)
            Debug.Log("stageManager null");
        stageManager.NextStage();
    }
}
