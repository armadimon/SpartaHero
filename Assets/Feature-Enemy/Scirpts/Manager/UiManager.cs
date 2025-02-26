using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject SKillSelect;
    public GameObject BossUi;
    public GameObject[] Prefabs;

    public static UiManager Instance;
    GameManager gameManager;


    private void Awake()
    {
        if(Instance == null) 
        {
            Instance = this;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }


    public void SkillsetActve()
    {
        SKillSelect.gameObject.SetActive(true);
    }
    public void BossUIActive()
    {
        BossUi.gameObject.SetActive(true);
    }
}
