using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    // 1. ���� ��ư�� ������ �÷��̾��� ���� �������� �ٲ�� ��
    // 2. Ȱ�̸� Ȱ, Į�̸� Į
    // 3. ��ư�� ���� ��������Ʈ�ε� �ٲ�� ��
    // 4. �÷��̾� ��Ʈ�ѷ� �޾ƿ;� ��

    public GameObject bow;
    public GameObject sword;
    public GameObject bowButton;
    public GameObject swordButton;
    public PlayerController player;

    PlayerController playerController;


    // init? ���� ��� ��� �� �Ǹ� ����
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





    // ���� Ȱ��ư�� �÷��̾ Ʈ���� �浹�ߴٸ� �� ��ư�� ������ �÷��̾� ��Ʈ�ѷ��� ���� �������� Ȱ�� �ٲ�
    // ���� �������ư�� �÷��̾ Ʈ���� �浹�ߴٸ� �� ��ư�� ������ �÷��̾� ��Ʈ�ѷ��� ���� �������� ������� �ٲ�




}
