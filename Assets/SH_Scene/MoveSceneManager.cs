using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveSceneManager : MonoBehaviour
{
    public static MoveSceneManager instance;

    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button settingButton;

    [SerializeField] private GameObject titleText;
    [SerializeField] private GameObject settingPanel;

    public AudioClip lobbyBGM;
    public AudioClip stageBGM;

    private bool isStageScene = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


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

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "StageScene" && isStageScene == false)
        {
            SoundManager.instance.ChangeBackGroundMusic(stageBGM);
            isStageScene = true;
        }
    }


    void OnClickStartButton()
    {
        SceneManager.LoadScene("Tutorial");
        SoundManager.instance.ChangeBackGroundMusic(lobbyBGM);
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

}
