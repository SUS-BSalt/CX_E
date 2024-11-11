using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// --һ����Ʒ������Ҫʵ�����µĺ�����
/// --����Ҫ�ṩһ���޲εĹ��캯��
/// --ItemFactory����������޲ι��캯��������Ʒʵ��������ȵ���SetProfileFromTable�������SetProfileFromJson��������ʵ����������
/// --����ʱ���κ��Զ��������Ӧ�ö���JsonIgnore�ģ�Ӧ��ֻ�ø����ṩ��ItemID��SerializedString���Խ������ݵı���
/// --��ΪJson�ķ����л�ʱֻ�����ɸû��࣬������Ҫ��Ҳֻ�ܸ���������������������һ��ʵ��������
/// </summary>
public abstract class ItemBase
{
    /// <summary>
    /// ItemFactory����ݸ�ID����������ñ�����һ��Ĭ��ʵ��������ٵ���������������Զ���
    /// </summary>
    public abstract int ItemID { get; set; }
    [JsonIgnore]
    public string ICON_PATH;
    /// <summary>
    /// ��Ʒ�ı�ǩ������������ν����Ƿ���
    /// </summary>
    [JsonIgnore]
    public List<int> ItemTabs { set; get; }
    /// <summary>
    /// ��Ʒ�Ƿ�ɶѵ�
    /// </summary>
    [JsonIgnore]
    public bool canStacking = true;
    /// <summary>
    /// �ѵ�����
    /// </summary>
    [JsonIgnore]
    public int stackingLimite = 50;
    /// <summary>
    /// �����ֵ����
    /// </summary>
    [JsonIgnore]
    public int value = 0;
    public bool IsTabExist(int tabInd)
    {
        if(ItemTabs == null)
        {
            ItemTabs = new();
            return false;
        }
        return ItemTabs.Contains(tabInd);
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
            if (!IsTabExist(int.Parse(tabName)))
            {
                ItemTabs.Add(int.Parse(tabName));
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
    /// <summary>
    /// ������Ʒʱ�����ȶ�ȡ��Ʒ�������һЩ��������
    /// </summary>
    /// <param name="tableReader"></param>
    /// <param name="rowIndex"></param>
    public virtual void SetProfileFromTable(ITableDataReader tableReader,int rowIndex)
    {
        ItemID = rowIndex;
        ICON_PATH = tableReader.GetData<string>(rowIndex, 3);
        AddTabs(tableReader.GetData<string>(rowIndex, 4));
        value = tableReader.GetData<int>(rowIndex, 5);
    }

    /// <summary>
    /// UIģ���������õ�һ��gameobj������ʾ����Ļ�ϣ���Ҫ����һ����ʵ������Ԥ���壬ֱ����������������ʵ����
    /// ----��������ʵ����Ҳ������Ʒ��ʵ����ʵ�ʷ���Inventory��
    /// </summary>
    /// <returns></returns>
    public abstract GameObject GetInstance(string _Profile);
    /// <summary>
    /// �����ж��Ƿ���Զѵ�
    /// </summary>
    /// <param name="_otherItem"></param>
    /// <returns></returns>
    public abstract bool AreTheySame(ItemBase _otherItem);

    public abstract ItemBase GetADeepCopy();
}
