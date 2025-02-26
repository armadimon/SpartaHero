using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    private EnemyManager enemyManager;
    private ItemController itemController;
    private Transform target;
    ResourceController resourceController;
    WeaponHandler weaponHandler;
    StatHandler statHandler;

    public void Init(ItemController itemController, Transform target)
    {
        this.itemController = itemController;
        this.target = target;
    }

    private void Start()
    {
        resourceController = gameObject.GetComponent<ResourceController>();
        weaponHandler = gameObject.GetComponent<WeaponHandler>();
        statHandler = gameObject.GetComponent<StatHandler>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GameObject.FindObjectOfType<PlayerController>())
            Destroy(gameObject);

        string Name = this.gameObject.name;

            switch (Name)
            {
                case "HealingPotion(Clone)":
                    UseheaHealingPotion();
                    break;
                case "PowerUpPotion(Clone)":
                    UsePowerUpPotion();
                    break;
                case "SpeedpPotion(Clone)":
                    UseSpeedPotion();
                break;
                default:
                    break;
            }
    }

    private void UseheaHealingPotion()
    {
        Debug.Log("ü�� ȸ��");
        statHandler.Health += 2;
    }

    private IEnumerator UsePowerUpPotion()
    {
        Debug.Log("���ݷ� ����");
        float Power = weaponHandler.AttackRange;
        weaponHandler.Power += 10f;
        Debug.Log($"���� ���ݷ� : {weaponHandler.AttackRange}");
        yield return new WaitForSecondsRealtime(3f); // 3�� ���
        weaponHandler.Power = Power;
        Debug.Log($"�ʱ�ȭ ���ݷ� : {weaponHandler.AttackRange}");
    }
    private IEnumerator UseSpeedPotion()
    {
        Debug.Log("�ӵ� ����");
        float speed = statHandler.Speed;
        statHandler.Speed += 10f;
        Debug.Log($"���� �ӵ� : {statHandler.Speed}");
        yield return new WaitForSecondsRealtime(3f); // 3�� ���
        statHandler.Speed = speed;
        Debug.Log($"�ʱ�ȭ ���ݷ� : {statHandler.Speed}");
    }


}
