using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// --一个物品，除了要实现以下的函数外
/// --还需要提供一个无参的构造函数
/// --ItemFactory会利用这个无参构造函数创建物品实例，随后先调用SetProfileFromTable，后调用SetProfileFromJson方法，对实例进行配置
/// --保存时，任何自定义的属性应该都是JsonIgnore的，应该只用该类提供的ItemID与SerializedString属性进行数据的保存
/// --因为Json的反序列化时只会生成该基类，所以需要、也只能根据这两个属性重新生成一个实例来覆盖
/// </summary>
public abstract class ItemBase
{
    /// <summary>
    /// ItemFactory会根据该ID查表，根据配置表生成一个默认实例，随后再调用其他方法完成自定义
    /// </summary>
    public abstract int ItemID { get; set; }
    [JsonIgnore]
    public string ICON_PATH;
    /// <summary>
    /// 物品的标签，用来决定如何将他们分类
    /// </summary>
    [JsonIgnore]
    public List<int> ItemTabs { set; get; }
    /// <summary>
    /// 物品是否可堆叠
    /// </summary>
    [JsonIgnore]
    public bool canStacking = true;
    /// <summary>
    /// 堆叠上限
    /// </summary>
    [JsonIgnore]
    public int stackingLimite = 50;
    /// <summary>
    /// 购物价值基数
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
    /// 物品将自己所需要保存的信息装成json字符串，保存系统就能保存它的返回值了
    /// </summary>
    /// <returns></returns>
    public abstract string GetProfileJson();
    /// <summary>
    /// 保存系统会将json字符串通过该方法给物品的实例，实例自身通过其进行设置
    /// </summary>
    /// <param name="_JsonString"></param>
    public abstract void SetProfileFromJson(string _JsonString);
    /// <summary>
    /// 生成物品时，首先读取物品表来获得一些基础数据
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
    /// UI模块调用这货得到一个gameobj用来显示在屏幕上，不要返回一个待实例化的预制体，直接在这个方法里完成实例化
    /// ----而这个类的实例，也就是物品的实例，实际放在Inventory里
    /// </summary>
    /// <returns></returns>
    public abstract GameObject GetInstance(string _Profile);
    /// <summary>
    /// 用来判断是否可以堆叠
    /// </summary>
    /// <param name="_otherItem"></param>
    /// <returns></returns>
    public abstract bool AreTheySame(ItemBase _otherItem);

    public abstract ItemBase GetADeepCopy();
}
