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
    public void Init()
    {
        canSave = true;
        canLoad = true;
        text.text = gameObject.name;
    }
    public bool LoadHeader(out SaveDataHeader header)
    {
        header = new SaveDataHeader();
        try
        {
            QuickSaveReader reader = QuickSaveReader.Create(gameObject.name);

            if (reader.TryRead<SaveDataHeader>("SaveDataHeader", out header))
            {
                return true;
            }
            else
            {
                header.DMSG = "文件损坏";
                return false;
                //print(gameObject.name + "大概是文件损坏");
            }
        }
        catch (QuickSaveException)
        {
            header.DMSG = "空存档位";
            return false;
            //print(gameObject.name + "文件不存在");
        }
    }
    private void Start()
    {
        Init();
        if(LoadHeader(out header))
        {
            text.text = header.DMSG;
        }
        else
        {
            canLoad = false;
            text.text = header.DMSG;
        }
    }
}

public class SaveDataHeader
{
    public DateTime LastModifyTime;
    public string DMSG = "空";
    public string 存档类型 = "";
}
