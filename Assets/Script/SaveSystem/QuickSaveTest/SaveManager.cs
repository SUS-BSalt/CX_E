using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// �������ļ�����Ϸ�ڵ����ݽ�����
/// ģ�������ݽ������Բ����������Reader��
/// ÿ��ģ�����б�д����ʱ�����ʱ�ķ��������ֱ����LoadEvent��SaveEvent�ϡ�
/// ��ȡ���ݿ�����LoadData���������ݿ�����SaveData��
/// 
/// ��������SaveField������ֱ�����ӱ����ļ�����Ϸ��һ��ʵ����Load��Save���ǶԵ�ǰ��SaveField���в���
/// 
/// ʹ����QuickSave���������˼·���ڶ�ȡʱ����һ��Reader����д��ʱ����һ��Writer���������ڴ����ָ��*��һ����*��
/// Ҳ����˵�������ͬʱ������һ��Reader��һ��Writer�����޸������ݲ���Writerִ��д�����ʹ��Reader�ᷢ�֣���ȡ����Ȼ�Ǿ����ݡ�
/// ����ÿ����ִ����һ��д�룬�����Ҫ���´���һ��Reader��
/// �����ҳ������Ǻ�1.13.2024����������Reader��פ������Writter��Ϊ��ʱ������������Ҫ����ʱ����������д�������Reader��
/// ��ʹ��Readerʱ�����ȼ�����ǣ���Ϊ�࣬�����¼���һ���ļ����ɾ��Ļ��������ʹ�á�
/// </summary>
public class SaveManager : Singleton<SaveManager>
{
    public UnityEvent LoadEvent;
    public UnityEvent SaveEvent;

    [SerializeField]
    private SaveField currentSaveField;

    /// <summary>
    /// ��ǿ³�������
    /// </summary>
    [SerializeField]
    private bool isReaderDirty;
    QuickSaveReader reader { get; set; }
    QuickSaveWriter writer { get; set; }

    public static readonly string NODATA = "NoData";

    /// <summary>
    /// ��Ϊ��������QuickSave���������Է���Ҳ�ǻ�������ƽ��е���չ
    /// ÿ�����صĴ浵�ļ������䶯ʱ��Ӧ�����µ����������
    /// </summary>
    /// <param name="fileName">�浵�ļ������֣����üӺ�׺</param>

    public void ChangeSaveField(SaveField saveField)
    {
        currentSaveField = saveField;
    }

    public void LoadFromFile()
    {
        print("fuck debug");
        
        reader = QuickSaveReader.Create(currentSaveField.gameObject.name);
        isReaderDirty = false;
        LoadEvent?.Invoke();
        print("Load From "+ currentSaveField.gameObject.name);
    }
    public void SaveToFile()
    {
        print("Save To " + currentSaveField.gameObject.name);
        //����д����
        writer = QuickSaveWriter.Create(currentSaveField.gameObject.name);
        //���²�д��浵ͷ��Ϣ
        currentSaveField.CreatHeader();
        SaveData<SaveDataHeader>("Header", currentSaveField.header);
        //������ģ��������ݱ���
        print("��ʼ����");
        SaveEvent?.Invoke();
        //�ύ����
        writer.Commit();
        print("�����ύ");
        //��������savefile���ļ�ͷ
        currentSaveField.LoadHeader();
        //print(currentSaveField.header.LastModifyTime.ToString());
        //���Ǵ浵
        isReaderDirty = true;
    }


    /// <summary>
    /// �Ӵ浵��ȡ�����ݣ�ģ�������ݽ������Բ�Ҫ�������
    /// </summary>
    /// <typeparam name="T">�������ͣ��������κ������л�������</typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    /// <exception cref="System.Exception">���û�ܼ��ص����ݣ����׳��쳣</exception>
    public T LoadData<T>(string key)
    {
        T data;
        //if (isReaderDirty)
        //{
        //    reader.Reload();
        //    //LoadFromFile();
        //}

        if (reader.TryRead<T>(key,out data))
        {
            return data;
        }
        throw new System.Exception(NODATA);
    }

    /// <summary>
    /// �������ݣ�����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">��</param>
    /// <param name="data">�κο������л��Ķ���</param>
    public void SaveData<T>(string key, T data)
    {
        writer.Write<T>(key, data);
        isReaderDirty = true;
    }
}
