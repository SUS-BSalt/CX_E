using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void _SlotUnselect();
public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public List<ItemSlotUI> slotUI;

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

    public _ItemSlotOnSelect SlotOnSelect;
    public event _SlotUnselect SlotOnUnselect;
    public event _ItemSlotOnSelect SlotOnClick;
    public void LinkToInventory(Inventory _inventory)
    {
        if(inventory != null)
        {
            inventory.SlotListChanged -= UpdateUI;
        }
        inventory = _inventory;
        //print(inventory.SlotListChanged == null);
        inventory.SlotListChanged += UpdateUI;
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
        _slotUI.OnSelect +=SlotBeSelect;
        _slotUI.OnUnselect += SlotBeUnselect;
        _slotUI.OnClick += SlotBeClick;
        _slotUI.parent = this;
        _slotUI.LinkToSlot(slot);
        slotUI.Add(_slotUI);
    }
    public void SlotBeSelect(ItemSlotUI slotUI)
    {
        SlotOnSelect?.Invoke(slotUI);
       //print("Select here?");
    }
    public void SlotBeUnselect(ItemSlotUI slotUI)
    {
        SlotOnUnselect?.Invoke();
        //print("Unselect here?");
    }
    public void SlotBeClick(ItemSlotUI slotUI)
    {
        SlotOnClick?.Invoke(slotUI);
        //print("Click here?");
    }
}
