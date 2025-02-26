using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera _camera;
    private GameManager _gameManager;
    public Collider2D[] Enemy;
    
    public Transform targeting;
    public float scanRange;

    public LayerMask targetLayer;

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
}
