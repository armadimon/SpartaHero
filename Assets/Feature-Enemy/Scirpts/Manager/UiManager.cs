using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }
    
    private Dictionary<string, GameObject> uiPanels = new Dictionary<string, GameObject>();

    // Start is called before the first frame update

    [SerializeField] private GameObject SKillSelect;
    public GameObject BossUi;
    public GameObject[] Prefabs;

    GameManager gameManager;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPanel(string name, GameObject panel)
    {
        if (!uiPanels.ContainsKey(name))
        {
            Debug.Log(name + " is not registered yet");
            uiPanels.Add(name, panel);
        }
    }


    public void ShowPanel(string name)
    {
        // foreach (var panel in uiPanels.Values)
        // {
        //     panel.SetActive(false);
        // }

        Debug.Log(name);
        if (uiPanels.ContainsKey(name))
        {
            Debug.Log(name);
            uiPanels[name].SetActive(true);
        }
    }
    public void ShowToast(string name, string message, float duration)
    {
        StartCoroutine(ShowToastCoroutine(name, message, duration));
    }

    IEnumerator ShowToastCoroutine(string name, string message, float duration)
    {
        uiPanels[name].SetActive(true);
        uiPanels[name].GetComponentInChildren<Text>().text = message;
        yield return new WaitForSeconds(duration);

        // 페이드 아웃 애니메이션
        CanvasGroup canvasGroup = uiPanels[name].GetComponent<CanvasGroup>();
        if (canvasGroup != null)
        {
            float fadeDuration = 0.5f;
            float startTime = Time.time;

            while (Time.time < startTime + fadeDuration)
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, (Time.time - startTime) / fadeDuration);
                yield return null;
            }
        }

        uiPanels[name].SetActive(false);
    }
    

    public void HidePanel(string name)
    {
        if (uiPanels.ContainsKey(name))
        {
            uiPanels[name].SetActive(false);
        }
    }
    
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        uiPanels.Clear();
    }

    public void BossUIActive()
    {
        BossUi.gameObject.SetActive(true);

    }
}


// public class UiManager : MonoBehaviour
// {
//     // Start is called before the first frame update
//
//     [SerializeField]
//     private GameObject GameObject;
//
//
//     public static UiManager Instance;
//     GameManager gameManager;
//
//
//     private void Awake()
//     {
//         if(Instance == null) 
//         {
//             Instance = this;
//
//         }
//         else
//         {
//             Destroy(this.gameObject);
//         }
//     }
//     public void Init(GameManager gameManager)
//     {
//         this.gameManager = gameManager;
//     }
//
//
//     public void setActve()
//     {
//         GameObject.gameObject.SetActive(true);
//     }
// }
