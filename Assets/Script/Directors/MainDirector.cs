using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public interface IDirector
{
    public IPerformance currentPerformance { get; set; }
}
public interface IPerformance
{
    public IDirector BaseDirector { get; set; }
    public void PerformanceStart();
    public void PerformanceEnd();
}

public class MainDirector : Singleton<MainDirector>, IDirector
{
    public IPerformance currentPerformance { get => _currentPerformance; set => _currentPerformance = value; }
    private IPerformance _currentPerformance;
    public int Date { get => _Date; private set => _Date = value; }
    private int _Date;
    [SerializeField]
    private TableDataSO GameScriptSO;
    private ITableDataReader GameScript;
    protected override void Awake()
    {
        base.Awake();
        GameScript = GameScriptSO.GetTable();
    }
    private void Start()
    {
        SaveManager.Instance.SaveEvent.AddListener(OnSave);
        SaveManager.Instance.LoadEvent.AddListener(OnLoad);
    }
    public void OnLoad()
    {
        _Date = SaveManager.Instance.LoadData<int>("Date");
    }
    public void OnSave()
    {
        SaveManager.Instance.SaveData<int>("Date",_Date);
    }
    public void StartNewDay(int Date)
    {
        _Date = Date;
        ExecEvent(GameScript.GetData<string>(_Date + 1, 2));
    }
    public void ExecEvent(string __EventString)
    {
        List<string> _EventString = EventString.Unpack(__EventString);
        switch (_EventString[0])
        {
            ///If-{condition}-{true branch}-{fales branch}
            case ("If"):
                {
                    IfEvent(_EventString.Skip(1).ToList());
                    break;
                }
        }
    }
    public void IfEvent(List<string> _EventString)
    {
        ///{conditionType-argvs}-{true branch}-{fales branch}
        List<string> _conditionString = EventString.Unpack(_EventString[0]);
        bool _conditionResult;
        switch (_conditionString[0])
        {
            ///If-{condition}-{true branch}-{fales branch}
            case ("Bigger"):
                {
                    _conditionResult = Bigger(_conditionString.Skip(1).ToList());
                    break;
                }
            default:
                {
                    _conditionResult = false;
                    break;
                }
        }
        if (_conditionResult)
        {
            ExecEvent(_EventString[1]);
        }
        else
        {
            ExecEvent(_EventString[2]);
        }
    }
    public bool Bigger(List<string> _EventString)
    {
        ///{float-1}-{Find-Date}
        ///{float-5}-{Inventory-Player-ItemID}
        return Find(_EventString[1]) > Find(_EventString[2]);
    }
    public float Find(string _EventString)
    {
        List<string> argvs = EventString.Unpack(_EventString);
        switch (argvs[0])
        {
            case ("float"):
                {
                    ///float-1
                    return float.Parse(argvs[1]);
                }
            case ("Inventory"):
                {
                    ///Inventory-Player-ItemID
                    return (float)InventoryManager.Instance.Inventorys[argvs[1]].FindItem(argvs[2]);
                }
            default:
                {
                    return 0;
                }
        }
    }
}
