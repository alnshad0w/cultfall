using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class InventoryData
{
    public List<int> boughtItems = new List<int>();
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<FoodItem> Inventory = new List<FoodItem>();
    public List<FoodItem> AllItems;

    [SerializeField] private bool isLevelOne = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadInventory();
        }

    }

    public void AddItem(FoodItem item)
    {
        Inventory.Add(item);
        SaveInventory();
    }

    public void RemoveItem(FoodItem item)
    {
        FoodItem itemToRemove = Inventory.Find(i => i.ID == item.ID);
        if (itemToRemove != null)
        {
            Inventory.Remove(itemToRemove);
            SaveInventory(); 
        }
    }

    public void SaveInventory()
    {
        InventoryData data = new InventoryData();

        foreach (var item in Inventory)
        {
            data.boughtItems.Add(item.ID);
        }

        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Inventory", json);
        Debug.Log(json);

        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        Inventory.Clear();

        if (PlayerPrefs.HasKey("Inventory"))
        {
            if (isLevelOne)
            {
                PlayerPrefs.DeleteKey("Inventory");
            }
            else
            {
                string json = PlayerPrefs.GetString("Inventory");
                InventoryData data = JsonUtility.FromJson<InventoryData>(json);

                Debug.Log(json);

                foreach (int id in data.boughtItems)
                {
                    FoodItem found = AllItems.Find(x => x.ID == id);
                    if (found != null)
                        Inventory.Add(found);
                }
            }
        } 

    }
}
