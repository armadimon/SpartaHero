using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class RangeWeaponHander : WeaponHandler
{
    [Header("Ranged Attack Data")]
    [SerializeField] private Transform projectileSpawnPosition;

    [SerializeField] private int bulletIndez;

    public int BulletIndex {  get { return bulletIndez; } }

    [SerializeField] private float bulletSize = 1f; //ź ����

    public float BulletSize { get { return bulletSize; } } //ź ũ��

    [SerializeField] private float duration;
    public float Duration { get { return duration; } } //ź ��ȿ�ð�

    [SerializeField] private float spread; // ź�� �����ݰ�

    public float Spread { get { return spread; } }

    [SerializeField] private int numberofprojectilesPerShot; //���簳��
    public int NumberofprojectilesPerShot { get { return numberofprojectilesPerShot; } }

    [SerializeField] private float multipleProjectileAngle; // �� ź�� �����ݰ�
    public float MultipleProjectileAngle { get { return multipleProjectileAngle; } }

    [SerializeField] private Color projectileColor; // ź ����

    public Color ProjectileColor { get { return projectileColor; } }


    public override void Attack()
    {
        base.Attack();
        float projectileAngleSpace = multipleProjectileAngle;
        int numberOfProjectilePerShot = numberofprojectilesPerShot;

        float minAlge = -(numberOfProjectilePerShot / 2f) * projectileAngleSpace;

        for (int i = 0; i < numberOfProjectilePerShot; i++)
        {
            float angle = minAlge + projectileAngleSpace * i;
            float randomSpread = Random.Range(-spread, spread);
            angle =+ randomSpread;
            CreateProjectile(Controller.LookDirection, angle);
        }
    }

    private void CreateProjectile(Vector3 _lookDirection, float angle)
    {

    }


    private static Vector2 RotateVector2(Vector2 v, float degree)
    {
        return Quaternion.Euler(0, 0, degree) * v;
    }


}
