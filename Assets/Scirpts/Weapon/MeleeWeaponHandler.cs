using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Data")]
    public Vector2 collideBoxSize = Vector2.one;

    protected void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();
        
        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x,
            collideBoxSize, 0, Vector2.zero, 0);
        
        if (hit.collider != null)
        {
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-Power);
                if (IsOnKnockBack)
                {
                    BaseController controller = hit.collider.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockBack(transform, KnockBackPower, KnockBackTime);
                    }
                }
            }

            bool isParry = false;
            if (ActiveSkills.TryGetValue(ActiveSkill.Parrying, out isParry))
            {
                if (isParry == true)
                {
                    ProjectileController projectileController = hit.collider.GetComponent<ProjectileController>();
                    if (projectileController != null)
                    {
                        projectileController.nowTargetLayer= (1 << LayerMask.NameToLayer("Enemy") | 1 << LayerMask.NameToLayer("Level"));
                        projectileController.direction = -projectileController.direction;
                    }
                }
            }
        }
    }

    private Coroutine parryCoroutine = null;
    public void StartParryCoroutine()
    {
        if (!ActiveSkills.ContainsKey(ActiveSkill.Parrying))
        {
            ActiveSkills[ActiveSkill.Parrying] = true;
        }

        if (parryCoroutine == null)
        {
            parryCoroutine = StartCoroutine(ParryCycleCoroutine());
        }
    }
    
    IEnumerator ParryCycleCoroutine()
    {
        while (true)
        {
            ActiveSkills[ActiveSkill.Parrying] = true;
            Debug.Log("패링 활성화 (5초)");
            yield return new WaitForSeconds(5f);

            ActiveSkills[ActiveSkill.Parrying] = false;
            Debug.Log("패링 비활성화 (5초)");
            yield return new WaitForSeconds(5f);
        }
    }
    
    
    public override void Rotate(bool isLeft)
    {
        if (isLeft)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }
}
