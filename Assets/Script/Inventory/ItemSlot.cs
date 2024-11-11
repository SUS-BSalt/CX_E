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
    /// ���ﲻ��unityevent����Ϊunityevent���ʻ���һ����Ҫʵ���������ͣ��������̰߳�ȫ
    /// </summary>
    [JsonIgnore]
    public UnityEvent SlotChanged = new();
    /// <summary>
    /// Slot�Ƿ�Ϊ��
    /// </summary>
    public bool isClean = true;
    [JsonIgnore]
    public ItemBase item;
    public int itemID;
    public string SerializedString;
    /// <summary>
    /// ��Ʒ�ѵ�����
    /// </summary>
    public int stackingQuantity;
    public ItemSlot()
    {
    }
    public ItemSlot(string profileString)
    {
    }
    /// <summary>
    /// ȡ�õ�ǰitem��������ʾ����Ļ�ϵ�UIԪ��
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
        if (!CanThisStack(_item))//����޷���Ӹ���Ʒ������false
        {
            return false;
        }
        if (stackingQuantity + _addNumber > _item.stackingLimite)//slot�ռ䲻��ʱ������false
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
    /// �Ƴ�ָ����������Ʒ����Ҫ��õ����Ƴ���ʲô��Ʒ�������Ƴ�ǰ���з��ʸ�slot��item�ֶ�
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


