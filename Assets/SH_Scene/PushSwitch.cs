using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushSwitch : MonoBehaviour
{
    private SpriteRenderer button;

    public Sprite redButtonDown;
    public Sprite redButtonUp;


    private void Start()
    {
        button = GetComponent<SpriteRenderer>();
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && collision.CompareTag("Player"))
        {
            player.SwapWeapon(this.gameObject.tag);
            StartCoroutine("OperateButton");
        }
        
    }

    private IEnumerator OperateButton()     // 2�� �� ���� ���·�
    {
        button.sprite = redButtonDown;
        yield return new WaitForSeconds(1f);
        button.sprite = redButtonUp;
    }
}
