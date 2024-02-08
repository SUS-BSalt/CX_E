using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对Inventory来说，slot实际上是“空间”概念的具象，Inventory需要slot的概念来规定空间的安排，而它不应该做更多事了，slot的位置之类的信息该是由UI来负责的部分
/// </summary>
public class Inventory : MonoBehaviour
{
    public List<ItemSlot> slots;
    public void AddSlot(ItemSlot.SlotType type)
    {
        slots.Add(new ItemSlot(type));
    }
    public void AddItem(ItemBase item)
    {
        foreach(ItemSlot slot in slots)
        {

        }
    }
}
