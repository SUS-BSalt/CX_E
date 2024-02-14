using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Test : MonoBehaviour
{
    public List<TestClass> testList;
    //public void Start()
    //{
    //    testList.Add(new TestClass());
    //    testList.Add(new TestClassB());
    //    SaveManager.Instance.SaveEvent.AddListener(Save);
    //    SaveManager.Instance.LoadEvent.AddListener(Load);
    //    SaveManager.Instance.SaveToFile();
    //    SaveManager.Instance.LoadFromFile();
    //    testList[0].Method();
    //    testList[1].Method();
    //}
    public void Save()
    {
        SaveManager.Instance.SaveData<List<TestClass>>("testSave", testList);
    }
    public void Load()
    {
        testList = SaveManager.Instance.LoadData<List<TestClass>>("testSave");
    }
}


