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
    /// ���Ը�slot�ܷ�ѵ�������������Ʒ
    /// </summary>
    /// <param name="_item">������Ʒ</param>
    /// <param name="number">��������</param>
    /// <returns></returns>
    public bool CanStackingMore(ItemBase _item, int number = 1)
    {
        if (item == null & item.canStacking & number <= _item.stackingLimite)//slotΪ��,��Ҫ��ӵ�����С�ڶѵ�����ʱ��������
        {
            return true;
        }
        if(item.AreTheySame(_item) & item.canStacking & (stackingNumber + number <= item.stackingLimite))//slot�����е���Ʒ���Ժϲ�����û���ѵ����ޣ�������
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// ������������Ʒ�ѵ�����slot������[û�ܹ�]�ѵ�����slot����Ʒ����
    /// </summary>
    /// <param name="_item">��Ʒ</param>
    /// <param name="_number">�ѵ�����</param>
    /// <returns></returns>
    public int StackingItem(ItemBase _item, int _number)
    {
        if (!item.canStacking || !item.AreTheySame(_item))// �����Ʒ��һ��������Ʒ�޷��ѵ�����ԭ��������������
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
    /// ���Դ�slotȡ��������Ʒ�볢������������ʵ��ȡ������Ʒ����
    /// ������ע�⣬����slot��Ʒ��ȫ��ȡ����slot�������ָ����Ʒ�����ã���Ҫ�����Ʒ����������֮ǰ����
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_number"></param>
    /// <returns></returns>
    public int RequestItem(ItemBase _item, int _number)
    {
        if (item == null || !item.AreTheySame(_item))// ���slotΪ�գ�����Ʒ��һ�����򷵻�0
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
        if (!CanStackingMore(_item, _addNumber))//����޷���Ӹ���������Ʒ������false
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
    /// �Ƴ�ָ����������Ʒ����Ҫ��õ����Ƴ���ʲô��Ʒ�������Ƴ�ǰ���з��ʸ�slot��item�ֶ�
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

