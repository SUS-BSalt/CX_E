using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class ItemFactory : Singleton<ItemFactory>
{
    public const int NULL_ITEM_ID = 1;
    public static ItemBase CreateItem(string _ItemClassName, string _ItemProfileJsonString)
    {
        ItemBase item = CreateItemInstance(_ItemClassName);
        item.SetProfileFromJson(_ItemProfileJsonString);
        return item;
    }
    public static ItemBase CreateItem(int ItemID, string _ItemProfileJsonString)
    {
        if(ItemID == NULL_ITEM_ID)
        {
            return null;
        }
        ITableDataReader ItemTable = GameDataManager.Instance.Tables["ItemData"];
        ItemBase item = CreateItemInstance(ItemTable.GetData<string>(ItemID, 1));
        item.SetProfileFromTable(ItemTable, ItemID);
        item.SetProfileFromJson(_ItemProfileJsonString);
        return item;
    }
    private static ItemBase CreateItemInstance(string _ItemClassName)
    {
        Type type = Assembly.GetExecutingAssembly().GetType(_ItemClassName);
        if (type == null)
        {
            throw new Exception("没找到该物品：" + _ItemClassName);
        }
        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor == null)
        {
            throw new Exception("该物品类没提供无参构造方法" + _ItemClassName);
        }
        object instance = constructor.Invoke(null);
        ItemBase item = instance as ItemBase;
        return item;
    }
}

