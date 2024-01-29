using CI.QuickSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 它是用来链接实体文件到游戏内部的类，比较特殊
/// </summary>
public class SaveField : MonoBehaviour
{
    [SerializeField]
    public string SaveFileName;
    public SaveDataHeader header;
    public bool canSave = true;
    public bool canLoad = true;
    public bool isSaveFileExist;

    public Text text;

    public void LoadHeader()
    {
        canSave = true;
        canLoad = true;
        text.text = "NoData";
        QuickSaveReader reader;
        try
        {
            reader = QuickSaveReader.Create(gameObject.name);

            if (reader.TryRead<SaveDataHeader>("Header", out header))
            {
                text.text = header.LastModifyTime.ToString();
                return;
            }
            else
            {
                canLoad = false;
                //print(gameObject.name + "大概是文件损坏");
            }
        }
        catch (QuickSaveException)
        {
            isSaveFileExist = false;
            canLoad = false;
            //print(gameObject.name + "文件不存在");
        }
    }

    public void CreatHeader()
    {
        header = new SaveDataHeader();
        header.LastModifyTime = DateTime.Now;
    }

    private void OnEnable()
    {
        LoadHeader();
    }

}
