using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSet : MonoBehaviour
{
    public ItemType type;
    public string name;
    public string description;
    public int price;
    public Sprite Image;

    public Action Action;

    public ItemSet(ItemType type, string name, string description, int price, Sprite image, Action action = null)
    {
        this.type = type;
        this.name = name;
        this.price = price;
        this.description = description;
        Image = image;
        Action = action;
    }
}