using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance { get; private set; }
    private Dictionary<string, ItemSet> items = new Dictionary<string, ItemSet>();

    [SerializeField] private Sprite[] images;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadItems();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LoadItems()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "items.json");
        // path = path.Replace("/", "\\");
        Debug.Log("Loading items...");
        Debug.Log(path);
        if (File.Exists(path))
        {
            Debug.Log("exist");
            string json = File.ReadAllText(path);
            Debug.Log(json);
            List<ItemData> itemList = JsonConvert.DeserializeObject<List<ItemData>>(json);

            foreach (var item in itemList)
            {
                Debug.Log(item.name);
                items[item.name] = new ItemSet(item.type, item.name, item.description, item.price, images[item.imageIndex]);
            }
        }
    }

    public ItemSet GetItem(string name)
    {
        return items.ContainsKey(name) ? items[name] : null;
    }

    public List<ItemSet> GetAllItems()
    {
        return new List<ItemSet>(items.Values);
    }
}

// JSON을 읽기 위한 데이터 클래스
[System.Serializable]
public class ItemData
{
    public ItemType type;
    public string name;
    public string description;
    public int price;
    public int imageIndex;
}