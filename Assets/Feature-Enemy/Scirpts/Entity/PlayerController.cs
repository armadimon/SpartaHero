using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera _camera;
    private GameManager _gameManager;
    public Collider2D[] Enemy;
    
    public Transform targeting;
    public float scanRange;

    public LayerMask targetLayer;


    private Follow follow;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        _camera = Camera.main;

        DontDestroyOnLoad(this.gameObject);

        for (int i = 0; i < transform.GetChild(3).transform.childCount; i++)
        {
            follow = transform.GetChild(3).transform.GetChild(i).GetComponent<Follow>();
            follow.SetTarget(transform);
        }

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
        base.Death();
        _gameManager.GameOver();
    }
    
    public void SwapWeapon(GameObject btn)
    {
        if (weaponHandler != null)
        {
            Destroy(weaponHandler.gameObject);
        }
        if (btn.tag == "BowBtn")
        {
            WeaponPrefab = Resources.Load<WeaponHandler>("P_Bow_EquipWeapon 1");
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        }
        else if (btn.tag == "SwordBtn")
        {
            WeaponPrefab = Resources.Load<WeaponHandler>("P_Sword_EquipWeapon 1");
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        }
        else if (btn.tag == "SpearBtn")
        {
            WeaponPrefab = Resources.Load<WeaponHandler>("P_Spear_EquipWeapon 1");
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        }
        else if (btn.tag == "StaffBtn")
        {
            WeaponPrefab = Resources.Load<WeaponHandler>("P_Staff_EquipWeapon 1");
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        }
        else return;
    }

}
