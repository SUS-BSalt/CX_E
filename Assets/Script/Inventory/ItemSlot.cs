using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Events;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using Unity.VisualScripting;

/// <summary>
/// 
/// </summary>
[Serializable]
public class ItemSlot
{
    /// <summary>
    /// 这里不用unityevent是因为unityevent本质还是一个需要实例化的类型，它并不线程安全
    /// </summary>
    [JsonIgnore]
    public UnityEvent SlotChanged = new();
    /// <summary>
    /// Slot是否为空
    /// </summary>
    public bool isClean = true;
    [JsonIgnore]
    public ItemBase item;
    public int itemID;
    public string SerializedString;
    /// <summary>
    /// 物品堆叠数量
    /// </summary>
    public int stackingQuantity;
    public ItemSlot()
    {
    }
    public ItemSlot(string profileString)
    {
    }
    /// <summary>
    /// 取得当前item的用于显示在屏幕上的UI元素
    /// </summary>
    /// <returns></returns>
    public GameObject GetItemUIIsntance()
    {
        if (item != null)
        {
            return item.GetInstance("");
        }
        else
        {
            return null;
        }
    }
    public bool CompareToSlotedItem(ItemBase _item)
    {
        return item.AreTheySame(_item);
    }
    /// <summary>
    /// 测试该slot能否堆叠给定物品，不包括数量检测
    /// </summary>
    /// <param name="_item">测试物品</param>
    /// <param name="number">给定数量</param>
    /// <returns></returns>
    public bool CanThisStack(ItemBase _item)
    {
        if(_item == null)//传入物品为空，返回false
        {
            return false;
        }

        if (item == null)//如果slot为空,返回真
        {
            return true;
        }
        if(item.AreTheySame(_item) & item.canStacking)//slot内已有的物品，且可以合并，返回真
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// 将给定数量物品堆叠到该slot，返回[￥没能￥]堆叠进该slot的物品数量
    /// </summary>
    /// <param name="_item">物品</param>
    /// <param name="_number">堆叠数量</param>
    /// <returns></returns>
    public int StackingItem(ItemBase _item, int _number)
    {

        if (!CanThisStack(_item))
        {
            //Debug.Log("are you?");
            return _number;
        }
        int _FreeSpace = _item.stackingLimite - stackingQuantity;
        int _addNumber;
        int _returnNumber;
        if(_number > _FreeSpace)
        {
            _addNumber = _FreeSpace;
            _returnNumber = _number - _FreeSpace;
        }
        else
        {
            _addNumber = _number;
            _returnNumber = 0;
        }
        AddItem(_item, _addNumber);
        return _returnNumber;
    }
    /// <summary>
    /// 尝试从slot取出给定物品与尝试数量，返回实际取出的物品数量
    /// ！！！注意，若是slot物品被全部取出，slot会清除其指向物品的引用，若要获得物品引用请在这之前进行
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_number"></param>
    /// <returns></returns>
    public int RequestItem(ItemBase _item, int _number)
    {
        if (item == null || !item.AreTheySame(_item))// 如果slot为空，或物品不一样，则返回0
        {
            return 0;
        }
        int _minusNumber;
        if (_number > stackingQuantity)
        {
            _minusNumber = stackingQuantity;
        }
        else
        {
            _minusNumber = _number;
        }
        RemoveItem(_minusNumber);
        return _minusNumber;
    }
    private bool AddItem(ItemBase _item,int _addNumber = 1)
    {
        if (!CanThisStack(_item))//如果无法添加该物品，返回false
        {
            return false;
        }
        if (stackingQuantity + _addNumber > _item.stackingLimite)//slot空间不足时，返回false
        {
            return false;
        }
        if(item == null)
        {
            item = _item;
            stackingQuantity = _addNumber;
        }
        else
        {
            stackingQuantity += _addNumber;
        }
        isClean = false;


        SlotChanged?.Invoke();
        return true;
    }
    /// <summary>
    /// 移除指定数量的物品，若要获得到底移除了什么物品，请在移除前自行访问该slot的item字段
    /// </summary>
    /// <param name="removeNumber"></param>
    /// <returns></returns>
    public bool RemoveItem(int removeNumber = 1)
    {
        if(stackingQuantity - removeNumber < 0)
        {
            return false;
        }
        stackingQuantity -= removeNumber;
        if (stackingQuantity == 0)
        {
            CleanSlot();
        }
        SlotChanged?.Invoke();
        return true;
    }
    public void CleanSlot()
    {
        item = null;
        stackingQuantity = 0;
        isClean = true;
        SlotChanged?.Invoke();
    }

    public void OnSave()
    {
        if (isClean) { return; }
        itemID = item.ItemID;
        SerializedString = item.GetProfileJson();
    }
    public void OnLoad()
    {
        if (isClean) { return; }
        this.item = ItemFactory.CreateItem(itemID, SerializedString);
    }

}


