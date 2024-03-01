using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public List<ItemSlotUI> slotUI;
    public ItemSlotUI currentSelectSlot;

    [Header("物品分类以及对应分类的slot预制体")]
    public GameObject missionBar;
    public GameObject missionSlot;
    [Space]
    public GameObject matterBar;
    public GameObject matterSlot;
    [Space]
    public GameObject informationBar;
    public GameObject informationSlot;
    [Space]
    public GameObject relationBar;
    public GameObject relationSlot;

    public UnityEvent<GameObject> UIOnSelect;
    public UnityEvent<GameObject> SelectedSlot;
    public void Awake()
    {
        UIOnSelect = new();
    }
    public void LinkToInventory(Inventory _inventory)
    {
        if(inventory != null)
        {
            inventory.SlotListChanged.RemoveListener(UpdateUI);
        }
        inventory = _inventory;
        //print(inventory.SlotListChanged == null);
        inventory.SlotListChanged.AddListener(UpdateUI);
        UpdateUI();
    }
    public void UpdateUI()
    {
        Clean();
        foreach (ItemSlot slot in inventory.slots)
        {
            //print(slot == null);
            AddSlotUI(slot);

        }
    }
    public void CleanSlotBar(GameObject bar)
    {
        Transform[] children = new Transform[bar.transform.childCount];
        for(int i = 0; i < bar.transform.childCount; i++)
        {
            children[i] = bar.transform.GetChild(i);
        }
        foreach(Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }
    public void Clean()
    {
        slotUI = new();
        CleanSlotBar(informationBar);
        CleanSlotBar(matterBar);
        CleanSlotBar(missionBar);
        CleanSlotBar(relationBar);
    }
    public void AddSlotUI(ItemSlot slot)
    {
        GameObject obj;
        switch (slot.type)
        {
            case (ItemSlot.SlotType.mission):
                {
                    obj = Instantiate(missionSlot, new Vector3(0, 0, 0), Quaternion.identity);
                    obj.transform.SetParent(missionBar.transform, false);
                    break;
                }
            case (ItemSlot.SlotType.matter):
                {
                    obj = Instantiate(matterSlot, new Vector3(0, 0, 0), Quaternion.identity);
                    obj.transform.SetParent(matterBar.transform, false);
                    break;
                }
            case (ItemSlot.SlotType.information):
                {
                    obj = Instantiate(informationSlot, new Vector3(0, 0, 0), Quaternion.identity);
                    obj.transform.SetParent(informationBar.transform, false);
                    break;
                }
            case (ItemSlot.SlotType.relation):
                {
                    obj = Instantiate(relationSlot, new Vector3(0, 0, 0), Quaternion.identity);
                    obj.transform.SetParent(relationBar.transform, false);
                    break;
                }
            default:
                {
                    obj = null;
                    break;
                }

        }

        var _slotUI = obj.GetComponent<ItemSlotUI>();
        _slotUI.OnSelect.AddListener(SlotBeSelect);
        _slotUI.LinkToSlot(slot);
        slotUI.Add(_slotUI);
    }
    public void SlotBeSelect(ItemSlotUI slotUI)
    {
        currentSelectSlot = slotUI;
        UIOnSelect?.Invoke(this.gameObject);
    }
}
