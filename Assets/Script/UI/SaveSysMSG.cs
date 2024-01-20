using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSysMSG : MonoBehaviour
{
    public SaveField selectedSaveField;

    public void SelectSaveField(GameObject gameObject)
    {
        selectedSaveField = gameObject.GetComponent<SaveField>();
        //print(gameObject.name);
    }
    public void NewGame()
    {

    }
    public void Load()
    {
        SaveManager.Instance.ChangeSaveField(selectedSaveField);
        SaveManager.Instance.LoadFromFile();
    }
    public void Save()
    {
        SaveManager.Instance.ChangeSaveField(selectedSaveField);
        SaveManager.Instance.SaveToFile();
    }
}
