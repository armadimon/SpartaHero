using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform content;
    public PlayerController player;
    private static InventoryManager instance;
    public static InventoryManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        
    }

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (player == null)
        {
            Debug.LogError("Player object not found!");
        }
        List<string> inventory = GameDataManager.Instance.Inventory;
        List<ItemSet> Items = ItemDatabase.Instance.GetAllItems();
        Debug.Log($"인벤토리에 확인 : {inventory}");
        foreach (var itemName in inventory)
        {
            ItemSet item = Items.Find(i => i.name == itemName);
            if (item != null)
            { 
                AddItem(item);
                Debug.Log($"인벤토리에 추가됨: {item.name}");
            }
            else
            {
                Debug.LogWarning($"아이템을 찾을 수 없음: {itemName}");
            }
        }
        GameDataManager.Instance.SaveInventory();
    }

    public void AddItem(ItemSet itemData)
    {
        GameObject newItem = Instantiate(itemPrefab, content); // 아이템 추가

        // UI 업데이트 (아이콘 & 이름)
        newItem.transform.GetComponentInChildren<Image>().sprite = itemData.Image;
        newItem.transform.GetComponentInChildren<Text>().text = itemData.name;
        if (itemData.type == ItemType.Weapon)
        {
            itemData.OnClick = () => player.SwapWeapon(itemData.name);
        }

        if (itemData.type == ItemType.Cosmetic)
        {
            
            itemData.OnClick = () => player.ChangeCharacter(itemData.name);
        }
        
        if (itemData.type == ItemType.Default)
        {
            
            itemData.OnClick = () => player.ChangeCharacter(itemData.name);
        }

        // 버튼 클릭 이벤트 추가
        Button button = newItem.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => itemData.OnClick?.Invoke());
    }
    
    public void LoadItem(ItemSet itemData)
    {
        GameObject newItem = Instantiate(itemPrefab, content); // 아이템 추가

        // UI 업데이트 (아이콘 & 이름)
        newItem.transform.GetComponentInChildren<Image>().sprite = itemData.Image;
        newItem.transform.GetComponentInChildren<Text>().text = itemData.name;
        if (itemData.type == ItemType.Weapon)
        {
            itemData.OnClick = () => player.SwapWeapon(itemData.name);
        }

        if (itemData.type == ItemType.Cosmetic)
        {
            
            itemData.OnClick = () => player.ChangeCharacter(itemData.name);
        }

        // 버튼 클릭 이벤트 추가
        Button button = newItem.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => itemData.OnClick?.Invoke());
    }
}
