using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryOpen : MonoBehaviour
{
    public InventoryUI inventoryUI;
    public string InventroyID;
    public void OnOpen()
    {
        inventoryUI.LinkToInventory(InventoryManager.Instance.GetInventory(InventroyID));
    }
}
