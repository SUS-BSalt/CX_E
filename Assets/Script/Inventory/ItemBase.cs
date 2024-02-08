using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class ItemBase//�̳�ʱҪ��дEquals��GetHashCode����Ϊ
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
        }//�������Ϊ�ջ����Ͳ�ͬ����false
        return true;//����಻���л��в����ʵ��������ֻҪ�Ǹ����ʵ������Ϊһ��
    }
    public override int GetHashCode()
    {
        unchecked
        {
            return (ItemName.GetHashCode());
        }
    }
}
