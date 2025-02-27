using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNpc : MonoBehaviour
{
    [SerializeField] private GameObject ShopPanel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShopPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShopPanel.SetActive(false);
        }
    }
}
