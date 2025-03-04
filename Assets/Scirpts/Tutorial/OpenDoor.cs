using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenDoor : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            GameDataManager.Instance.SaveGameData("IsTutorialCleared", true);
            SceneManager.LoadScene("MainLobbyScene");
        }
    }
}
