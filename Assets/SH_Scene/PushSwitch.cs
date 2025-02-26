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
        if (collision.tag != "Player") return;

        StartCoroutine("OperateButton");
    }


    private IEnumerator OperateButton()     // 2초 후 원래 상태로
    {
        button.sprite = redButtonDown;
        yield return new WaitForSeconds(1f);
        button.sprite = redButtonUp;
    }
}
