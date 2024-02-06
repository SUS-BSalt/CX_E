using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM : MonoBehaviour
{
    public Dictionary<string, Test> dict;
    public void Start()
    {
        dict = new();
        dict.Add("0",new Test());
        dict.Add("1", new TestChild());
        foreach (Test t in dict.Values)
        {
            print(t.GetType());
        }
    }
    public void save()
    {
        SaveManager.Instance.SaveData("test", dict);
    }
    public void load()
    {
        dict = SaveManager.Instance.LoadData<Dictionary<string, Test>>("test");
        foreach(Test t in dict.Values)
        {
            print(t.GetType());
        }
    }

}
