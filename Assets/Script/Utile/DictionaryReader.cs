using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using static TestMissionItem;

public interface IDictionaryDataReader
{
    public T GetData<T>(string key);
}
public class JsonReader : IDictionaryDataReader
{
    public string FilePath { get; private set; }

    public int totalRows;
    public T GetData<T>(string key)
    {
        //JsonConvert.DeserializeObject<TestMissionItemData>();
        throw new System.NotImplementedException();
    }
}
