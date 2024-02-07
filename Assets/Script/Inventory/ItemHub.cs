using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemHub
{
    public List<ItemData> itemList;
    string HubID;
    public UnityEvent<string> HubChanged;
    public void PushItemToHub(ItemData item)
    {
        if (item != null)
        {
            itemList.Add(item);
            HubChanged?.Invoke(HubID);
        }
    }
    public void DestroyItemFromHub(ItemData item)
    {
        itemList.Remove(item);
    }
    public bool CheckItemExist(ItemData item)
    {
        return itemList.Contains(item);
    }

}
