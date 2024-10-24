using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSysMSG : MonoBehaviour
{
    public SaveField selectedSaveField;

    public Text text;
    public Text �浵ʱ��;
    public Text �浵����;

    public Button savebut;
    public Button loadbut;

    public void SelectSaveField(GameObject gameObject)
    {
        selectedSaveField = gameObject.GetComponent<SaveField>();

        text.text = selectedSaveField.header.DMSG;
        �浵ʱ��.text = selectedSaveField.header.LastModifyTime.ToString("yyyy.M.d  HH:mm");
        �浵����.text = selectedSaveField.header.�浵����;

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
    
}
