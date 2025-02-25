
using Assets.Scirpts.Weapon;
using UnityEngine;

public class WeaponHandler : MonoBehaviour, IAttack
{
    [Header("Attack Info")]
    [SerializeField] private float delay = 1f;
    public float Delay { get => delay; set => delay = value; }

    [SerializeField] private float weaponSize = 1f;
    public float WeaponSize {get => weaponSize; set => weaponSize = value; }

    [SerializeField] private float power = 1f;
    public float Power { get => power; set => power = value; }
    
    [SerializeField] private float speed = 1f;
    public float Speed { get => speed; set => speed = value; }
    
    [SerializeField] private float attackRange = 1f;

    public float AttackRange
    {
        get => attackRange;
        set => attackRange = value;
    }

    [SerializeField] public int Id = 0;

    public LayerMask target;

    [Header("Knock Back Info")]
    [SerializeField] private bool isOnKnockBack = false;
    public bool IsOnKnockBack { get => isOnKnockBack; set => isOnKnockBack = value; }
    
    [SerializeField] private float knockBackPower = 0.1f;
    public float KnockBackPower { get => knockBackPower; set => knockBackPower = value; }
    
    [SerializeField] private float knockBackTime = 1f;
    public float KnockBackTime { get => knockBackTime; set => knockBackTime = value; }

    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    public BaseController Controller { get; private set; }
    
    private Animator animator;
    private SpriteRenderer weaponRenderer;

    protected void Awake()
    {
        Controller = GetComponentInParent<BaseController>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        animator.speed = 1.0f / delay;
        transform.localScale = Vector3.one * weaponSize;
    }

    protected virtual void Start()
    {
    }

    public virtual void Attack()
    {
        AttackAnimation();
    }

    public virtual void AttackAnimation()
    {
        animator.SetTrigger(IsAttack);
    }

    public virtual void Rotate(bool isLeft)
    {
        weaponRenderer.flipY = isLeft;
    }
}
