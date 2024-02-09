using Neo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
public class ItemSlot : MonoBehaviour
{
    public UnityEvent SlotChanged;
    public enum SlotType
    {
        mission, matter, speach, other
    }
    public SlotType type;
    public ItemBase item;
    public int stackingNumber;
    public ItemSlot(SlotType _type)
    {
        type = _type;
    }
    public bool CompareToSlotedItem(ItemBase _item)
    {
        return item.AreTheySame(_item);
    }
    /// <summary>
    /// 测试该slot能否堆叠给定数量的物品
    /// </summary>
    /// <param name="_item">测试物品</param>
    /// <param name="number">给定数量</param>
    /// <returns></returns>
    public bool CanStackingMore(ItemBase _item, int number = 1)
    {
        if (item == null & item.canStacking & number <= _item.stackingLimite)//slot为空,且要添加的数量小于堆叠限制时，返回真
        {
            return true;
        }
        if(item.AreTheySame(_item) & item.canStacking & (stackingNumber + number <= item.stackingLimite))//slot内已有的物品可以合并，且没到堆叠上限，返回真
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// 将给定数量物品堆叠到该slot，返回[没能够]堆叠进该slot的物品数量
    /// </summary>
    /// <param name="_item">物品</param>
    /// <param name="_number">堆叠数量</param>
    /// <returns></returns>
    public int StackingItem(ItemBase _item, int _number)
    {
        if (!item.canStacking || !item.AreTheySame(_item))// 如果物品不一样，或物品无法堆叠，则原样返回请求数量
        {
            return _number;
        }
        int _FreeSpace = item.stackingLimite - stackingNumber;
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
        if (_number > stackingNumber)
        {
            _minusNumber = stackingNumber;
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
        if (!CanStackingMore(_item, _addNumber))//如果无法添加该数量的物品，返回false
        {
            return false;
        }
        if(item == null)
        {
            item = _item;
            stackingNumber = _addNumber;
        }
        else
        {
            stackingNumber += _addNumber;
        }
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
        if(stackingNumber - removeNumber < 0)
        {
            return false;
        }
        stackingNumber -= removeNumber;
        if (stackingNumber == 0)
        {
            CleanSlot();
        }
        SlotChanged?.Invoke();
        return true;
    }
    public void CleanSlot()
    {
        item = null;
        stackingNumber = 0;
        SlotChanged?.Invoke();
    }

}

