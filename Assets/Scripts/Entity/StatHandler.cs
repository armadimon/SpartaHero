using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    [Range(1, 100)][SerializeField] private int health = 10;
    public int Health  //체력
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }

    [Range(1f, 20f)][SerializeField] private float speed = 3;

    public float Speed //속도
    {
        get => speed;
        set => speed = Mathf.Clamp(value, 0, 20);
    }

}
