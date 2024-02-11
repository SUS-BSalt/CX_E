using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 对Inventory来说，slot实际上是“空间”概念的具象，Inventory需要slot的概念来规定空间的安排，而它不应该做更多事了，slot如何显示，显示在哪之类的信息该是由UI来全权负责
/// </summary>
public class Inventory : MonoBehaviour
{
    public InventoryDataClass inventoryDataClass;
    public List<ItemSlot> slots;
    public UnityEvent SlotListChanged;
    public void CleanSlots()
    {
        slots = new();
    }
    
    public void AddSlot(ItemSlot.SlotType type)
    {
        if (slots == null)
        {
            slots = new();
        }
        slots.Add(new ItemSlot(type));
        SlotListChanged?.Invoke();
    }
    /// <summary>
    /// 添加物品，返回没能添加进库存的物品的数量
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    public int AddItem(ItemBase item,int number)
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
        return left;
    }
    /// <summary>
    /// 请求物品，返回实际请求到的物品的数量
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    public int RequestItem(ItemBase item, int number)
    {
        int get = 0;
        foreach (ItemSlot slot in slots)
        {
            get += slot.RequestItem(item, number - get);
            if (get == number)
            {
                break;
            }
        }
        return get;
    }
}
[CreateAssetMenu(fileName = "InventoryData",menuName ="Inventory/InventoryData")]
public class InventoryDataClass : ScriptableObject
{

    public int[] ItemNumber;
    public Dictionary<string, string> ItemInfo;
}
