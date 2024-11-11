using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 一坨屎，别看，因为看了折寿
/// </summary>
public class InventoryManager : Singleton<InventoryManager>, IDataUser
{
    public Dictionary<string, Inventory> Inventorys;

    public T GetData<T>(params string[] argv)
    {
        throw new System.Exception("请用InventoryManager内建的函数查找物体");
    }
    public bool SetData<T>(T value, params string[] argv)
    {
        throw new System.Exception("请用InventoryManager内建的函数添加物体");
    }

    public string PackName => "Inventory";

    public bool IndividualizedSave => true;

    protected override void Awake()
    {
        base.Awake();
    }
    public void New()
    {
        Inventorys = new();
        Init();
    }
    public Inventory GetInventory(string InventoryID)
    {
        if (!Inventorys.ContainsKey(InventoryID))
        {
            Inventory inventory = new(InventoryID);
            Inventorys.Add(inventory.InventoryID, inventory);
        }
        return Inventorys[InventoryID];
    }

    public void AddItemToInventoryWithSlotAuto(string InventoryID, int ItemID, string ItemProfileString, int number)
    {
        var item = ItemFactory.CreateItem(ItemID, ItemProfileString);
        GetInventory(InventoryID).AddItemWithAddSlotAuto(item, number);
    }
    public int RequestItemFromInventoryWithSlotAuto(string InventoryID, int ItemID, string ItemProfileString, int number)
    {
        var item = ItemFactory.CreateItem(ItemID, ItemProfileString);
        int i = Inventorys[InventoryID].RequestItem(item, number);
        Inventorys[InventoryID].RemoveCleanSlot();
        return i;
    }

    public DataPack SerializedToDataPack()
    {
        foreach (Inventory i in Inventorys.Values)
        {
            i.OnSave();
        }
        DataPack newpack = new();
        newpack.PackName = "Inventory";
        newpack.DeserializeData = JsonConvert.SerializeObject(Inventorys,Formatting.Indented);
        return newpack;
    }
    public void Init()
    {
        try
        {
            DataManager.Instance.RegisterDataUser(this);
        }
        catch
        {

        }
        try
        {
            
            OnLoad();
            
        }
        catch
        {
            Inventorys = new();
        }
        
    }
    public void OnLoad()
    {
        string DeserializeData = DataManager.Instance.GetDataPack(PackName).DeserializeData;
        Inventorys = JsonConvert.DeserializeObject<Dictionary<string, Inventory>>(DeserializeData);
        foreach(Inventory i in Inventorys.Values)
        {
            i.OnLoad();
        }
    }
}
