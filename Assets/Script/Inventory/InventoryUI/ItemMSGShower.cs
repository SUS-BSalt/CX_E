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
        inventoryUI.SlotOnSelect +=UpdateView;
        inventoryUI.SlotOnUnselect += CleanView;
        inventoryUI2.SlotOnSelect += UpdateView;
        inventoryUI2.SlotOnUnselect += CleanView;
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
