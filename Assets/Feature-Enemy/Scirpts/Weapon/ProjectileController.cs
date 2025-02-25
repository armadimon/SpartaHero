using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask levelCollisionLayer;

    private RangeWeaponHandler rangeWeaponHandler;

    private float currentDuration;
    private Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    ProjectileManager projectileManager;
    private Vector2 reflectionVelocity;
    public bool fxOnDestroy = true;
    public int boundcount;

    bool isFirst = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
        rangeWeaponHandler = GetComponentInParent<RangeWeaponHandler>();

    }

    private void Start()
    {
        boundcount = rangeWeaponHandler.BoundCountt;
    }

    private void Update()
    {
        if (!isReady)
            return;

        currentDuration += Time.deltaTime;

        if (currentDuration > rangeWeaponHandler.Duration)
        {
            DestroyProjectile(transform.position, false);
        }

        if (isFirst)
            rigidbody.velocity = direction * rangeWeaponHandler.Speed;


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {

            if (boundcount > 0)
            {
                isFirst = false;
                Bounding();
                boundcount--;
            }
            else
            {
                DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f, fxOnDestroy);
            }


        }
        else if (rangeWeaponHandler.target.value == (rangeWeaponHandler.target.value | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();

            if (resourceController != null)
            {
                resourceController.ChangeHealth(-rangeWeaponHandler.Power);
                if (rangeWeaponHandler.IsOnKnockBack)
                {
                    BaseController controller = collision.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockBack(transform, rangeWeaponHandler.KnockBackPower, rangeWeaponHandler.KnockBackTime);
                    }
                }
            }
            Debug.Log($"bound{boundcount}");



            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);

        }
    }

    private void Bounding()
    {

        if (boundcount > 0 && !isFirst)
        {
            rigidbody.velocity = -rigidbody.velocity;
            spriteRenderer.flipY = !spriteRenderer.flipY;

        }


    }

    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler, ProjectileManager projectileManager)
    {
        this.projectileManager = projectileManager;
        rangeWeaponHandler = weaponHandler;

        this.direction = direction;
        currentDuration = 0;
        transform.localScale = Vector3.one * weaponHandler.BulletSize;
        spriteRenderer.color = weaponHandler.ProjectileColor;

        transform.right = this.direction;

        if (direction.x < 0)
            pivot.localRotation = Quaternion.Euler(180, 0, 0);
        else
            pivot.localRotation = Quaternion.Euler(0, 0, 0);

        isReady = true;
    }

    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            projectileManager.CreateImpactParticlesAtPosition(position, rangeWeaponHandler);
        }
        Destroy(this.gameObject);
    }
}
