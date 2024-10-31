using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float ©чак;
    private void Start()
    {
        //print(DataManager.Instance.GetData<int>("Profile", "TestTable", "1", "1"));
        //DataManager.Instance.SetData<int>(5,"Profile", "TestTable", "1", "1");
        //print(DataManager.Instance.GetData<int>("Profile", "TestTable", "1", "1"));
        //DataManager.Instance.SetData<int>(2, "Profile", "TestTable", "1", "1");
        //print(DataManager.Instance.GetData<int>("Profile", "TestTable", "1", "1"));
    }
    private void Awake()
    {
        print("fff");
    }
    private void OnEnable()
    {
        print("eee");
    }
}


