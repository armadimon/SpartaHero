using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance { get; private set; }
    
    private Dictionary<string, GameObject> uiPanels = new Dictionary<string, GameObject>();
    public GameObject itemPrefab;
    
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

        if (uiPanels.ContainsKey(name))
        {
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

    public void ShowAchievement(string name)
    {
        
        StartCoroutine(ShowAchievementCoroutine(name, 3f));
    }
    
    IEnumerator ShowAchievementCoroutine(string name, float duration)
    {
        
        GameObject newItem = Instantiate(itemPrefab); // 아이템 추가
        AchievementManager.MyAchievement achieve = AchievementManager.Instance.GetAchievementByName(name);
        // UI 업데이트 (아이콘 & 이름)
        string imagePath = achieve.ImagePath;  // 예: "Images/BossKill"
        Sprite sprite = Resources.Load<Sprite>(imagePath); // Resources 폴더 내에서 이미지 로드

        if (sprite != null)
        {
            Button temp = newItem.transform.GetComponentInChildren<Button>();
            temp.image.sprite = sprite;
        }
        else
        {
            Debug.LogError("이미지를 로드할 수 없습니다: " + imagePath);
        }
        
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            newItem.transform.SetParent(canvas.transform, false);
        }
        else
        {
            Debug.LogError("Canvas not found in the scene!");
        }
        newItem.transform.GetComponentInChildren<Text>().text = achieve.Name;
        // 버튼 클릭 이벤트 추가/ 캔버스에 추가 (Canvas 컴포넌트를 찾고 그 자식으로 추가)
        
        RectTransform rectTransform = newItem.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(1, 0);  // 오른쪽 아래
        rectTransform.anchorMax = new Vector2(1, 0);  // 오른쪽 아래
        rectTransform.pivot = new Vector2(1, 0);     // 오른쪽 아래
        rectTransform.anchoredPosition = new Vector2(-10, 10);  // 적당한 여백
        
        yield return new WaitForSeconds(duration);

        // 페이드 아웃 애니메이션
        CanvasGroup canvasGroup = newItem.GetComponent<CanvasGroup>();
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
        Destroy(newItem);
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
