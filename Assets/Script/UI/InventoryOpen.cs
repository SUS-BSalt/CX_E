using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOpen : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public string InventroyID;

    public void InitUI()
    {
        inventoryUI.LinkToInventory(InventoryManager.Instance.GetInventory(InventroyID));
    }
    public void OnEnable()
    {
        //inventoryUI.TabUI.toggleGroup.SetAllTogglesOff(true);
        inventoryUI.TabUI.TabClicks[1].GetComponent<Toggle>().isOn = true;
        print("fuck");
    }
}
