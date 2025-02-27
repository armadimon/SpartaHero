using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EnterDungeon : MonoBehaviour
{
    SoundManager soundManager;
    
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button settingButton;
    
    [SerializeField] private GameObject settingPanel;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            startButton.onClick.AddListener(OnClickStartButton);
            exitButton.onClick.AddListener(OnclickExitButton);
            settingButton.onClick.AddListener(OnClickSettingButton);
        }
        else return;
    }


    void OnClickStartButton()
    {
        SceneManager.LoadScene("TestScene");
    }

    void OnclickExitButton()
    {
        Application.Quit();
    }

    void OnClickSettingButton()
    {
        bool isActive = settingPanel.activeSelf;
        settingPanel.SetActive(!isActive);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SceneManager.LoadScene("StageScene");
    }
}
