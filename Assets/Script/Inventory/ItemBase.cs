using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// һ����Ʒ������Ҫʵ�����µĺ�����
/// ����Ҫע��ʵ����ItemType
/// �ṩһ���޲εĹ��캯��
/// ItemFactory����������޲ι��캯��������Ʒʵ����������SetProfileFromJson��������ʵ����������
/// </summary>
public abstract class ItemBase
{
    public int ItemID;
    /// <summary>
    /// ��Ʒ�����ͣ�����������ν����Ƿ��࣬������Щslot
    ///  �������ڹ��캯����ʵ����
    /// </summary>
    public abstract ItemTypeBase ItemType { set; get; }
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
    public abstract GameObject GetInstance(string _Profile);

    public abstract bool AreTheySame(ItemBase _otherItem);
    public abstract ItemBase GetADeepCopy();
}
