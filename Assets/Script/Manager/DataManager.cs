using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using UnityEngine;
using CI.QuickSave;
using UnityEngine.Events;

/// <summary>
/// 把所有的，需要与本地文件交换的，各个模块的各种数据类全扔这，让所有的
/// </summary>
public class DataManager : Singleton<DataManager>
{
    public UnityEvent Load;
    public UnityEvent Saved;


    public Dictionary<string,string> ConfigData;

    public Dictionary<string, string> CharacterName;

    public Dictionary<string, Dictionary<string, string>> CharacterData;
    public Dictionary<string, Dictionary<string, string>> LanguageData;

    public PlayerSaveData playerSaveData;

    public static readonly string NODATA = "NoData";

    QuickSaveReader reader { get; set; }
    QuickSaveWriter writer { get; set; }
    /// <summary>
    /// 因为这个类基于QuickSave插件搭建，所以方法也是基于其设计进行的拓展
    /// </summary>
    /// <param name="fileName">存档文件的名字，不用加后缀</param>
    public void LoadSaveFile(string fileName)
    {
        writer = QuickSaveWriter.Create(fileName);
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
    /// 将数据写入本地
    /// </summary>
    public void CommitData()
    {
        writer.Commit();
    }


    protected override void Awake()
    {
        base.Awake();
        LoadData();
        playerSaveData = new("SaveData01.json");
    }


    public void SaveData()
    {
        string ConfigPATH = Path.Combine(Application.streamingAssetsPath, "GameData/Config.json");
        string ConfigJson = JsonConvert.SerializeObject(ConfigData, Formatting.Indented);
        File.WriteAllText(ConfigPATH, ConfigJson);

    }
    public void LoadData()
    {
        string tempPath;
        string tempJson;

        string ConfigPATH = Path.Combine(Application.streamingAssetsPath, "GameData/Config.json");
        string ConfigJson = File.ReadAllText(ConfigPATH);
        ConfigData = JsonConvert.DeserializeObject<Dictionary<string, string>>(ConfigJson);

        var CharacterPATH = Path.Combine(Application.streamingAssetsPath, "GameData/CharacterMap.json");
        var CharacterJSON = File.ReadAllText(CharacterPATH);
        CharacterData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(CharacterJSON);

        tempPath = Path.Combine(Application.streamingAssetsPath, "GameData/LanguageMap.json");
        tempJson = File.ReadAllText(tempPath);
        LanguageData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(tempJson);

        GetNameData();
    }
    public void GetNameData()
    {
        string tempPath;
        string tempJson;

        tempPath = Path.Combine(Application.streamingAssetsPath, LanguageData[ConfigData["Language"]]["Path"],"CharacterNameMap.json");
        tempJson = File.ReadAllText(tempPath);
        CharacterName = JsonConvert.DeserializeObject<Dictionary<string, string>>(tempJson);
    }

}
public class PlayerSaveData
{
    public Dictionary<string, Dictionary<string, string>> JsonSaveData;
    public string JsonSavePath;

    public string bookPath;
    public int bookMark;

    int Money;

    Dictionary<string, int> PlayerItem;

    public PlayerSaveData(string SaveName)
    {
        LoadSaveData(SaveName);
    }
    public void SaveData()
    {
        bookPath = DialogManager.Instance.bookReader.BookPath;
        bookMark = DialogManager.Instance.bookMark;

        JsonSaveData["Dialog"]["BookPath"] = bookPath;
        JsonSaveData["Dialog"]["BookMark"] = bookMark.ToString();


        string tempJson = JsonConvert.SerializeObject(JsonSaveData, Formatting.Indented);
        File.WriteAllText(JsonSavePath, tempJson);
    }

    public void LoadSaveData(string SaveName)
    {
        string tempJson;

        JsonSavePath = Path.Combine(Application.streamingAssetsPath, "SaveData", SaveName);
        tempJson = File.ReadAllText(JsonSavePath);
        JsonSaveData = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(tempJson);

        bookPath = JsonSaveData["Dialog"]["BookPath"];
        bookMark = int.Parse(JsonSaveData["Dialog"]["BookMark"]);

        //DialogManager.Instance.SetBook(bookPath);
        //DialogManager.Instance.bookMark = bookMark;
        
    }

}

