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
public class Inventory : MonoBehaviour
{
    public Text text;
    public string InventoryID;

    [SerializeField]
    private InventoryDataClass data;
    [Serialize]
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
        text.text = "保存1";
        text.text = (data == null).ToString();
        if (data == null)
        {
            text.text = "保存2";
            data = new();
        }
        text.text = "保存3";
        data.Clear();
        foreach(ItemSlot slot in slots)
        {
            data.ItemSlotType.Add(slot.type);
            data.ItemStackingQuantity.Add(slot.stackingQuantity);
            if (slot.item == null)
            {
                data.ItemClassName.Add(ItemSlot.NO_ITEM);
                data.ItemProfileJson.Add("{}");
            }
            else
            {
                data.ItemClassName.Add(slot.item.GetType().ToString());
                data.ItemProfileJson.Add(slot.item.GetProfileJson());
            }
        }
        SaveManager.Instance.SaveData<InventoryDataClass>("Inventory" + InventoryID, data);

    }
    public void OnLoad()
    {
        text.text = "加载1";
        var _data = SaveManager.Instance.LoadData<InventoryDataClass>("Inventory" + InventoryID);
        text.text = "加载2";
        data = _data;
        slots.Clear();
        for(int i = 0; i < data.ItemSlotType.Count; i++)
        {
            slots.Add(new(data.ItemSlotType[i]));
            if (data.ItemClassName[i] == ItemSlot.NO_ITEM)
            {
                continue;
            }
            ItemBase _item =  ItemFactory.CreateItem(data.ItemClassName[i], data.ItemProfileJson[i]);
            slots[i].StackingItem(_item, data.ItemStackingQuantity[i]);
        }
        SlotListChanged?.Invoke();

    }
}
public class InventoryDataClass
{
    public string InventoryID;
    [Serialize]
    public List<ItemSlot.SlotType> ItemSlotType;
    [Serialize]
    public List<int> ItemStackingQuantity;
    [Serialize]
    public List<string> ItemClassName;
    [Serialize]
    public List<string> ItemProfileJson;
    public InventoryDataClass()
    {
        ItemSlotType = new();
        ItemStackingQuantity = new();
        ItemClassName = new();
        ItemProfileJson = new();
    }
    public void Clear()
    {
        ItemSlotType.Clear();
        ItemStackingQuantity.Clear();
        ItemClassName.Clear();
        ItemProfileJson.Clear();
    }
}
