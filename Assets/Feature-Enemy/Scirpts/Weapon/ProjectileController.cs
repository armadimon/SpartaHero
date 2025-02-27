using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] public LayerMask levelCollisionLayer;

    public RangeWeaponHandler rangeWeaponHandler;
    public LayerMask nowTargetLayer;

    private float currentDuration;
    public Vector2 direction;
    private bool isReady;
    private Transform pivot;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;

    ProjectileManager projectileManager;
    private Vector2 reflectionVelocity;
    public bool fxOnDestroy = true;

    //Weapon Variable
    SkillHandler skillHandler;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0);
        rangeWeaponHandler = GetComponentInParent<RangeWeaponHandler>();
        skillHandler = GetComponent<SkillHandler>();

    }

    private void Start()
    {
        nowTargetLayer = rangeWeaponHandler.target.value;
        skillHandler.boundcount = rangeWeaponHandler.BoundCountt;
        skillHandler.Penetration = rangeWeaponHandler.Penetration;
        projectileManager = FindObjectOfType<ProjectileManager>();
        rangeWeaponHandler.Debuff.Add(skillHandler.IsSlow);
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

        if (skillHandler.isFirst)
            rigidbody.velocity = direction * rangeWeaponHandler.Speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (levelCollisionLayer.value == (levelCollisionLayer.value | (1 << collision.gameObject.layer)))
        {
            if (skillHandler.boundcount > 0)
            {
     
                //if Arrow hit the wall then Bounce
                skillHandler.isFirst = false;
                Vector2 closestPoint = collision.ClosestPoint(transform.position);
                Vector2 normalVector = (Vector2)transform.position - closestPoint;
                normalVector.Normalize();
                direction = Vector2.Reflect(direction, normalVector);
                rigidbody.velocity = direction * rangeWeaponHandler.Speed;
                float rotZ = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
                skillHandler.Bounding(rigidbody,spriteRenderer);
                skillHandler.boundcount--;
            }
            else
            {
                DestroyProjectile(collision.ClosestPoint(transform.position) - direction * 0.2f, fxOnDestroy);
            }
        }
        else if (nowTargetLayer == (nowTargetLayer | (1 << collision.gameObject.layer)))
        {
            ResourceController resourceController = collision.GetComponent<ResourceController>();
            if (rangeWeaponHandler.Debuff[0])
                skillHandler.StartSlow(collision);
            
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

            //if Penetration left dont Destroy
            if (skillHandler.Penetration <= 0)
            {
                DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestroy);
            }
            skillHandler.Penetration--;
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
