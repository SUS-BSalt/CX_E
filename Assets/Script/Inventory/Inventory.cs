using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Inventory��˵��slotʵ�����ǡ��ռ䡱����ľ���Inventory��Ҫslot�ĸ������涨�ռ�İ��ţ�������Ӧ�����������ˣ�slot��λ��֮�����Ϣ������UI������Ĳ���
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
