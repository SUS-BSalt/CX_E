using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public Text text;
    public class TestDataClass
    {
        public int addss = 10;
        public ChildDataClass childData = new();
    }
    public class ChildDataClass
    {
        public int G = 0079;
    }
    TestDataClass data;
    private void Awake()
    {
        data = new();
        //reader = QuickSaveReader.Create("Testing");
        LoadSaveFile("Testing");
        Load();
        //print(Application.persistentDataPath);

    }
    public void Save()
    {
        UpdateData<TestDataClass>("class", data);
        CommitData();
        print("Commit!");
        updateText();
    }
    public void Load()
    {
        data = GetData<TestDataClass>("class");
        updateText();
    }
    public void addss()
    {
        data.addss += 1;
        updateText();

    }
    public void minss()
    {
        data.addss -= 1;
        updateText();

    }
    public void updateText()
    {
        text.text = data.addss.ToString() +"/n"+ data.childData.G.ToString();
    }


    QuickSaveReader reader { get; set; }
    QuickSaveWriter writer { get; set; }
    [SerializeField]
    private string currentSaveFileName;

    /// <summary>
    /// ��Ϊ��������QuickSave���������Է���Ҳ�ǻ�������ƽ��е���չ
    /// ÿ�����صĴ浵�ļ������䶯ʱ��Ӧ�����µ����������
    /// </summary>
    /// <param name="fileName">�浵�ļ������֣����üӺ�׺</param>
    public void LoadSaveFile(string fileName)
    {
        currentSaveFileName = fileName;

        reader = QuickSaveReader.Create(fileName);
    }

    public T GetData<T>(string key)
    {
        T data = reader.Read<T>(key);
        return data;
    }
    /// <summary>
    /// �������ݣ�д�뱾�ػ���Ҫ����CommitData
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">��</param>
    /// <param name="data">�κο������л��Ķ���</param>
    public void UpdateData<T>(string key, T data)
    {
        writer.Write<T>(key, data);
    }
    /// <summary>
    /// ������д�뱾�أ����������µ���LoadSaveFile
    /// </summary>
    public void CommitData()
    {
        writer.Commit();
        LoadSaveFile(currentSaveFileName);
    }
}
