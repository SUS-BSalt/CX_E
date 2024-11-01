using CI.QuickSave;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField]
    private SaveField currentSaveField;
    [SerializeField]
    public SaveField AutoSaveFiled;

    public UnityEvent SaveFiledChanged;
    public UnityEvent Loaded;

    public SaveDataHeader Header { get { return currentSaveField.header; } }

    public void ChangeSaveField(SaveField saveField)
    {
        currentSaveField = saveField;
        SaveFiledChanged.Invoke();
        //isSaveFieldChanged = true;
    }
    public void LoadFromFile()
    {
        QuickSaveReader reader = QuickSaveReader.Create(currentSaveField.SaveFileName);
        DataManager.Instance.LoadFromFile(reader.Read<string>("SaveData"));
        DataManager.Instance.RegisterDataUser(ProfileDataManager.Instance);
        Loaded.Invoke();
    }
    public void SaveToFile()
    {
        QuickSaveWriter writer = QuickSaveWriter.Create(currentSaveField.SaveFileName);
        SaveDataHeader Header = CreatHeader();

        writer.Write<SaveDataHeader>("SaveDataHeader", CreatHeader());
        writer.Write<string>("SaveData", DataManager.Instance.SaveToFile());
        writer.Commit();
        currentSaveField.LoadHeader();
    }
    public SaveDataHeader CreatHeader()
    {
        SaveDataHeader header = new();
        header.isExist = true;
        header.LastModifyTime = DateTime.Now;
        string formattedDate = header.LastModifyTime.ToString("yyyy��M��d��  HH:mm");

        header.DMSG = formattedDate;

        if (currentSaveField.SaveFileName == AutoSaveFiled.SaveFileName)
        {
            header.�浵���� = "�Զ�����";
        }
        else
        {
            header.�浵���� = "�ֶ�����";
        }

        return header;
    }
    public void AutoSave()
    {
        ChangeSaveField(AutoSaveFiled);
        QuickSaveWriter writer = QuickSaveWriter.Create(AutoSaveFiled.gameObject.name);
        SaveDataHeader Header = CreatHeader();
        writer.Write<SaveDataHeader>("SaveDataHeader", CreatHeader());
        writer.Write<string>("SaveData", DataManager.Instance.SaveToFile());
        writer.Commit();
    }

}


