using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Starting;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json;


public class MainDirector : Singleton<MainDirector>, IDirector, IDataUser
{
    public IPerformance currentPerformance { get; set; }
    [SerializeField]
    private TableDataSO GameScriptSO;
    private ITableDataReader GameScript;

    public DialogManager Dialog;
    public MenuBase DialogMenu;
    public TradeManager Trader;
    public InventoryManager Inventory;
    public MenuBase Phone;
    public GameObject LoadingMenu;
    public MenuBase MainMenu;

    public MainDirectorData GlobalData;

    public int Date { get=> GlobalData.Date; set=> GlobalData.Date=value; }

    string IDataUser.PackName { get { return "GlobalData"; } }

    bool IDataUser.IndividualizedSave { get { return true; } }

    protected override void Awake()
    {
        base.Awake();
        GlobalData = new();
        DataManager.Instance.RegisterDataUser(this);
        GameScript = GameScriptSO.GetTable();
    }
    private void Start()
    {
        //SaveManager.Instance.SaveEvent.AddListener(OnSave);
        //SaveManager.Instance.LoadEvent.AddListener(OnLoad);
        Trader.TradeEnd += TradeEndMethod;
        SaveManager.Instance.Loaded.AddListener(OnLoad);
    }
    public void OnLoad()
    {
        GlobalData = JsonConvert.DeserializeObject<MainDirectorData>( DataManager.Instance.GetDataPack("GlobalData").DeserializeData);
        StopEveryThing();
        Inventory.Init();
        if (GlobalData.currentPerformance == "Dialog")
        {
            Dialog.PerformanceStart(this);
            Dialog.Load();
            MainMenu.OnExit(Phone);
        }
    }
    public void StopEveryThing()
    {
        Dialog.PerformanceEnd();
        Phone.gameObject.SetActive(false);
    }
    public void NewGame()
    {
        GlobalData = new();
        LoadingMenu.SetActive(true);
        this.Inventory.New();
        MainMenu.OnExit(Phone);
        //currentPerformance = Dialog;
        //Dialog.PerformanceStart(this);
        //Dialog.Init("test.xlsx");
        //print(Dialog.data.bookMark + "why?");
        //print(Dialog.data.currentBookChapter+"why?");
        Date = 1;
        NextStep();
        //Dialog.OnClick();
        //print("fuck");
        LoadingMenu.SetActive(false);
    }
    public void TestScence()
    {
        GlobalData = new();
        LoadingMenu.SetActive(true);
        this.Inventory.New();
        Date = 1;
        MainMenu.OnExit(Phone);
        StartDialogPerformance("test.xlsx");
        //print("fuck");
        LoadingMenu.SetActive(false);
    }
    public void TradeEndMethod()
    {
        //print("s");
        Dialog.NextStep();
    }
    public void ExecEvents(string __EventString)
    {
        List<List<string>> Events = EventString.UnpackComplex(__EventString);
        foreach(List<string> Eventpach in Events)
        {
            ExecEvent(Eventpach);
        }
    }
    public void ExecEvent(List<string> eventArgv)
    {
        switch (eventArgv[0])
        {
            case ("If"):
                {
                    ///If-{condition}-{true branch}-{fales branch}
                    IfEvent(eventArgv.Skip(1).ToList());
                    break;
                }
            case ("Dialog"):
                {
                    ///Dialog-BookPath
                    StartDialogPerformance(eventArgv[1]);
                    break;
                }
            case ("DialogSet"):
                {
                    Dialog.ExecEvent(eventArgv.Skip(1).ToList());
                    break;
                }
            case ("Add"):
                {
                    //Add-InventoryID-ItemID-{ProfileJson}-number
                    InventoryManager.Instance.AddItemToInventoryWithSlotAuto(eventArgv[1], int.Parse(eventArgv[2]), eventArgv[3], int.Parse(eventArgv[4]));
                    //print("add");
                    break;
                }
            case ("Remove"):
                {
                    //Remove-InventoryID-ItemID-{ProfileJson}-number
                    InventoryManager.Instance.RequestItemFromInventoryWithSlotAuto(eventArgv[1], int.Parse(eventArgv[2]), eventArgv[3], int.Parse(eventArgv[4]));
                    //print("add");
                    break;
                }
            case ("Trade"):
                {
                    //GT-Trade-InventoryID-number
                    Trader.StartTrader(eventArgv[1]);
                    break;
                }
            case ("NextDay"):
                {
                    //NextDay
                    NextDay();
                    break;
                }
            case ("End"):
                {
                    //End
                    currentPerformance.PerformanceEnd();
                    NextStep();
                    break;
                }
            case ("Next"):
                {
                    NextStep();
                    break;
                }
            case ("AutoSave"):
                {
                    SaveManager.Instance.AutoSave();
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
                    foreach(var s in _conditionString)
                    {
                        print(s);
                    }
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
            ExecEvents(_EventString[1]);
        }
        else
        {
            ExecEvents(_EventString[2]);
        }
    }
    public bool Bigger(List<string> _EventString)
    {
        ///{float-1}-{Find-Date}
        ///{float-5}-{Inventory-Player-ItemID}
        return Find(_EventString[0]) > Find(_EventString[1]);
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
                    print((float)InventoryManager.Instance.GetInventory(argvs[1]).FindItem(argvs[2]));
                    return (float)InventoryManager.Instance.GetInventory(argvs[1]).FindItem(argvs[2]);
                }
            default:
                {
                    return 0;
                }
        }
    }

    public void NextDay()
    {
        GlobalData.Date += 1;
    }

    public void NextStep()
    {
        GlobalData.GameScriptIndex += 1;
        string _EventString = DataManager.Instance.GetData<string>("Profile", "MainDirectorScript", GlobalData.GameScriptIndex.ToString(),"1");
        ExecEvents(_EventString);
    }

    public void StartDialogPerformance(string bookPath)
    {
        currentPerformance = Dialog;
        Dialog.PerformanceStart(this);
        Dialog.Init(bookPath);
        Dialog.OnClick();
    }

    DataPack IDataUser.SerializedToDataPack()
    {
        if ((object)currentPerformance == Dialog)
        {
            GlobalData.currentPerformance = "Dialog";
        }
        DataPack newpack = new();
        newpack.PackName = "GlobalData";
        newpack.DeserializeData = JsonConvert.SerializeObject(GlobalData);

        return newpack;
    }

    T IDataUser.GetData<T>(params string[] argv)
    {
        switch (argv[0])
        {
            case ("Date"):
            {
                    return (T)(object)Date;
            }
        }
        throw new System.Exception($"没有该成员:{argv[0]}");
    }

    bool IDataUser.SetData<T>(T value, params string[] argv)
    {
        throw new System.NotImplementedException();
    }
}
