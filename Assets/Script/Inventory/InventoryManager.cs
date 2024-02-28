using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

/// <summary>
/// 一坨屎，别看，因为看了折寿
/// </summary>
public class InventoryManager : Singleton<InventoryManager>
{
    public Dictionary<string, InventoryDataClass> InventoryData;
    public Dictionary<string, Inventory> Inventorys;
    
    protected override void Awake()
    {
        base.Awake();
        InventoryData = new();
        Inventorys = new();
    }

    public void OnSave()
    {
        foreach(Inventory i in Inventorys.Values)
        {
            i.OnSave();
            SetData(i.data);
        }
        SaveManager.Instance.SaveData<Dictionary<string, InventoryDataClass>>("InventoryData", InventoryData);
    }
    public void OnLoad()
    {
        Inventorys.Clear();
        InventoryData = SaveManager.Instance.LoadData<Dictionary<string, InventoryDataClass>>("InventoryData");
        foreach(InventoryDataClass i in InventoryData.Values)
        {
            Inventory inventory = new();
            inventory.data = i;
            inventory.OnLoad();
            Inventorys.Add(inventory.InventoryID, inventory);
        }
    }
    public InventoryDataClass GetData(string InventoryID)
    {
        if (!InventoryData.ContainsKey(InventoryID))
        {

            InventoryData.Add(InventoryID, new(InventoryID));

        }
        return InventoryData[InventoryID];
    }
    public Inventory GetInventory(string InventoryID)
    {
        if (!InventoryData.ContainsKey(InventoryID))
        {
            Inventory inventory = new();
            inventory.data = GetData(InventoryID);
            inventory.OnLoad();
            Inventorys.Add(inventory.InventoryID, inventory);
        }
        return Inventorys[InventoryID];
    }
    public void SetData(InventoryDataClass data)
    {
        if (!InventoryData.ContainsKey(data.InventoryID))
        {
            InventoryData.Add(data.InventoryID, data);
        }
        else
        {
            InventoryData[data.InventoryID] = data;
        }
    }
    public void AddItemToInventory(string InventoryID,int ItemID,string ItemProfileString, int number)
    {
        var item = ItemFactory.CreateItem(ItemID, ItemProfileString);
        Inventorys[InventoryID].AddItemWithAddSlotAuto(item, number);
    }
    public int RequestItemFromInventory(string InventoryID, int ItemID, string ItemProfileString, int number)
    {
        var item = ItemFactory.CreateItem(ItemID, ItemProfileString);
        return Inventorys[InventoryID].RequestItem(item, number);
    }
}
