using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController_clone : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = 0.5f;

    private BaseController _baseController;
    private StatHandler _statHandler;
    private AnimationHandler _animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => _statHandler.Health;

    public AudioClip damageClip;        // 추가


    private void Awake()
    {
        _baseController = GetComponent<BaseController>();
        _statHandler = GetComponent<StatHandler>();
        _animationHandler = GetComponent<AnimationHandler>();
    }

    private void Start()
    {
        CurrentHealth = _statHandler.Health;
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                _animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;
        if (change < 0)
        {
            _animationHandler.Damage();

            if (damageClip != null)                         // 추가
                SoundManager.PlayClip(damageClip);          // 추가
        }

        if (CurrentHealth <= 0f)
        {
            Death();
        }
        return true;
    }

    public void GetExp(int exp)
    {
        _statHandler.Exp += exp;
    }

    private void Death()
    {
        _baseController.Death();
    }
}
