using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class HUD : MonoBehaviour
{
    public enum Uitype { EXP, HealthBar}
    public Uitype uitype;

    private Slider Slider;


    private void Start()
    {
        Slider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (uitype)
        {
            case Uitype.HealthBar:

                float MaxHP = GetComponentInParent<StatHandler>().Health;
                float CurrentHp = GetComponentInParent<ResourceController>().CurrentHealth;
                Slider.value = CurrentHp/MaxHP;
                break;
            case Uitype.EXP:
                int level = GameManager.Instance.Level;
                float MaxExp = GameManager.Instance.NeedExp[level];
                float current = GameManager.Instance.CurrentExp;


                if( current >= MaxExp)
                {
                    current -= MaxExp;
                }
                Slider.value = current / MaxExp;
                Slider.fillRect.gameObject.SetActive(Slider.value > 0);
                break;
                


        }

    }

}
