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
                header.DMSG = "�ļ���";
                return false;
                //print(gameObject.name + "������ļ���");
            }
        }
        catch (QuickSaveException)
        {
            header.DMSG = "�մ浵λ";
            return false;
            //print(gameObject.name + "�ļ�������");
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
    public string DMSG = "��";
    public string �浵���� = "";
}
