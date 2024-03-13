using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// һ����Ʒ������Ҫʵ�����µĺ�����
/// ����Ҫע��ʵ����ItemType
/// �ṩһ���޲εĹ��캯��
/// ItemFactory����������޲ι��캯��������Ʒʵ����������SetProfileFromJson��������ʵ����������
/// </summary>
public abstract class ItemBase
{
    public abstract int ItemID { get; set; }
    /// <summary>
    /// ��Ʒ�����ͣ�����������ν����Ƿ��࣬������Щslot
    ///  �������ڹ��캯����ʵ����
    /// </summary>
    public abstract ItemTypeBase ItemType { set; get; }
    public List<string> ItemTabs { set; get; }
    public ItemMSGBoard MSG = new();
    /// <summary>
    /// ��Ʒ�Ƿ�ɶѵ�
    /// </summary>
    public bool canStacking = true;
    /// <summary>
    /// �ѵ�����
    /// </summary>
    public int stackingLimite = 64;

    public int value = 0;
    public bool IsTabExist(string tabName)
    {
        if(ItemTabs == null)
        {
            ItemTabs = new();
            return false;
        }
        return ItemTabs.Contains(tabName);
    }
    public void AddTabs(string _OriginString)
    {
        if (ItemTabs == null)
        {
            ItemTabs = new();
        }
        if (_OriginString == null)
        {
            return;
        }
        foreach (string tabName in _OriginString.Split("+"))
        {
            if (!IsTabExist(tabName))
            {
                ItemTabs.Add(tabName);
            }

        }
    }
    /// <summary>
    /// ��Ʒ���Լ�����Ҫ�������Ϣװ��json�ַ���������ϵͳ���ܱ������ķ���ֵ��
    /// </summary>
    /// <returns></returns>
    public abstract string GetProfileJson();
    /// <summary>
    /// ����ϵͳ�Ὣjson�ַ���ͨ���÷�������Ʒ��ʵ����ʵ������ͨ�����������
    /// </summary>
    /// <param name="_JsonString"></param>
    public abstract void SetProfileFromJson(string _JsonString);
    public virtual void SetProfileFromTable(ITableDataReader tableReader,int rowIndex)
    {

        ItemID = rowIndex;
        MSG.ItemName = tableReader.GetData<string>(rowIndex, 2);
        MSG.ItemDescribe = tableReader.GetData<string>(rowIndex, 3);
        value = tableReader.GetData<int>(rowIndex, 4);
        MSG.ItemValueDescribe = value.ToString();
        AddTabs(tableReader.GetData<string>(rowIndex, 5));
    }


    /// <summary>
    /// UIģ���������õ�һ��������ʾ����Ļ�ϵ����⣬���Ǹ�gameobj�ϲ������Ʒ��ʵ������ֻ����һ��������ʾ��Ƥ�ң���Ʒʵ���������ڵĵط�Ӧ����Inventoryϵͳ����
    /// </summary>
    /// <returns></returns>
    public abstract GameObject GetInstance(string _Profile);

    public abstract bool AreTheySame(ItemBase _otherItem);
    public abstract ItemBase GetADeepCopy();
}
