using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase
{
    public string ItemID;
    public ItemTypeBase ItemType;
    /// <summary>
    /// 物品是否可堆叠
    /// </summary>
    public bool canStacking = true;
    /// <summary>
    /// 堆叠数量
    /// </summary>
    public int stackingLimite = 64;

    public int value = 0;
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
    /// UI模块调用这货得到一个可以显示在屏幕上的玩意，而那个gameobj上不会挂物品的实例，它只会是一个用来显示的皮囊，物品实例真正所在的地方应该在Inventory系统里面
    /// </summary>
    /// <returns></returns>
    public abstract GameObject GetInstance();

    public abstract bool AreTheySame(ItemBase _otherItem);
    public abstract ItemBase getADeepCopy();
}
