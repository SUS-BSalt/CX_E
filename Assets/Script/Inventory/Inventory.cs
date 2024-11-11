using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
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
    public string InventoryID;
    public List<ItemSlot> slots;

    [Serialize]
    [JsonIgnore]
    public UnityEvent OnLoadE = new();
    [JsonIgnore]
    public UnityEvent<List<ItemSlot>> SlotRemoved = new();
    [JsonIgnore]
    public UnityEvent<List<ItemSlot>> SlotAdded = new();
    public Inventory(string InventoryID)
    {
        slots = new();
        this.InventoryID = InventoryID;
    }
    public void CleanSlots()
    {
        slots = new();
    }
    
    public void AddSlot()
    {
        if (slots == null)
        {
            slots = new();
        }
        ItemSlot NewSlot = new();
        slots.Add(NewSlot);

        List<ItemSlot> ChangedSlot = new();
        ChangedSlot.Add(NewSlot);
        SlotAdded?.Invoke(ChangedSlot);
    }
    /// <summary>
    /// 清除空的Slot
    /// </summary>
    public void RemoveCleanSlot()
    {
        List<ItemSlot> ChangedSlot = new();
        foreach(ItemSlot slot in slots)
        {
            if (slot.isClean == true)
            {
                ChangedSlot.Add(slot);
            }
        }
        SlotRemoved?.Invoke(ChangedSlot);
        slots.RemoveAll(_slot => _slot.isClean == true);
        
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
                AddSlot();
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
    public int FindItem(ItemBase item)
    {
        return 0;
    }
    public int FindItem(string _itemID)
    {
        int temp = 0;
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            if (int.Parse(_itemID) == slots[i].item.ItemID)
            {
                temp += slots[i].stackingQuantity;
            }
        }
        return temp;
    }
    public void OnSave()
    {
        foreach (ItemSlot slot in slots)
        {
            slot.OnSave();
        }
    }
    public void OnLoad()
    {
        foreach(ItemSlot slot in slots)
        {
            slot.OnLoad();
        }
        OnLoadE?.Invoke();
    }
}

