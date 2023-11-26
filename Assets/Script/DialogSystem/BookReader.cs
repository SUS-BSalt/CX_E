using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeOpenXml;
using System.IO;
using System.Linq;

public class BookReader
{
    private ExcelPackage book;
    private ExcelWorksheet bookChapter;
    public int totalRows;
    public void ReadBookFile(string bookPath)
    {
        book = new ExcelPackage(new FileInfo(bookPath));
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
    public string GetConcept(int row,int columns)
    {
        if (row >= totalRows)
        {
            return "������ͼԽ����ʵ�ǰ���ֱ�������֮����У���������һ��bug";
        }
        return bookChapter.Cells[row,columns].ToString();
    }
}
