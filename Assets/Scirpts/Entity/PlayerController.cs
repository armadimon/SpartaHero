using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : BaseController
{
    private Camera _camera;
    private GameManager _gameManager;

    [SerializeField] private WeaponHandler Bow;
    [SerializeField] private WeaponHandler Sword;
    [SerializeField] private WeaponHandler Staff;
    [SerializeField] private WeaponHandler Spear;

    public void Init(GameManager gameManager)
    {
        _gameManager = gameManager;
        _camera = Camera.main;
    }
    
    protected override void HandleAction()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;
        
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPos = _camera.ScreenToWorldPoint(mousePos); //마우스 해상도 좌표를 월드 좌표로
        lookDirection = (worldPos - (Vector2)transform.position);

        if (lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
        
        isAttacking = Input.GetMouseButton(0);
    }

    public override void Death()
    {
        base.Death();
        _gameManager.GameOver();
    }


    // 활 장착
    public void ChoiceBow()
    {
        if (weaponHandler != null)
            Destroy(weaponHandler.gameObject);

        weaponHandler = Instantiate(Bow, weaponPivot);
    }
    // 칼 장착
    public void ChoiceSword()
    {
        if (weaponHandler != null)
            Destroy(weaponHandler.gameObject);

        weaponHandler = Instantiate(Sword, weaponPivot);
    }
    // 지팡이 장착
    public void ChoiceStaff()
    {
        if (weaponHandler != null)
            Destroy(weaponHandler.gameObject);

        weaponHandler = Instantiate(Staff, weaponPivot);
    }
    // 창 장착
    public void ChoiceSpear()
    {
        if (weaponHandler != null)
            Destroy(weaponHandler.gameObject);

        weaponHandler = Instantiate(Spear, weaponPivot);
    }
}
