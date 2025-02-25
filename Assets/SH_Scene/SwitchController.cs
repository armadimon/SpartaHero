using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    // 1. 밑의 버튼이 눌리면 플레이어의 무기 프리팹을 바꿔야 됨
    // 2. 활이면 활, 칼이면 칼
    // 3. 버튼이 눌린 스프라이트로도 바꿔야 됨
    // 4. 플레이어 컨트롤러 받아와야 됨

    public GameObject bow;
    public GameObject sword;
    public GameObject bowButton;
    public GameObject swordButton;
    public PlayerController player;

    PlayerController playerController;


    // init? 만약 없어도 기능 잘 되면 빼기
    //public void Init(PlayerController playerController)
    //{
    //    this.playerController = playerController;
    //}


    private void Start()
    {
        player = GetComponent<PlayerController>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.WeaponPrefab.name == "P_Bow_EquipWeapon")
        {
            //player.WeaponPrefab = weapon
                //player.WeaponPrefab.name;
        }
    }





    // 만약 활버튼과 플레이어가 트리거 충돌했다면 → 버튼이 눌리고 플레이어 컨트롤러의 웨폰 프리팹이 활로 바뀜
    // 만약 스워드버튼과 플레이어가 트리거 충돌했다면 → 버튼이 눌리고 플레이어 컨트롤러의 웨폰 프리팹이 스워드로 바뀜




}
