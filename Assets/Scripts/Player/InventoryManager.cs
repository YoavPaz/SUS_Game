using System;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class InventoryItem
{
    public string DisplayName;
    public string ImagePath;
    public int Count;
}

[System.Serializable]
public class Inventory
{
    public List<InventoryItem> Items;
}

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    [SerializeField]
    public Inventory inventory = new Inventory();

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(InventoryItem item)
    {
        PlayerManager.instance.playerSaveData.inventoryManager.Items.Add(item);
        SaveManager.Save(PlayerManager.instance.playerSaveData);
    }

    public void RemoveItem(InventoryItem item)
    {
        PlayerManager.instance.playerSaveData.inventoryManager.Items.Remove(item);
        SaveManager.Save(PlayerManager.instance.playerSaveData);
    }
}