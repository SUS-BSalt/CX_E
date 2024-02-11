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
    public void AddItem()
    {
        inventory.AddItem(new TestItem(""), 1);

    }
    public void t2()
    {

    }
    public void t1()
    {
        TestItem item = new("{}");
        TestItem _item = new("{}");
        print(item.AreTheySame(_item));
    }
}
