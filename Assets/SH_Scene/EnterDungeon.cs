using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterDungeon : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnclickExitButton);
        }
        else return;
    }


    void OnClickStartButton()
    {
        SceneManager.LoadScene("MainLobbyScene");
    }

    void OnclickExitButton()
    {
        Application.Quit();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("StageScene");
    }
}
