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

                float MaxHP = GameManager.Instance.player.GetComponent<StatHandler>().Health;
                float CurrentHp = GameManager.Instance.player.GetComponent<ResourceController>().CurrentHealth;
                Slider.value = CurrentHp/MaxHP;
                break;

        }

    }

}
