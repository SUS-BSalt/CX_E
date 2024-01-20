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
    public bool canSave;
    public bool canLoad;
    public bool isSaveFileExist;

    public Text text;

    public void LoadHeader()
    {
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
                print(gameObject.name + "大概是文件损坏");
            }
        }
        catch (QuickSaveException)
        {
            print(gameObject.name + "文件不存在");
        }
    }

    public void CreatHeader()
    {
        header = new SaveDataHeader();
        header.LastModifyTime = DateTime.Now;
    }

    private void Awake()
    {
        LoadHeader();
    }

}
