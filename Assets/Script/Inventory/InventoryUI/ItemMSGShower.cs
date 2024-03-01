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
        inventoryUI.UIOnSelect.AddListener(UpdateView);
        inventoryUI2.UIOnSelect.AddListener(UpdateView);
    }
    public void UpdateView(GameObject gameObject)
    {
        var UI = gameObject.GetComponent<InventoryUI>();
        if (UI.currentSelectSlot.slot.item == null)
        {
            return;
        }
        var _MSG = UI.currentSelectSlot.slot.item.MSG;
        ItemName.text = _MSG.ItemName;
        ItemDescribe.text = _MSG.ItemDescribe;
        ItemValueDescribe.text = _MSG.ItemValueDescribe;
        ItemLeftTime.text = _MSG.ItemLeftTime;
    }
}
