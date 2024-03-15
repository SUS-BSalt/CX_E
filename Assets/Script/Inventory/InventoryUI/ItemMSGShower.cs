using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMSGShower : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public InventoryUI inventoryUI2;
    public Text ItemName;
    public Text ItemDescribe;
    public Text ItemValueDescribe;
    public Text ItemLeftTime;


    private void Start()
    {
        if (inventoryUI != null)
        {
            inventoryUI.SlotOnSelect += UpdateView;
            inventoryUI.SlotOnClick += UpdateView;
            inventoryUI.SlotOnUnselect += CleanView;
        }
        if (inventoryUI2 != null)
        {
            inventoryUI2.SlotOnSelect += UpdateView;
            inventoryUI2.SlotOnClick += UpdateView;
            inventoryUI2.SlotOnUnselect += CleanView;
        }

    }
    public void UpdateView(ItemSlotUI slotUI)
    {
        var _MSG = slotUI.slot.item.MSG;
        ItemName.text = _MSG.ItemName;
        ItemDescribe.text = _MSG.ItemDescribe;
        ItemValueDescribe.text = _MSG.ItemValueDescribe;
        ItemLeftTime.text = _MSG.ItemLeftTime;
    }
    public void CleanView()
    {
        ItemName.text = "";
        ItemDescribe.text = "";
        ItemValueDescribe.text = "";
        ItemLeftTime.text = "";
    }
}
