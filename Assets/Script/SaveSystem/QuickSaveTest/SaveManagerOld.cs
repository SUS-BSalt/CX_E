using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// �������ļ�����Ϸ�ڵ����ݽ�����
/// ��Ϸ�ڵ����ݽ������Բ����������Reader��
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
/// 
/// (1.29.2024)��İɵ�����������밴���ȡ��
/// �����ȡ�����ɵ�ѡ��浵�ļ�ֻ��ѡһ��������ϵͳ���ӶȻ����Ӻܶ࣬һ�����������һ��û��ȫ���꣬��ʱ������Ѵ浵�浽��һ���ط�ȥ��ʣ��û�����������զ�죿
/// ��2024.9.22���Ҵ��������浵ϵͳ����������ڴ���ά��һ��������������������Ϸ������ͨ����������⽫���ݱ�������ص��������ڣ�
/// ������ҽ��д浵����ʱ��������������Ӳ�̽����������ȿ��԰����ȡ��Ҳ������ѡ��浵�ļ���
/// ��2024.9.23���Ҽᶨ��������뷨���������ģʽ�Ȳ���Ч�ֲ�����
/// û��һ�����ݹ����������й������ݣ������ñ���רע��gameplay�ļһ��Լ���һ��������ά������̫��ֱ��
/// ����������ô����������̫���ˣ�ÿ��Դ�ļ����޸ľ���Ҫ���¼��أ�����ͬһ�ļ��Ķ�ȡ��д��𿪳�����ʵ��
/// </summary>
public class SaveManager_old : Singleton<SaveManager_old>
{
    public UnityEvent LoadEvent;
    public UnityEvent SaveEvent;

    [SerializeField]
    private SaveField currentSaveField;

    /// <summary>
    /// ��ǿ³�������
    /// </summary>
    [SerializeField]
    private bool isSaveFieldChanged;
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
        //isSaveFieldChanged = true;
    }
    
    public void LoadFromFile()
    {
        //print("fuck debug");
        
        reader = QuickSaveReader.Create(currentSaveField.gameObject.name);
        LoadEvent?.Invoke();
        //print("Load From "+ currentSaveField.gameObject.name);
    }
    public void SaveToFile()
    {
        //print("Save To " + currentSaveField.gameObject.name);
        //����д����
        writer = QuickSaveWriter.Create(currentSaveField.gameObject.name);
        //���²�д��浵ͷ��Ϣ
        //currentSaveField.CreatHeader();
        SaveData<SaveDataHeader>("Header", currentSaveField.header);
        //������ģ��������ݱ���
        //print("��ʼ����");
        SaveEvent?.Invoke();
        //�ύ����
        writer.Commit();
        //print("�����ύ");
        //��������savefile���ļ�ͷ
        //currentSaveField.LoadHeader();
        //print(currentSaveField.header.LastModifyTime.ToString());
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
        //if (isSaveFieldChanged)
        //{
        //    reader = QuickSaveReader.Create(currentSaveField.gameObject.name);
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
    }
}
