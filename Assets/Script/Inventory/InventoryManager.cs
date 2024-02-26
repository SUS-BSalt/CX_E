using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Dictionary<string, InventoryDataClass> InventoryData;
    public void OnSave()
    {
        SaveManager.Instance.SaveData<Dictionary<string, InventoryDataClass>>("Inventory", InventoryData);
    }
    public void OnLoad()
    {
        InventoryData = SaveManager.Instance.LoadData<Dictionary<string, InventoryDataClass>>("Inventory");
    }
    public InventoryDataClass GetData(string InventoryID)
    {
        if (!InventoryData.ContainsKey(InventoryID))
        {

            InventoryData.Add(InventoryID, new(InventoryID));

        }
        return InventoryData[InventoryID];
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
}
