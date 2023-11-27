using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    public ConfigData data;

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
        print("SuccessLoad");
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
    public float MasterVolume;
    public float MusicVolume;
    public float EffectVolme;
}
