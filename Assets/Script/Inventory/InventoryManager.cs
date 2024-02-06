using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public Dictionary<string, ItemHub> ItemHubDict;

    public void OnSave()
    {
        SaveManager.Instance.SaveData<Dictionary<string, ItemHub>>("StoreHouse", ItemHubDict);
    }
    public void OnLoad()
    {
        ItemHubDict = SaveManager.Instance.LoadData<Dictionary<string, ItemHub>>("StoreHouse");
    }
    public ItemHub GetStoreHouse(string HubID)
    {
        return ItemHubDict[HubID];
    }
    public void PushItemToHub(ItemData item, string HubID)
    {
        //ItemHubDict[HubID]
    }
}
