using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareCrow : MonoBehaviour
{
    
    public GameObject GameObject;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject.SetActive(false);
    }
}
