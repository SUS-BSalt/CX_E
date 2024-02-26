using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// ��Inventory��˵��slotʵ�����ǡ��ռ䡱����ľ���Inventory��Ҫslot�ĸ������涨�ռ�İ��ţ�������Ӧ�����������ˣ�slot�����ʾ����ʾ����֮�����Ϣ������UI��ȫȨ����
/// </summary>
public class Inventory : MonoBehaviour
{
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
    /// �����Ʒ������û����ӽ�������Ʒ������
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
    /// ������Ʒ������ʵ�����󵽵���Ʒ������
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
    public void SetInventory(string InventoryID)
    {
        data = InventoryManager.Instance.GetData(InventoryID);
        OnLoad();
    }
    public void InventoryQuit()
    {
        OnSave();
        InventoryManager.Instance.SetData(data);
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
    }
    public void OnLoad()
    {
        slots.Clear();
        for(int i = 0; i < data.ItemSlotData.Count; i++)
        {
            slots.Add(new(data.ItemSlotData[i]));
        }
        SlotListChanged?.Invoke();

    }
}
public class InventoryDataClass
{
    public string InventoryID;
    public List<ItemSlotDataClass> ItemSlotData;
    public InventoryDataClass()
    {
        ItemSlotData = new();
    }
    public InventoryDataClass(string InventoryID)
    {
        ItemSlotData = new();
        this.InventoryID = InventoryID;
    }
    public void Clear()
    {
        ItemSlotData.Clear();
    }
}
