using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    public void UseHealingPotion()
    {
        Debug.Log("ü�� ȸ��");
        player.GetComponent<StatHandler>().Health += 5;
    }

    public IEnumerator UsePowerUpPotion()
    {
        Debug.Log("���ݷ� ����");
        yield return new WaitForSeconds(3f); // 3�� ���
    }
    public IEnumerator UseSpeedPotion()
    {
        Debug.Log("�ӵ� ����");
        player.GetComponent<StatHandler>().Speed += 3f;

        yield return new WaitForSeconds(3f); // 3�� ���

        Debug.Log("�ӵ� ����");
        player.GetComponent<StatHandler>().Speed -= 3f;

    }
}
