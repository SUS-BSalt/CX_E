using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSysMSG : MonoBehaviour
{
    public SaveField selectedSaveField;

    public Text text;

    public Button savebut;
    public Button loadbut;


    public void SelectSaveField(GameObject gameObject)
    {
        selectedSaveField = gameObject.GetComponent<SaveField>();

        text.text = gameObject.name;

        if (savebut != null)
        {
            savebut.interactable = selectedSaveField.canSave;
        }
        if (loadbut != null)
        {
            loadbut.interactable = selectedSaveField.canLoad;
        }
        
        //print(gameObject.name);
    }
    public void NewGame()
    {

    }
    public void Load()
    {
        print("SaveSysMSG Load");
        SaveManager.Instance.ChangeSaveField(selectedSaveField);
        SaveManager.Instance.LoadFromFile();
    }
    public void Save()
    {
        //print("SaveSysMSG Save");
        SaveManager.Instance.ChangeSaveField(selectedSaveField);
        SaveManager.Instance.SaveToFile();
    }
}
