using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private SpriteRenderer button;

    public GameObject obObject;

    public Sprite redButtonDown;
    public Sprite redButtonUp;


    private void Start()
    {
        button = GetComponent<SpriteRenderer>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        AchievementManager.Instance.UnlockAchievement("TutorialMaster");
        obObject.SetActive(false);
        StartCoroutine("OperateButton");
    }


    private IEnumerator OperateButton()  
    {
        button.sprite = redButtonDown;
        yield return new WaitForSeconds(1f);
        button.sprite = redButtonUp;
    }
}
