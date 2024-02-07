using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHubUI : MonoBehaviour
{
    public ItemHub Hub;
    public GameObject MissionBar;
    public GameObject MatterBar;
    public GameObject SpeechBar;

    public ItemObjBase currentSelect;

    private void Start()
    {
        if(Hub == null)
        {
            print("没连接到hub！");
        }
        Hub.HubChanged.AddListener(UpdateUI);
    }
    public void SelectItem(ItemObjBase item)
    {
        currentSelect = item;
    }
    public void PutCurrentSelectToOtherHub(ItemHubUI otherHub)
    {
        if (currentSelect == null) return;
        otherHub.Hub.PushItemToHub(currentSelect.data);
        Hub.DestroyItemFromHub(currentSelect.data);
        Destroy(currentSelect.gameObject);
        currentSelect = null;
    }
    public void LinkToHub(ItemHub _hub)
    {
        Hub = _hub;
        foreach(ItemData item in Hub.itemList)
        {
            GameObject gameObject;
            switch(item.ItemType)
            {
                case "Mission":
                    {
                        gameObject = InstanceUIObj("");
                        gameObject.transform.SetParent(MissionBar.transform);
                        gameObject.GetComponent<MissionItem>().InitObj(item);
                        return;
                    }
                case "Matter":
                    {
                        gameObject = InstanceUIObj("");
                        gameObject.transform.SetParent(MatterBar.transform);
                        return;
                    }
                case "Speech":
                    {
                        gameObject = InstanceUIObj("");
                        gameObject.transform.SetParent(SpeechBar.transform);
                        return;
                    }
            }
        }
    }
    public void UpdateUI()
    {
        ClearUI();
        LinkToHub(Hub);
    }
    public void UpdateUI(string ID)
    {
        UpdateUI();
    }
    public void ClearUI()
    {
        foreach(Transform t in MissionBar.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in MatterBar.transform)
        {
            Destroy(t.gameObject);
        }
        foreach (Transform t in SpeechBar.transform)
        {
            Destroy(t.gameObject);
        }
    }

    public GameObject InstanceUIObj(string PrefabPath)
    {
        GameObject prefab = Resources.Load<GameObject>(PrefabPath);
        GameObject gameObject = Instantiate(prefab);
        return gameObject;
    }

}
