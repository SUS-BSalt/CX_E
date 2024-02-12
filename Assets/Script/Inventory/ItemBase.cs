using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase
{
    public string ItemID;
    public ItemTypeBase ItemType;
    /// <summary>
    /// ��Ʒ�Ƿ�ɶѵ�
    /// </summary>
    public bool canStacking = true;
    /// <summary>
    /// �ѵ�����
    /// </summary>
    public int stackingLimite = 64;

    public int value = 0;
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
    /// UIģ���������õ�һ��������ʾ����Ļ�ϵ����⣬���Ǹ�gameobj�ϲ������Ʒ��ʵ������ֻ����һ��������ʾ��Ƥ�ң���Ʒʵ���������ڵĵط�Ӧ����Inventoryϵͳ����
    /// </summary>
    /// <returns></returns>
    public abstract GameObject GetInstance();

    public abstract bool AreTheySame(ItemBase _otherItem);
    public abstract ItemBase getADeepCopy();
}
