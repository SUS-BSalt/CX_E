using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : Singleton<ItemFactory>
{
    public static ItemBase CreateItem(string _ItemClassName, string _ItemProfileJsonString)
    {
        Type type = Assembly.GetExecutingAssembly().GetType(_ItemClassName);
        if (type == null)
        {
            throw new Exception("û�ҵ�����Ʒ��" + _ItemClassName);
        }
        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor == null)
        {
            throw new Exception("����Ʒ��û�ṩ�޲ι��췽��" + _ItemClassName);
        }
        object instance = constructor.Invoke(null);
        ItemBase item = instance as ItemBase;
        item.SetProfileFromJson(_ItemProfileJsonString);
        return item;
        
    }
}

