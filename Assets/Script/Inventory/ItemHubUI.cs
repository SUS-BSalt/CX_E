using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHubUI : MonoBehaviour
{
    public ItemHub Hub;
    public GameObject MissionBar;
    public GameObject MatterBar;
    public GameObject SpeechBar;
    public void LinkToHub(ItemHub _hub)
    {
        Hub = _hub;
        foreach(ItemObjBase item in Hub.itemList)
        {
            if (item.GetType() == typeof(MissionItem))
            {
                
            }
        }
    }
    public void CreateItemOnScreen(ItemObjBase item)
    {
        if (item.GetType() == typeof(MissionItem))
        {

        }
    }
}
