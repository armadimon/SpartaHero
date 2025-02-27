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
    Cosmetic
}

public class ShopManager : MonoBehaviour
{
        // Start is called before the first frame update


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
        
        ItemCreate();
        closeBtn.onClick.AddListener(Close);
        nextBtn.onClick.AddListener(NextList);
    }

    // Update is called once per frame

    private void OnEnable()
    {
        ShowItem();
    }
    public void NextList()
    {
        itemIndex++;
        Debug.Log("Next List : "  + itemIndex);
        if (itemIndex > Items.Count / 3)
        {
            itemIndex = 0;
        }
        ShowItem();
    }

    public void Close()
    {
        Parent.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ShowItem()
    {
        Debug.Log(itemIndex);
        
            for (int i = 0; i < 3; i++)
            {
                int index = (itemIndex * 3) + i;
                if (index <= Items.Count - 1)
                {
                    Buttons[i].onClick.AddListener(() => Items[itemIndex].Action());
                    Images[i].sprite = Items[index].Image;
                    ItemName[i].text = Items[index].name;
                    ItemDesc[i].text = Items[index].description;
                }
            }
    }

    private void ItemCreate()
    {
        Items.Add(new ItemSet(ItemType.Weapon,"Bow", "가격 : 50\n활입니다.", 50, images[0], () => GetItem("Bow", 50)));
        Items.Add(new ItemSet(ItemType.Weapon,"Sword", "가격 : 50\n칼입니다.", 50, images[1], () => GetItem("Sword", 50)));
        Items.Add(new ItemSet(ItemType.Weapon,"Spear", "가격 : 50\n창입니다.", 50, images[2], () => GetItem("Spear", 50)));
        Items.Add(new ItemSet(ItemType.Weapon,"Staff", "가격 : 50\n지팡이입니다.", 50, images[3], () => GetItem("Staff", 50)));
        Items.Add(new ItemSet(ItemType.Cosmetic,"Dwarf", "가격 : 500\n드워프가됩니다.", 500, images[4], () => GetItem("Dwarf", 500)));
        Items.Add(new ItemSet(ItemType.Cosmetic,"Default", "empty", 500, images[4], () => GetItem()));
    }
    private void GetItem()
    {
    }
    
    private void GetItem(string itemName, int Gold)
    {
        if (Gold > GameDataManager.Instance.GetGold())
        {
            UIManager.Instance.ShowToast("ToastMsg", "GOLD가 부족합니다!", 3);
            return;
        }
        if (GameDataManager.Instance.Inventory.Contains(itemName))
        {
            UIManager.Instance.ShowToast("ToastMsg", "You already have it!", 3);
        }
        else
        {
            GameDataManager.Instance.AddItemToInventory(itemName);
            GameDataManager.Instance.TakeGold(Gold);
        }
    }
    

}


