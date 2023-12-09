using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using System.Linq;

public class BookReader
{
    public BookReader(string BookPath)
    {
        ReadBookFile(BookPath);
    }

    public string BookPath;

    private ExcelPackage book;
    private ExcelWorksheet bookChapter;
    public int totalRows;
    public void ReadBookFile(string bookPath)
    {
        BookPath = bookPath;
        string _bookPath = Path.Combine(Application.streamingAssetsPath, bookPath);
        book = new ExcelPackage(new FileInfo(_bookPath));
        bookChapter = book.Workbook.Worksheets[1];
        totalRows = GetLastUsedRow(bookChapter);
    }
    public void ChangeBookChapter(int chapterIndex)
    {
        bookChapter = book.Workbook.Worksheets[chapterIndex];
        totalRows = GetLastUsedRow(bookChapter);
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

    /// <summary>
    /// 取得书中内容
    /// </summary>
    /// <param name="row">从1开始</param>
    /// <param name="columns">从1开始</param>
    /// <returns></returns>
    public string GetConcept(int row,int columns)
    {
        try
        {
            return bookChapter.Cells[row,columns].Value.ToString();
        }
        catch
        {
            return "NoData";
        }
    }


}
