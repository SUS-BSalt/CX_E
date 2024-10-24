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
    /// 数据包名称
    /// </summary>
    public string PackName { get; set; }

    /// <summary>
    /// 未序列化的原始数据，或是文件地址，具体用法看对应的DataUser如何使用
    /// </summary>
    public string DeserializeData { get; set; }
}

public interface IDataUser
{
    /// <summary>
    /// 数据包名称
    /// </summary>
    public string PackName { get; }
    /// <summary>
    /// --为true时，是需要独立储存的数据，也就是这份数据需要存入存档文件中
    /// --为false时，DataManager不负责对其进行存储，
    /// --比如全局通用的配置数据，它们将由DataUser自己进行储存，或是完全不需要储存的临时数据
    /// </summary>
    public bool IndividualizedSave { get; }
    /// <summary>
    /// DataManager从此属性取得需要保存到本地的数据
    /// </summary>
    public DataPack SerializedToDataPack();

    /// <summary>
    /// 其他的数据使用者访问数据的接口
    /// </summary>
    public T GetData<T>(params string[] argv);
    /// <summary>
    /// 其他人修改数据的接口
    /// </summary>
    /// <returns>数据是否修改成功</returns>
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
            if (!Modifided_data.ContainsKey(key) && DataUsers[key].IndividualizedSave)//检查这份数据是否需要保存到本地
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
            throw new Exception("DataUser已注册:" + dataUser.PackName);
        }
        DataUsers.Add(dataUser.PackName, dataUser);
        Debug.Log($"DataUser注册成功:{dataUser.PackName}");
    }
    public void UnrigisterDataUser(IDataUser dataUser)
    {
        if (DataUsers.ContainsKey(dataUser.PackName))
        {
            DataUsers.Remove(dataUser.PackName);
        }
        throw new Exception("DataUser不存在:" + dataUser.PackName);
    }
    public DataPack GetDataPack(string packName)
    {
        if (!DataUsers.ContainsKey(packName))
        {
            throw new Exception("未找到数据包:" + packName);
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
            Debug.Log("未找到DataUser:" + argv[0]);
            throw new DataGetException("未找到DataUser:" + argv[0]);
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
            Debug.Log("未找到DataUser:" + argv[0]);
            throw new DataSetException("未找到DataUser:" + argv[0]);
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