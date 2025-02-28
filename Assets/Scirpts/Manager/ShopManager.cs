using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Weapon,
    Cosmetic,
    Default
}

public class ShopManager : MonoBehaviour
{


    [SerializeField] private Sprite[] images;
    [SerializeField] private Button closeBtn;
    [SerializeField] private Button nextBtn;
    [SerializeField] private GameObject Parent;
    // public GameObject shopPanel;
    
    
    private int itemIndex = 0;
    private List<Button> Buttons = new List<Button>();
    private List<Image> Images = new List<Image>();
    private List<TextMeshProUGUI> ItemName = new List<TextMeshProUGUI>();
    private List<TextMeshProUGUI> ItemDesc = new List<TextMeshProUGUI>();

    private List<ItemSet> Items = new List<ItemSet>();

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            Buttons.Add(transform.GetChild(i).GetComponent<Button>());
            Images.Add(Buttons[i].transform.GetChild(0).GetComponent<Image>());
            ItemName.Add(Buttons[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>());
            ItemDesc.Add(Buttons[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>());
        }

        Items = ItemDatabase.Instance.GetAllItems();
        closeBtn.onClick.AddListener(Close);
        nextBtn.onClick.AddListener(NextList);
    }

    private void OnEnable()
    {
        ShowItem();
    }
    public void NextList()
    {
        itemIndex++;
        if (itemIndex >= Mathf.CeilToInt(Items.Count / 3f))
        {
            itemIndex = 0;
        }
        ShowItem();
    }

    public void Close()
    {
        Parent.SetActive(false);
    }

    // 현재 페이지에 맞게 보여준다. 아이템은 최대 3개.
    private void ShowItem()
    {
        foreach (var btn in Buttons)
        {
            btn.onClick.RemoveAllListeners();
        }
        for (int i = 0; i < 3; i++)
        {
            int index = (itemIndex * 3) + i;

            if (index < Items.Count)
            {
                Buttons[i].gameObject.SetActive(true);
                int itemIndexCopy = index;
                Buttons[i].onClick.AddListener(() => GetItem(Items[itemIndexCopy]));

                Images[i].sprite = Items[index].Image;
                ItemName[i].text = Items[index].name;
                ItemDesc[i].text = Items[index].description;
            }
            else
            {
                Buttons[i].gameObject.SetActive(false);
            }
        }
    }
    
    private void GetItem()
    {
    }
    
    private void GetItem(ItemSet item)
    {
        Debug.Log(item.name);
        if (item.price > GameDataManager.Instance.GetGold())
        {
            UIManager.Instance.ShowToast("ToastMsg", "GOLD가 부족합니다!", 3);
            return;
        }
        if (GameDataManager.Instance.Inventory.Contains(item.name))
        {
            UIManager.Instance.ShowToast("ToastMsg", "You already have it!", 3);
        }
        else
        {
            GameDataManager.Instance.AddItemToInventory(item.name);
            InventoryManager.Instance.AddItem(item);
            GameDataManager.Instance.TakeGold(item.price);
        }
    }
}


