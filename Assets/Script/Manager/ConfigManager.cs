using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    public ConfigData data;

    protected override void Awake()
    {
        base.Awake();
        LoadData();
    }


    public void SaveData()
    {
        var PATH = Path.Combine(Application.streamingAssetsPath, "SaveData/Config.json");
        var JSON = JsonUtility.ToJson(data,true);
        try
        {
            File.WriteAllText(PATH,JSON);
            print("SuccessSave");
        }
        catch (FileNotFoundException e)
        {

        }
    }
    public void LoadData()
    {
        data = new();
        var PATH = Path.Combine(Application.streamingAssetsPath, "SaveData/Config.json");
        var JSON = File.ReadAllText(PATH);
        data = JsonUtility.FromJson<ConfigData>(JSON);
        //print("SuccessLoad");
    }
    public void PrintData()
    {
        print(data.Language);
    }
    public void WriteData()
    {
        data.Language = 2;
        PrintData();
    }
}

[Serializable]
public class ConfigData
{
    public int Language = 1;
    public float MasterVolume = 1;
    public float MusicVolume = 1;
    public float EffectVolme = 1;
    public int ResolutionX = 1280;
    public int ResolutionY = 720;
    public bool isFullScreen = false;
};

