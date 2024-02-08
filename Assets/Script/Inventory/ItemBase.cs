using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemBase//继承时要重写Equals与GetHashCode，因为
{
    public const string ItemName = "ItemBase";
    public abstract bool canStacking { get; set; }
    public int stackingLimite = 64;
    public abstract string SerializeToJson();
    public abstract void DeSerializeFromJson(string _JsonString);
    public abstract GameObject GetInstance(string _profile);

    public override bool Equals(object obj)
    {
        if(obj == null || GetType() != obj.GetType())
        {
            return false;
        }//如果传参为空或类型不同返回false
        return true;//这个类不会有互有差异的实例，所以只要是该类的实例便视为一致
    }
    public override int GetHashCode()
    {
        unchecked
        {
            return (ItemName.GetHashCode());
        }
    }
}
