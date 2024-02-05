using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHub
{
    public List<ItemObjBase> itemList;
    string HubID;
    public void PushItemToHub(ItemObjBase item)
    {
        if (item != null)
        {
            itemList.Add(item);
        }
    }
    public void CheckItemExist(ItemObjBase item)
    {

    }

}
