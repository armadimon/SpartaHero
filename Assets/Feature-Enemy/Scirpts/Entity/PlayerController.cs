using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera _camera;
    private GameManager _gameManager;


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
}
