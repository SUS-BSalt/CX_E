using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSysMSG : MonoBehaviour
{
    public SaveField selectedSaveField;

    public Text text;
    public Text 存档时间;
    public Text 存档类型;

    public Button savebut;
    public Button loadbut;

    public void SelectSaveField(GameObject gameObject)
    {
        selectedSaveField = gameObject.GetComponent<SaveField>();
        selectedSaveField.LoadHeader();

        if (selectedSaveField.header.isExist)
        {
            text.text = selectedSaveField.header.DMSG;
            存档时间.text = selectedSaveField.header.LastModifyTime.ToString("yyyy.M.d  HH:mm");
            存档类型.text = selectedSaveField.header.存档类型;
        }
        else
        {
            text.text = selectedSaveField.header.DMSG;
            存档时间.text = "";
            存档类型.text = "";
        }
        if(selectedSaveField.SaveFileName == SaveManager.Instance.AutoSaveFiled.SaveFileName)
        {
            selectedSaveField.canSave = false;
        }

        if (savebut != null)
        {
            savebut.interactable = selectedSaveField.canSave;
        }
        if (loadbut != null)
        {
            loadbut.interactable = selectedSaveField.canLoad;
        }
        SaveManager.Instance.ChangeSaveField(selectedSaveField);
        
        //print(gameObject.name);
    }
    public void Load()
    {
        SaveManager.Instance.LoadFromFile();
    }
    public void Save()
    {
        SaveManager.Instance.SaveToFile();
    }
    
}
