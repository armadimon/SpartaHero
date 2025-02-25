
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;
    
    [SerializeField] private int bulletIndex;
    public int BulletIndex { get => bulletIndex; }

    [SerializeField] private float bulletSize = 1f;
    public float BulletSize { get => bulletSize; set => bulletSize = value; }

    [SerializeField] private float duration;
    public float Duration { get => duration; }
    
    [SerializeField] private float spread;
    public float Spread { get => spread; }
    
    [SerializeField] private int numberOfProjectilesPerShot;
    public int NumberOfProjectilesPerShot { get => numberOfProjectilesPerShot; set => numberOfProjectilesPerShot = value; }
    
    [SerializeField] private float multipleProjectilesAngle;
    public float MultipleProjectilesAngle { get => multipleProjectilesAngle; }

    [SerializeField] private int BoundCount = 100;
    public int BoundCountt { get => BoundCount; set => BoundCount = value; }

    [SerializeField] private int penetration = 1;
    public int Penetration { get => penetration; set => penetration = value; }

    [SerializeField] private Color projectileColor;
    public Color ProjectileColor { get => projectileColor; }
    
    private ProjectileManager projectileManager;
    
    // 스킬에 대해서 가지고 있음.
    protected override void Start()
    {
        base.Start();
        projectileManager = ProjectileManager.Instance;
    }

    public override void Attack()
    {
        base.Attack();
        float projectileAngleSpace = multipleProjectilesAngle;
        int numberofProjectilesPerShot = numberOfProjectilesPerShot;
        float minAngle = -(numberofProjectilesPerShot / 2) * projectileAngleSpace;

        for (int i = 0; i < numberofProjectilesPerShot; i++)
        {
            float angle = minAngle + (i * projectileAngleSpace);
            float randomSpread = Random.Range(-spread, spread);
            angle += randomSpread;
            CreateProjectile(Controller.LookDirection, angle);
        }
    }

    private void CreateProjectile(Vector2 lookDirection, float angle)
    {
        projectileManager.ShootBullet(
            this,
            projectileSpawnPosition.position,
            RotateVector2(lookDirection, angle)
            );
    }

    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }
}
