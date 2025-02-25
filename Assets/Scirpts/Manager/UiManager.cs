using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    UiManager uiManager;

    private void Start()
    {
        uiManager = GetComponentInChildren<UiManager>();
    }


    public void EqWeapon() 
    {
        uiManager.gameObject.SetActive(true);
    }
}
