using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileDataManager : Singleton<ProfileDataManager>, IDataUser
{
    /// <summary>
    /// 配置表
    /// </summary>
    public Dictionary<string, ITableDataReader> Tables;
    /// <summary>
    /// 用于在编辑器中添加配置表信息
    /// </summary>
    [SerializeField]
    private List<TableDataSO> tableDataSoList;

    string IDataUser.PackName { get { return "Profile"; } }

    bool IDataUser.IndividualizedSave{ get { return false; } }

    T IDataUser.GetData<T>(params string[] argv)
    {
        //当想直接获取配置表时
        if (typeof(T).Equals(typeof(ITableDataReader)))
        {
            return (T)Tables[argv[0]];
        }
        //当想获取表内部的内容时
        if (Tables.ContainsKey(argv[0]))
        {
            try
            {
                return Tables[argv[0]].GetData<T>(int.Parse(argv[1]), int.Parse(argv[2]));
            }
            catch
            {
                throw new DataGetException($"在访问{argv[0]}的数据时遇到错误，访问者传递了:'{argv}'的参数");
            }
        }
        throw new DataGetException("项不存在:"+ argv[0]);
    }
    bool IDataUser.SetData<T>(T value, params string[] argv)
    {
        if (Tables.ContainsKey(argv[0]))
        {
            try
            {
                Tables[argv[0]].SetData<T>(int.Parse(argv[1]), int.Parse(argv[2]),value);
            }
            catch
            {
                throw new DataGetException($"在访问{argv[0]}的数据时遇到错误，访问者传递了:'{argv[1]}'的参数");
            }
        }
        else
        {
            throw new DataGetException("项不存在:" + argv[0]);
        }
        return true;
    }

    DataPack IDataUser.SerializedToDataPack()
    {
        throw new System.Exception("配置文件无法转化为DataPack");
    }

    protected override void Awake()
    {
        base.Awake();
        Tables = new();
        foreach (TableDataSO table in tableDataSoList)
        {
            Tables.Add(table.tableName, table.GetTable());
        }
        DataManager.Instance.RegisterDataUser(this);
    }
}

