using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    public Dictionary<string, ITableDataReader> Tables;
    /// <summary>
    /// �����ڱ༭�������ñ���Ϣ
    /// </summary>
    [SerializeField]
    private List<TableDataSO> tableDataSoList;

    protected override void Awake()
    {
        base.Awake();
        Tables = new();
        foreach (TableDataSO table in tableDataSoList)
        {
            Tables.Add(table.tableName, table.GetTable());
        }
    }
}

