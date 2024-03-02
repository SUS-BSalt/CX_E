using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 对Inventory来说，slot实际上是“空间”概念的具象，Inventory需要slot的概念来规定空间的安排，而它不应该做更多事了，slot如何显示，显示在哪之类的信息该是由UI来全权负责
/// </summary>
public class Inventory
{
    public List<BuffBase> buffs;
    public string InventoryID { get { return data.InventoryID; } set { data.InventoryID = value; } }

    [SerializeField]
    public InventoryDataClass data;
    [Serialize]
    public List<ItemSlot> slots;
    public UnityEvent SlotListChanged;
    public Inventory()
    {
        SlotListChanged = new UnityEvent();
        slots = new();
        buffs = new();
    }
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
    /// 清除空的Slot
    /// </summary>
    public void RemoveCleanSlot()
    {
        slots.RemoveAll(_slot => _slot.item == null);
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
    public void AddItemWithAddSlotAuto(ItemBase item, int number)
    {
        int left = number;
        while(true)
        {
            left = AddItem(item, left);
            if(left != 0)
            {
                AddSlot(item.ItemType.SlotType);
            }
            if(left == 0)
            {
                break;
            }
        }
    }
    /// <summary>
    /// 请求物品，返回实际请求到的物品的数量
    /// </summary>
    /// <param name="item"></param>
    /// <param name="number"></param>
    public int RequestItem(ItemBase item, int number)
    {
        int get = 0;

        for (int i = slots.Count-1;  i >= 0; i--)
        {
            get += slots[i].RequestItem(item, number - get);
            if (get == number)
            {
                break;
            }
        }
        return get;
    }
    public void OnSave()
    {
        if (data == null)
        {
            data = new();
        }
        data.Clear();
        foreach(ItemSlot slot in slots)
        {
            slot.OnSave();
            data.ItemSlotData.Add(slot.data);
        }
        foreach(BuffBase buff in buffs)
        {
            data.BuffNames.Add(buff.GetType().ToString());
        }
    }
    public void OnLoad()
    {
        slots.Clear();
        buffs.Clear();
        for(int i = 0; i < data.ItemSlotData.Count; i++)
        {
            slots.Add(new(data.ItemSlotData[i]));
        }
        SlotListChanged?.Invoke();
        foreach(string buffname in data.BuffNames)
        {
            buffs.Add(BuffFactory.CreateBuffInstance(buffname));
        }

    }
    public int GetItemValueAfterBuff(ItemBase item)
    {
        int value = 0;

        foreach(BuffBase buff in buffs)
        {
            value += buff.ValueCheck(item) - item.value;
        }
        return value + item.value;
    }
}
public class InventoryDataClass
{
    public string InventoryID;
    public List<ItemSlotDataClass> ItemSlotData;
    public List<string> BuffNames;
    public InventoryDataClass()
    {
        ItemSlotData = new();
        BuffNames = new();
    }
    public InventoryDataClass(string InventoryID)
    {
        ItemSlotData = new();
        BuffNames = new();
        this.InventoryID = InventoryID;
    }
    public void Clear()
    {
        ItemSlotData.Clear();
    }
}
