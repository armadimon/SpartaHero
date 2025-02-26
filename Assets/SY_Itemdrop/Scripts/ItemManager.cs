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
        Debug.Log("체력 회복");
        player.GetComponent<StatHandler>().Health += 5;
    }

    public IEnumerator UsePowerUpPotion()
    {
        Debug.Log("공격력 증가");
        yield return new WaitForSeconds(3f); // 3초 대기
    }
    public IEnumerator UseSpeedPotion()
    {
        Debug.Log("속도 증가");
        player.GetComponent<StatHandler>().Speed += 3f;

        yield return new WaitForSeconds(3f); // 3초 대기

        Debug.Log("속도 감소");
        player.GetComponent<StatHandler>().Speed -= 3f;

    }
}
