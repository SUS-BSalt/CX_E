using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileDataManager : Singleton<ProfileDataManager>, IDataUser
{
    /// <summary>
    /// ���ñ�
    /// </summary>
    public Dictionary<string, ITableDataReader> Tables;
    /// <summary>
    /// �����ڱ༭����������ñ���Ϣ
    /// </summary>
    [SerializeField]
    private List<TableDataSO> tableDataSoList;

    string IDataUser.PackName { get { return "Profile"; } }

    bool IDataUser.IndividualizedSave{ get { return false; } }

    T IDataUser.GetData<T>(params string[] argv)
    {
        //����ֱ�ӻ�ȡ���ñ�ʱ
        if (typeof(T).Equals(typeof(ITableDataReader)))
        {
            return (T)Tables[argv[0]];
        }
        //�����ȡ���ڲ�������ʱ
        if (Tables.ContainsKey(argv[0]))
        {
            try
            {
                return Tables[argv[0]].GetData<T>(int.Parse(argv[1]), int.Parse(argv[2]));
            }
            catch
            {
                throw new DataGetException($"�ڷ���{argv[0]}������ʱ�������󣬷����ߴ�����:'{argv}'�Ĳ���");
            }
        }
        throw new DataGetException("�����:"+ argv[0]);
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
                throw new DataGetException($"�ڷ���{argv[0]}������ʱ�������󣬷����ߴ�����:'{argv[1]}'�Ĳ���");
            }
        }
        else
        {
            throw new DataGetException("�����:" + argv[0]);
        }
        return true;
    }

    DataPack IDataUser.SerializedToDataPack()
    {
        throw new System.Exception("�����ļ��޷�ת��ΪDataPack");
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

