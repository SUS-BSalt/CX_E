using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    public Inventory inventory;
    public void AddSlot()
    {
        inventory.AddSlot(ItemSlot.SlotType.allType);
    }
    public void AddItem(int number)
    {
        print(inventory.AddItem(new TestItem(""), number));
    }

    public void GetItem(int number)
    {
        print(inventory.RequestItem(new TestItem(""), number));
    }
    public void t2(string MissionName)
    {
        print(inventory.AddItem(new TestMissionItem(MissionName,"",5),1));
    }
    public void t3()
    {

    }
    public void t1()
    {
        TestItem item = new("{}");
        TestItem _item = new("{}");
        print(item.AreTheySame(_item));
    }
}
