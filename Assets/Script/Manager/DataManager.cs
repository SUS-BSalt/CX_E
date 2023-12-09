using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public Dictionary<string,string> ConfigData;

    public Dictionary<string, string> CharacterName;

    public Dictionary<string, Dictionary<string, string>> CharacterData;
    public Dictionary<string, Dictionary<string, string>> LanguageData;

    public PlayerSaveData playerSaveData;


    public const string NODATA = "NoData";

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

