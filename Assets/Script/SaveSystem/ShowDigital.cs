using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.Networking;

public class ShowDigital : MonoBehaviour
{
    public string bookpath;
    public ExcelPackage book;
    public ExcelWorksheet bookChapter;
    public int totalRows;

    public Text text;
    public Text pathtext;
    public Text fucktext;
    public Text SBtext;
    public TestSaveSO so;
    public int testInt;
    private void Start()
    {
        fucktext.text = "fuck";
        ReadBookFile(bookpath);
        //text.text = bookChapter.Cells[1, 1].Value.ToString();
    }
    public void UpdateDigital()
    {
        testInt += 1;
        text.text = bookChapter.Cells[testInt, 1].Value.ToString();
    }
    #region ¶ÁÈ¡ÎÄ¼þ
    public void ReadBookFile(string bookPath)
    {
        string _path = Application.streamingAssetsPath + "/" + bookPath;
        pathtext.text = _path;
        try
        {
            book = new ExcelPackage(new FileInfo(_path));
            //print(_path);
            bookChapter = book.Workbook.Worksheets[1];
            totalRows = GetLastUsedRow(bookChapter);
        }
        catch
        {
            fucktext.text = "FUCK!!!!";
        }
        SBtext.text = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "Book/s.txt"));
    }
    public void ChangeBookChapter(int chapterIndex)
    {
        bookChapter = book.Workbook.Worksheets[chapterIndex];
        totalRows = GetLastUsedRow(bookChapter);
    }
    int GetLastUsedRow(ExcelWorksheet sheet)
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
    #endregion

}
