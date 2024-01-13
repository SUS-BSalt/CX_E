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
    /// 因为这个类基于QuickSave插件搭建，所以方法也是基于其设计进行的拓展
    /// 每当本地的存档文件发生变动时，应当重新调用这个方法
    /// </summary>
    /// <param name="fileName">存档文件的名字，不用加后缀</param>
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
    /// 更新数据，写入本地还需要调用CommitData
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key">键</param>
    /// <param name="data">任何可以序列化的东西</param>
    public void UpdateData<T>(string key, T data)
    {
        writer.Write<T>(key, data);
    }
    /// <summary>
    /// 将数据写入本地，别忘了重新调用LoadSaveFile
    /// </summary>
    public void CommitData()
    {
        writer.Commit();
        LoadSaveFile(currentSaveFileName);
    }
}
