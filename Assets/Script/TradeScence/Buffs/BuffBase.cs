using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BuffFactory
{
    public static BuffBase CreateBuffInstance(string _BuffClassName)
    {
        Type type = Assembly.GetExecutingAssembly().GetType(_BuffClassName);
        if (type == null)
        {
            throw new Exception("没找到该物品：" + _BuffClassName);
        }
        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
        if (constructor == null)
        {
            throw new Exception("该物品类没提供无参构造方法" + _BuffClassName);
        }
        object instance = constructor.Invoke(null);
        BuffBase Buff = instance as BuffBase;
        return Buff;
    }
}
public class BuffBase
{
    public string BuffName;
    public string BuffDescribe;
    public virtual int ValueCheck(ItemBase _item)
    {
        return _item.value;
    }
}

public class AngryBuff
{

}
