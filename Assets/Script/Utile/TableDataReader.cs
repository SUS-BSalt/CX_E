using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using Unity.VisualScripting;
using System.Linq;

public interface ITableDataReader
{
    public T GetData<T>(int row, int col);
    public int TotalRows { get; }
}

public class XlsxReader : ITableDataReader
{
    public string FilePath { get; private set; }
    public int TotalRows { get { return totalRows; } }
    private int totalRows;
    private ExcelWorksheet excelWorksheet;
    private int sheet;
    public T GetData<T>(int row, int col)
    {
        T data;
        try
        {
            data = excelWorksheet.Cells[row, col].ConvertTo<T>();
            return data;
        }
        catch
        {

            Debug.Log($"在{FilePath}文件第{sheet}Sheet-{row}行-{col}列处的数据无法读取为：“" + typeof(T).FullName + "”类型");
            throw new System.Exception("DataConvertFaile");
        }
    }
    public XlsxReader(string _FilePath,int sheet)
    {
        ReadXlsxFile(_FilePath,sheet);
    }
    public void ReadXlsxFile(string _FilePath,int sheet)
    {
        this.sheet = sheet;
        //Debug.Log(Application.streamingAssetsPath);
        //Debug.Log(_bookPath);
        var excelPackage = new ExcelPackage(new FileInfo(_FilePath));
        excelWorksheet = excelPackage.Workbook.Worksheets[sheet];
        totalRows = GetLastUsedRow(excelWorksheet);
    }
    private int GetLastUsedRow(ExcelWorksheet sheet)
    {
        var row = sheet.Dimension.End.Row;
        while (row >= 1)
        {
            var range = sheet.Cells[row, 1, row, sheet.Dimension.End.Column];
            if (range.Any(c => !string.IsNullOrEmpty(c.Text)))
            {
                break;
            }
            row--;
        }
        return row;
    }
}

