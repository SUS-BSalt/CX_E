using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : Singleton<GameDataManager>
{
    public Dictionary<string, ITableDataReader> Tables;
    /// <summary>
    /// 用于在编辑器中配置表信息
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

