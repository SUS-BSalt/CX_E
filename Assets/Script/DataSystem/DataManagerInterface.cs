using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.VisualScripting;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.InputSystem;
using System.Linq;

public class DataPack
{
    /// <summary>
    /// ���ݰ�����
    /// </summary>
    public string PackName { get; set; }

    /// <summary>
    /// δ���л���ԭʼ���ݣ������ļ���ַ�������÷�����Ӧ��DataUser���ʹ��
    /// </summary>
    public string DeserializeData { get; set; }
}

public interface IDataUser
{
    /// <summary>
    /// ���ݰ�����
    /// </summary>
    public string PackName { get; }
    /// <summary>
    /// --Ϊtrueʱ������Ҫ������������ݣ�Ҳ�������������Ҫ����浵�ļ���
    /// --Ϊfalseʱ��DataManager�����������д洢��
    /// --����ȫ��ͨ�õ��������ݣ����ǽ���DataUser�Լ����д��棬������ȫ����Ҫ�������ʱ����
    /// </summary>
    public bool IndividualizedSave { get; }
    /// <summary>
    /// DataManager�Ӵ�����ȡ����Ҫ���浽���ص�����
    /// </summary>
    public DataPack SerializedToDataPack();

    /// <summary>
    /// ����������ʹ���߷������ݵĽӿ�
    /// </summary>
    public T GetData<T>(params string[] argv);
    /// <summary>
    /// �������޸����ݵĽӿ�
    /// </summary>
    /// <returns>�����Ƿ��޸ĳɹ�</returns>
    public bool SetData<T>(T value, params string[] argv);
}
public class DataManagerBase
{
    private Dictionary<string, DataPack> Org_datas;
    private Dictionary<string, IDataUser> DataUsers;

    public DataManagerBase()
    {
        Org_datas = new();
        DataUsers = new();
    }

    public void Append_Org_datas(Dictionary<string, DataPack> append_data)
    {
        foreach(string key in append_data.Keys)
        {
            try
            {
                Org_datas.Add(key, append_data[key]);
            }
            catch (ArgumentException)
            {
                Org_datas[key] = append_data[key];
            }
        }
    }
    public void Init(string Json_Org_Data)
    {
        Org_datas = new();
        DataUsers = new();
        Org_datas = JsonConvert.DeserializeObject<Dictionary<string, DataPack>>(Json_Org_Data);
    }
    public string SerializeDatas()
    {
        Dictionary<string, DataPack> Modifided_data = new();
        foreach(string key in Org_datas.Keys)
        {
            if (DataUsers.ContainsKey(key))
            {
                Modifided_data.Add(key, DataUsers[key].SerializedToDataPack());
            }
            else
            {
                Modifided_data.Add(key,Org_datas[key]);
            }
        }
        foreach(string key in DataUsers.Keys)
        {
            if (!Modifided_data.ContainsKey(key) && DataUsers[key].IndividualizedSave)//�����������Ƿ���Ҫ���浽����
            {
                Modifided_data.Add(key, DataUsers[key].SerializedToDataPack());
            }
        }
        return JsonConvert.SerializeObject(Modifided_data, Formatting.Indented);
    }
    public void RegisterDataUser(IDataUser dataUser)
    {
        if (DataUsers.ContainsKey(dataUser.PackName))
        {
            throw new Exception("DataUser��ע��:" + dataUser.PackName);
        }
        DataUsers.Add(dataUser.PackName, dataUser);
        Debug.Log($"DataUserע��ɹ�:{dataUser.PackName}");
    }
    public void UnrigisterDataUser(IDataUser dataUser)
    {
        if (DataUsers.ContainsKey(dataUser.PackName))
        {
            DataUsers.Remove(dataUser.PackName);
        }
        throw new Exception("DataUser������:" + dataUser.PackName);
    }
    public DataPack GetDataPack(string packName)
    {
        if (!DataUsers.ContainsKey(packName))
        {
            throw new Exception("δ�ҵ����ݰ�:" + packName);
        }
        return Org_datas[packName];
    }

    public void UpdateDataPack(string packName, string DeserializeData)
    {
        if (Org_datas.ContainsKey(packName))
        {
            Org_datas[packName].DeserializeData = DeserializeData;
        }
        else
        {
            DataPack pack = new();
            pack.PackName = packName;
            pack.DeserializeData = DeserializeData;
            Org_datas.Add(packName, pack);
        }
    }
    public T GetData<T>(params string[] argv)
    {
        T result;
        IDataUser user;
        try
        {
            user = DataUsers[argv[0]];
        }
        catch
        {
            Debug.Log("δ�ҵ�DataUser:" + argv[0]);
            throw new DataGetException("δ�ҵ�DataUser:" + argv[0]);
        }

        result = user.GetData<T>(argv.Skip(1).ToArray());
        return result;
    }
    public bool SetData<T>(T value,params string[] argv)
    {
        bool result;
        IDataUser user;
        try
        {
            user = DataUsers[argv[0]];
        }
        catch
        {
            Debug.Log("δ�ҵ�DataUser:" + argv[0]);
            throw new DataSetException("δ�ҵ�DataUser:" + argv[0]);
        }

        result = user.SetData<T>(value, argv.Skip(1).ToArray());
        return result;
    }
}

public class DataGetException: Exception
{
    public DataGetException(string message) : base(message)
    {

    }
}
public class DataSetException : Exception
{
    public DataSetException(string message) : base(message)
    {

    }
}