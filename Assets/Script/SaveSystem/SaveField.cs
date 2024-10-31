using CI.QuickSave;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ������������ʵ���ļ�����Ϸ�ڲ����࣬�Ƚ�����
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
        SaveFileName = gameObject.name;
        text.text = gameObject.name;
    }
    public bool LoadHeader()
    {
        header = new SaveDataHeader();
        try
        {
            QuickSaveReader reader = QuickSaveReader.Create(gameObject.name);

            if (reader.TryRead<SaveDataHeader>("SaveDataHeader", out header))
            {
                canSave = true;
                canLoad = true;
                text.text = header.DMSG;
                return true;
            }
            else
            {
                header.DMSG = "�ļ���";
                canSave = true;
                canLoad = false;
                text.text = header.DMSG;
                return false;
                //print(gameObject.name + "������ļ���");
            }
        }
        catch (QuickSaveException)
        {
            header.isExist = false;
            canSave = true;
            canLoad = false;
            header.DMSG = "�մ浵λ";
            text.text = header.DMSG;
            return false;
            //print(gameObject.name + "�ļ�������");
        }
        
    }
    private void Start()
    {
        Init();
        if(LoadHeader())
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
    public bool isExist = false;
    public DateTime LastModifyTime;
    public string DMSG = "��";
    public string �浵���� = "";
}
