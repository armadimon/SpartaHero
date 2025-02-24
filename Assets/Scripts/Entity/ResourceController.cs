using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{

    [SerializeField] private float healthChangeDelay = .5f;

    private BaseController baseController;
    private StatHandler statHandler;
    private AnimationHanadler animationHanadler;

    private float timeSinceLastChange = float.MaxValue;


    public float CurrentHealth { get; private set; }

    public float MaxHealth => statHandler.Health;


    private void Awake()
    {
        baseController = GetComponent<BaseController>();
        statHandler = GetComponent<StatHandler>();
        animationHanadler = GetComponent<AnimationHanadler>();
    }

    private void Start()
    {
        CurrentHealth = statHandler.Health;
    }


    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHanadler.InvincibilityEnd();
            }
        }
    }


    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if(change < 0)
        {
            animationHanadler.Damage();
        }

        if(CurrentHealth <= 0F)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {

    }
}
