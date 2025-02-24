using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Transform weaponPivot;
    [SerializeField] private WeaponHandler Bow;
    [SerializeField] private WeaponHandler Sword;
    [SerializeField] private WeaponHandler Staff;
    [SerializeField] private WeaponHandler Spear;

    protected Vector2 movementDirection = Vector2.zero;

    public Vector2 MovementDirection { get { return movementDirection; } }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get { return lookDirection; } }

    private Vector2 knockBack = Vector2.zero;
    private float knockBackDuration = 0.0f;

    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;

    protected void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();

        weaponHandler = Instantiate(Bow, weaponPivot);

    }

    // 무기 장착
    public void ChoiceBow()
    {
        if(weaponHandler != null)
            Destroy(weaponHandler.gameObject);

        weaponHandler = Instantiate(Bow, weaponPivot);
    }
    // 무기 장착
    public void ChoiceSword()
    {
        if (weaponHandler != null)
            Destroy(weaponHandler.gameObject);

        weaponHandler = Instantiate(Sword, weaponPivot);
    }
    // 무기 장착
    public void ChoiceStaff()
    {
        if (weaponHandler != null)
            Destroy(weaponHandler.gameObject);

        weaponHandler = Instantiate(Staff, weaponPivot);
    }
    // 무기 장착
    public void ChoiceSpear()
    {
        if (weaponHandler != null)
            Destroy(weaponHandler.gameObject);

        weaponHandler = Instantiate(Spear, weaponPivot);
    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();

    }
    protected virtual void Start()
    {

    }

    protected void FixedUpdate()
    {
        Movement(movementDirection);
        if (knockBackDuration > 0.0f)
        {
            knockBackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }

    private void Movement(Vector2 direction)
    {
        direction = direction * statHandler.Speed;
        // 넉백을 적용해야된다면 기존 이동방향의 힘은 없애고 넉백방향으로 힘을 준다.
        if (knockBackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockBack;
        }

        _rigidbody2D.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bool isLeft = Mathf.Abs(rotZ) > 90f;

        _spriteRenderer.flipX = isLeft;

        if (weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        weaponHandler?.Rotate(isLeft);
    }

    public void ApplyKnockBack(Transform other, float power, float duration)
    {
        knockBackDuration = duration;
        knockBack = (other.position - transform.position).normalized * power;


    }

    private void HandleAttackDelay()
    {
        if (weaponHandler == null)
            return;
        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {
        if (lookDirection != Vector2.zero)
            weaponHandler?.Attack();
    }

    public virtual void Death()
    {
        _rigidbody2D.velocity = Vector2.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }
}

