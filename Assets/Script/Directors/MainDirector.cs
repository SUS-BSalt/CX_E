using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Starting;
public interface IDirector
{
    public IPerformance currentPerformance { get; set; }
    public void NextStep();
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

    public DialogManager Dialog;
    public TradeManager Trader;
    public InventoryManager Inventory;
    public GameObject LoadingMenu;
    public GameObject MainMenu;

    protected override void Awake()
    {
        base.Awake();
        GameScript = GameScriptSO.GetTable();
    }
    private void Start()
    {
        SaveManager.Instance.SaveEvent.AddListener(OnSave);
        SaveManager.Instance.LoadEvent.AddListener(OnLoad);
        Dialog.plotTrigger += GamePlot;
        Trader.TradeEnd += TradeEndMethod;
    }
    public void OnLoad()
    {
        _Date = SaveManager.Instance.LoadData<int>("Date");
    }
    public void NewGame()
    {
        LoadingMenu.SetActive(true);
        this.Inventory.New();
        Dialog.gameObject.SetActive(true);
        Dialog.data = new(1, "", 1);
        Dialog.SetBook("test.xlsx");
        //print(Dialog.data.bookMark + "why?");
        //print(Dialog.data.currentBookChapter+"why?");
        Date = 1;
        StartNewDay(Date);
        Dialog.OnClick();

        MainMenu.SetActive(false);
        //print("fuck");
        LoadingMenu.SetActive(false);
    }
    public void TradeEndMethod()
    {
        print("s");
        Dialog.NextStep();
    }
    public void GamePlot(List<string> argv)
    {
        switch (argv[1])
        {
            case ("Add"):
                {
                    //GT-Add-InventoryID-ItemID-{ProfileJson}-number
                    InventoryManager.Instance.AddItemToInventory(argv[2], int.Parse(argv[3]), argv[4], int.Parse(argv[5]));
                    //print("add");
                    break;
                }
            case ("Trust"):
                {
                    //GT-Trust-InventoryID-number
                    InventoryManager.Instance.Inventorys[argv[2]].Trust = int.Parse(argv[3]);
                    //print("add");
                    break;
                }
            case ("Trade"):
                {
                    Trader.StartTrader(argv[2]);
                    break;
                }
            case ("TradeBranch"):
                {
                    //GT-TradeBranch-{"BranchName":"A","BookPath":"","Level":"1"}-{"BranchName":"B","BookPath":"","Level":"2"}-{"BranchName":"C","BookPath":"","Level":"3"}
                    //RegisteTradeBranch(argv[2..]);
                    break;
                }
            ///If-{condition}-{true branch}-{fales branch}
            case ("If"):
                {
                    IfEvent(argv.Skip(1).ToList());
                    break;
                }
        }
    }
    public void OnSave()
    {
        SaveManager.Instance.SaveData<int>("Date",_Date);
    }
    public void StartNewDay(int Date)
    {
        _Date = Date;
        ExecEvents(GameScript.GetData<string>(_Date + 1, 2));
    }
    public void ExecEvents(string __EventString)
    {
        List<List<string>> Events = EventString.UnpackComplex(__EventString);
        foreach(List<string> Eventpach in Events)
        {
            ExecEvent(Eventpach);
        }
    }
    public void ExecEvent(List<string> Eventpach)
    {
        switch (Eventpach[0])
        {
            ///If-{condition}-{true branch}-{fales branch}
            case ("If"):
                {
                    IfEvent(Eventpach.Skip(1).ToList());
                    break;
                }
            case "Jump":
                {
                    Dialog.bookMark = int.Parse(eventArgv[1]);
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
                    return (float)InventoryManager.Instance.GetInventory(argvs[1]).FindItem(argvs[2]);
                }
            default:
                {
                    return 0;
                }
        }
    }

    public void NextStep()
    {
        Date++;
        StartNewDay(Date);
    }
}
