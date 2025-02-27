using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnBtn : MonoBehaviour
{
    public void ReturnMainLobby()
    {
        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null)
            Destroy(player.gameObject);
        SceneManager.LoadScene("MainLobbyScene1");
    }
}
