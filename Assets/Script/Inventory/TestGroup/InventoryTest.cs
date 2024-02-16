using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Inventory inventory;
    public InventoryUI ui;

    public void AddSlot()
    {
        inventory.AddSlot(ItemSlot.SlotType.mission);
    }
    public void AddItem(string MissionName)
    {
        print(inventory.AddItem(new TestMissionItem(MissionName, "", 5), 1));
    }

    public void GetItem(string MissionName)
    {
        print(inventory.RequestItem(new TestMissionItem(MissionName, "", 5), 1));
    }
    public void RemoveCleanSlot()
    {
        inventory.RemoveCleanSlot();
    }
    public void t2(string MissionName)
    {
        ui.LinkToInventory(inventory);
    }
    public void t3()
    {
        
    }
    public void t4()
    {

    }
    public void t1()
    {
        TestItem item = new("{}");
        TestItem _item = new("{}");
        print(item.AreTheySame(_item));
    }
}
