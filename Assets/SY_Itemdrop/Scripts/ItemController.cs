using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Newtonsoft.Json.Linq;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private PlayerController player;
    private ItemController itemController;
    private Transform target;

    ItemManager itemManager;

    public void Init(ItemController itemController, Transform target)
    {
        this.itemController = itemController;
        this.target = target;
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player)
            gameObject.SetActive(false);


        string Name = this.gameObject.name;

            switch (Name)
            {
                case "HealingPotion(Clone)":
                    HealingPotion();
                break;
                case "PowerUpPotion(Clone)":
                    Debug.Log("���ݷ� ����");
                    player.GetComponentInChildren<WeaponHandler>().Speed += 3f;
                    Invoke("PowerUpPotion", 3); // 3�� �� ����

                break;
                case "SpeedpPotion(Clone)":
                    Debug.Log("�ӵ� ����");
                    player.GetComponent<StatHandler>().Speed += 3f;
                    Invoke("SpeedPotion",3); // 3�� �� ����
                break;
                default:
                    break;
            }


    }
    private void HealingPotion()
    {
        Debug.Log("ü�� ȸ��");
        player.GetComponent<StatHandler>().Health += 30;
        Destroy(gameObject);
    }

    private void PowerUpPotion()
    {
        Debug.Log("���ݷ� ����");
        player.GetComponentInChildren<WeaponHandler>().Speed -= 3f;
        Destroy(gameObject);
    }
    private void SpeedPotion()
    {
        Debug.Log("�ӵ� ����");
        player.GetComponent<StatHandler>().Speed -= 3f;
        Destroy(gameObject);
    }



}
