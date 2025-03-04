using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManger;
    private Transform target;

    private ResourceController resourceController;
    private ItemManager itemManager;

    [SerializeField] private float followRange = 15f;

    private void Start()
    {
        resourceController = GetComponent<ResourceController>();
    }
    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManger = enemyManager;
        this.target = target;
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (weaponHandler == null || target == null)
        {
            if (!movementDirection.Equals(Vector2.zero))
                movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        isAttacking = false;

        if (distance < followRange)
        {
            lookDirection = direction;

            if (distance < weaponHandler.AttackRange)
            {
                // layerMask를 통한 충돌 구분
                int layerMaskTarget = weaponHandler.target;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,
                    weaponHandler.AttackRange * 1.5f,
                    (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);
                    // Ray를 Scene 뷰에서 시각화
                    Debug.DrawRay(transform.position, direction);    
                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                }
                movementDirection = Vector2.zero;
                return;
            }
            else
            {
                
            }
            movementDirection = direction;
        }
    }

    public override void Death()
    {
        base.Death();
        if(enemyManger != null) 
        enemyManger.RemoveEnemyOnDeath(this);
    }
}
