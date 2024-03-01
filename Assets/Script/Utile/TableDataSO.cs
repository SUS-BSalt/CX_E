using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "TableDataSO", menuName = "GameData/TableData")]
public class TableDataSO : ScriptableObject
{
    /// <summary>
    /// 
    /// </summary>
    public string filePath;
    public string tableName;
    /// <summary>
    /// ����ļ�������xlsx����Ҫ������Ǳ��ڵڼ���sheet
    /// </summary>
    public int sheet = 1;
    public enum FileType { xlsx }
    public FileType fileType;
    public ITableDataReader GetTable()
    {
        string trueFilePath = Path.Combine(Application.streamingAssetsPath, filePath);
        //Debug.Log(trueFilePath);
        switch (fileType)
        {
            case (FileType.xlsx):
                {
                    return new XlsxReader(trueFilePath, sheet);
                }
        }
        throw new System.Exception("can't get table");
    }
}
