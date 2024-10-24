using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : Singleton<DataManager>
{
    private DataManagerBase worker;

    public UnityEvent Loaded;
    public void RegisterDataUser(IDataUser dataUser){ worker.RegisterDataUser(dataUser);}
    public DataPack GetDataPack(string packName) { return worker.GetDataPack(packName); }
    public T GetData<T>(params string[] argv) { return worker.GetData<T>(argv); }
    public bool SetData<T>(T value, params string[] argv) { return worker.SetData<T>(value, argv); }
    
    public void LoadFromFile(string SaveData)
    {
        worker.Init(SaveData);
        Loaded.Invoke();
    }
    public string SaveToFile()
    {
        return worker.SerializeDatas();
    }

    protected override void Awake()
    {
        base.Awake();
        worker = new();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
