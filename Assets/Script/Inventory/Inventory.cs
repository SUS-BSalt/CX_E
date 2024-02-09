using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
    public void AddItem(ItemBase item,int number)
    {
        int left = number;
        foreach (ItemSlot slot in slots)
        {
            left = slot.StackingItem(item, left);
            if (left == 0)
            {
                break;
            }
        }
    }
}
