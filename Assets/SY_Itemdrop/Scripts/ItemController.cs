using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
                    Debug.Log("공격력 증가");
                    player.GetComponentInChildren<WeaponHandler>().Speed += 3f;
                    Invoke("PowerUpPotion", 3); // 3초 후 시작

                break;
                case "SpeedpPotion(Clone)":
                    Debug.Log("속도 증가");
                    player.GetComponent<StatHandler>().Speed += 3f;
                    Invoke("SpeedPotion",3); // 3초 후 시작
                break;
                default:
                    break;
            }


    }
    private void HealingPotion()
    {
        Debug.Log("체력 회복");
        player.GetComponent<StatHandler>().Health += 5;
        Destroy(gameObject);
    }

    private void PowerUpPotion()
    {
        Debug.Log("공격력 감소");
        player.GetComponentInChildren<WeaponHandler>().Speed -= 3f;
        Destroy(gameObject);
    }
    private void SpeedPotion()
    {
        Debug.Log("속도 감소");
        player.GetComponent<StatHandler>().Speed -= 3f;
        Destroy(gameObject);
    }



}
