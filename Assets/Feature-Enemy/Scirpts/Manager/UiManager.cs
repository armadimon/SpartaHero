using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject GameObject;


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


    public void setActve()
    {
        GameObject.gameObject.SetActive(true);
    }
}
