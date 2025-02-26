using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    private Dictionary<string, GameObject> uiPanels = new Dictionary<string, GameObject>();

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
            uiPanels.Add(name, panel);
        }
    }

    public void ShowPanel(string name)
    {
        foreach (var panel in uiPanels.Values)
        {
            panel.SetActive(false);
        }

        if (uiPanels.ContainsKey(name))
        {
            Debug.Log(name);
            uiPanels[name].SetActive(true);
        }
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
