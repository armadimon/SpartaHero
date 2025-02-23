using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : BaseController
{
    private EnemyManager enemyManger;
    public Transform target;

    public GameObject attackRangeIndicator; // 타원 범위 표시 오브젝트 (AttackRangeIndicator) 연결
    public float attackRangeDisplayTime = 2.0f; // 공격 범위 표시 시간
    public float attackChargeTime = 1.5f; // 빨간색으로 채우는 시간
    public float attackDamageTime = 0.5f; // 점프 공격 애니메이션 및 공격 판정 시간
    public bool isCharge = false;

    private SpriteRenderer attackRangeRenderer;
    private Color initialColor; // 초기 색상 저장
    
    [SerializeField] private float followRange = 30f;

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }

    protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }
    
    public void Init(EnemyManager enemyManager, Transform target)
    { 
        attackRangeRenderer = attackRangeIndicator.GetComponent<SpriteRenderer>();
        initialColor = attackRangeRenderer.color; // 초기 색상 저장
        attackRangeIndicator.transform.localScale = Vector3.zero; // 초기에는 숨김
        this.enemyManger = enemyManager;
        this.target = target;
    }

    public void Start()
    {      
        attackRangeRenderer = attackRangeIndicator.GetComponent<SpriteRenderer>();
        initialColor = attackRangeRenderer.color; // 초기 색상 저장
        attackRangeIndicator.transform.localScale = Vector3.zero; // 초기에는 숨김

    }
    
    protected override void HandleAction()
    {
        base.HandleAction();

        if (target == null)
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
                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    isAttacking = true;
                    if (!isCharge)
                    {
                        isCharge = true;
                        StartJumpAttack();
                    }
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
    
    public void StartJumpAttack()
    {
        StartCoroutine(JumpAttackCoroutine());
    }

    IEnumerator JumpAttackCoroutine()
    {
        attackRangeIndicator.transform.position = target.position;
        // 1. 공격 범위 표시
        attackRangeIndicator.transform.localScale = new Vector3(20, 20 , 0);
        attackRangeRenderer.color = initialColor;

        yield return new WaitForSeconds(attackRangeDisplayTime);

        // 2. 빨간색으로 서서히 채우기
        yield return StartCoroutine(FillAttackRangeColor());

        // 3. 점프 공격 애니메이션 및 공격 판정 실행
        yield return StartCoroutine(ExecuteJumpAttack());

    }
    
    IEnumerator ExecuteJumpAttack()
    {
        
        // TODO: 공격 판정 실행 (OverlapArea, Raycast 등 활용)
        Vector2 direction = (target.position - transform.position);

        // 2. 거리 계산: this와 target 사이의 거리
        Debug.Log(direction.magnitude);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(attackRangeIndicator.transform.position, 1, LayerMask.GetMask("Player"));
        // RaycastHit2D hit = Physics2D.CircleCast(attackRangeIndicator.transform.position, 1f, direction.normalized, 0);
        
        if (hitColliders.Length > 0)
        {
            Debug.Log(hitColliders[0].gameObject.name);
            Debug.Log("점프 공격!"); // 공격 실행 로그 (임시)
        }
        yield return new WaitForSeconds(attackDamageTime); // 공격 애니메이션 및 판정 시간 대기

        // TODO: 공격 후 처리 (예: 보스 상태 변경, 쿨타임 시작 등)
        isCharge = false;
    }
    
    IEnumerator FillAttackRangeColor()
    {
        float timer = 0f;
        Color targetColor = Color.red;

        while (timer < attackChargeTime)
        {
            // TODO: 보스 점프 애니메이션 실행 (Animator 제어)
            
            timer += Time.deltaTime;
            float progress = timer / attackChargeTime;
            attackRangeRenderer.color = Color.Lerp(initialColor, targetColor, progress);
            yield return null;
        }
        attackRangeRenderer.color = targetColor;
        attackRangeIndicator.transform.localScale = Vector3.zero;
    }
    
    // 기즈모
    public Color gizmosColor = Color.green; // Gizmos 색상
    
    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor; // Gizmos 색상 설정

        Vector2 direction = (target.position - transform.position);

        float distance = direction.magnitude;
        // 1. 시작 지점 (transform.position) 에 반지름 크기의 원 그리기
        Gizmos.DrawWireSphere(transform.position, 1);

        // 2. 방향 벡터 (direction) 와 최대 거리 (distance) 를 나타내는 선 그리기
        Vector3 castDirection = transform.TransformDirection(direction.normalized);
        Gizmos.DrawRay(transform.position, castDirection * distance);

        // 3.  최대 거리 끝 지점에 반지름 크기의 원 그리기 (CircleCast의 최종 범위)
        Vector3 endPoint = transform.position + castDirection * distance;
        Gizmos.DrawWireSphere(endPoint, 1);

    }
    
    public override void Death()
    {
        base.Death();
        enemyManger.RemoveBossOnDeath(this);
    }
}
