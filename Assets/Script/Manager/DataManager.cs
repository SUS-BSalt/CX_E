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

    protected override void Awake()
    {
        base.Awake();
        LoadData();
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

