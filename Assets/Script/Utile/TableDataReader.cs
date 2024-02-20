using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using Unity.VisualScripting;
using System.Linq;

public interface ITableDataReader
{
    public T GetData<T>(int row, int col, int sheet);
    public int SheetLength(int sheet);
}

public class XlsxReader : ITableDataReader
{
    public string FilePath { get; private set; }
    private ExcelPackage excelPackage;
    public int totalRows;
    public T GetData<T>(int row, int col, int sheet = 1)
    {
        T data;
        try
        {
            data = excelPackage.Workbook.Worksheets[sheet].Cells[row, col].ConvertTo<T>();
            return data;
        }
        catch
        {

            Debug.Log($"在{FilePath}文件{row}行{col}列{sheet}章处的数据无法读取为：“" + typeof(T).FullName + "”类型");
            throw new System.Exception("DataConvertFaile");
        }
    }
    public XlsxReader(string _FilePath)
    {
        ReadXlsxFile(_FilePath);
    }
    public void ReadXlsxFile(string _FilePath)
    {
        FilePath = _FilePath;
        string trueFilePath = Path.Combine(Application.streamingAssetsPath, _FilePath);
        //Debug.Log(Application.streamingAssetsPath);
        //Debug.Log(_bookPath);
        excelPackage = new ExcelPackage(new FileInfo(trueFilePath));
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
    public int SheetLength(int sheet)
    {
        return GetLastUsedRow(excelPackage.Workbook.Worksheets[sheet]);
    }
}
