using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : BaseController
{
    private Camera _camera;
    private GameManager _gameManager;
    public Collider2D[] Enemy;
    public Transform targeting;
    public float scanRange;
    public LayerMask targetLayer;
    private Follow follow;
    private Animator animator;
    public GameObject gameOverPanel;
    public Button retryButton;
    public Button mainLobbyButton;


    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        _camera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        
        DontDestroyOnLoad(gameObject);
        for (int i = 0; i < transform.GetChild(3).transform.childCount-1; i++)
        {
            follow = transform.GetChild(3).transform.GetChild(i).GetComponent<Follow>();
            follow.SetTarget(transform);
        }
        ChangeCharacter(GameDataManager.Instance.LoadGameData("Character", "Player"));
    }
    
    protected override void HandleAction()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        
        // Vector2 mousePos = Input.mousePosition;
        // Vector2 worldPos = _camera.ScreenToWorldPoint(mousePos); //마우스 해상도 좌표를 월드 좌표로

        Enemy = Physics2D.OverlapCircleAll(transform.position, scanRange, targetLayer);
        targeting = GetNearest();
        if (targeting == null)
            lookDirection = Vector2.zero;
        else
        {
            lookDirection = ((Vector2)targeting.position - (Vector2)transform.position);
            isAttacking = true;
        }

        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }

    public Transform GetNearest() //사정거리내 에너미 리스트
    {
        Transform result = null;
        float diff = 50;
        foreach (Collider2D target in Enemy)
        {
            Vector3 mypos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(mypos, targetPos);
            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }

    public override void Death()
    {
        _gameManager.GameOver();
        
        base.Death();
    }

    private void OnDestroy()
    {
        gameOverPanel.SetActive(true);
        if (gameOverPanel)
        {
            retryButton.onClick.AddListener(OnClickRetryButton);
            mainLobbyButton.onClick.AddListener(OnClickLobbyButton);
        }
    }

    public void SwapWeapon(string weaponName)
    {
        bool isSwapped = false;
        GameObject tmp = null;
        if (weaponHandler != null)
             tmp = weaponHandler.gameObject;
        WeaponPrefab = Resources.Load<WeaponHandler>(weaponName);
        if (WeaponPrefab != null)
        {
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
            isSwapped = true;
        }
        if (tmp != null && isSwapped)
        {
            Destroy(tmp.gameObject);
        }
        else
            return;
    }
    
    public void ChangeCharacter(string name)
    {
        if (animator == null)
        {
            return;
        }
        AnimatorOverrideController overrideController = Resources.Load<AnimatorOverrideController>("Animation/" + name);
        
        if (overrideController != null)
        {
            animator.runtimeAnimatorController = overrideController;
            GameDataManager.Instance.SaveGameData("Character", name);
        }
        else
        {
            Debug.LogError("Animator Override Controller Error.");
        }
    }

    

    void OnClickRetryButton()
    {
        SceneManager.LoadScene("StageScene");
        gameOverPanel.SetActive(false);
    }

    void OnClickLobbyButton()
    {
        SceneManager.LoadScene("MainLobbyScene");
        gameOverPanel.SetActive(false);
    }

}
