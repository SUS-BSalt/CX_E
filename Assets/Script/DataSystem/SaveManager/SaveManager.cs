using CI.QuickSave;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]
    private SaveField currentSaveField;
    [SerializeField]
    private SaveField AutoSaveFiled;

    public UnityEvent SaveFiledChanged;

    public SaveDataHeader Header { get { return currentSaveField.header; } }

    public void ChangeSaveField(SaveField saveField)
    {
        currentSaveField = saveField;
        SaveFiledChanged.Invoke();
        //isSaveFieldChanged = true;
    }
    public void LoadFromFile()
    {
        QuickSaveReader reader = QuickSaveReader.Create(currentSaveField.gameObject.name);
        DataManager.Instance.LoadFromFile(reader.Read<string>("SaveData"));
    }
    public void SaveToFile()
    {
        QuickSaveWriter writer = QuickSaveWriter.Create(currentSaveField.gameObject.name);
        SaveDataHeader Header = CreatHeader();

        writer.Write<SaveDataHeader>("SaveDataHeader", CreatHeader());
        writer.Write<string>("SaveData", DataManager.Instance.SaveToFile());
        writer.Commit();
    }
    public SaveDataHeader CreatHeader()
    {
        SaveDataHeader header = new();

        header.LastModifyTime = DateTime.Now;
        string formattedDate = header.LastModifyTime.ToString("yyyy年M月d日  HH:mm");

        header.DMSG = formattedDate;

        if (currentSaveField == AutoSaveFiled)
        {
            header.存档类型 = "自动保存";
        }
        else
        {
            header.存档类型 = "手动保存";
        }

        return header;
    }

}


