using Neo;
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
public class ItemSlot
{
    public UnityEvent SlotChanged;

    public ItemSlotDataClass data;
    public enum SlotType
    {
        mission, matter, speach, other, allType
    }
    public SlotType type;
    [Serialize]
    public ItemBase item;
    public string itemName;
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
    /// ���Ը�slot�ܷ�ѵ�������Ʒ���������������
    /// </summary>
    /// <param name="_item">������Ʒ</param>
    /// <param name="number">��������</param>
    /// <returns></returns>
    public bool CanThisStack(ItemBase _item)
    {
        if(_item == null)//������ƷΪ�գ�����false
        {
            return false;
        }
        if (_item.ItemType.SlotType != type && type != SlotType.allType)// �����Ʒ�Ĵ洢�������slot�����ݣ�����false
        {
            return false;
        }

        if (item == null)//���slotΪ��,������
        {
            return true;
        }
        if(item.AreTheySame(_item) & item.canStacking)//slot�����е���Ʒ���ҿ��Ժϲ���������
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    /// <summary>
    /// ������������Ʒ�ѵ�����slot������[��û�ܣ�]�ѵ�����slot����Ʒ����
    /// </summary>
    /// <param name="_item">��Ʒ</param>
    /// <param name="_number">�ѵ�����</param>
    /// <returns></returns>
    public int StackingItem(ItemBase _item, int _number)
    {
        if (!CanThisStack(_item))
        {
            return _number;
        }
        int _FreeSpace = _item.stackingLimite - stackingNumber;
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
        if (!CanThisStack(_item))//����޷���Ӹ���Ʒ������false
        {
            return false;
        }
        if (stackingNumber + _addNumber > _item.stackingLimite)//slot�ռ䲻��ʱ������false
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
        itemName =  item.GetType().ToString();
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
        itemName = "";
        stackingNumber = 0;
        SlotChanged?.Invoke();
    }

}

public class ItemSlotDataClass
{
    public SlotType slotType;
    public int stackingNumber;
    public string ItemClassName;
    public string ItemProfileInfo;
}

