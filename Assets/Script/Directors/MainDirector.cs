using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static Starting;
using Newtonsoft.Json.Bson;
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
        Dialog.gameObject.SetActive(true);
        Dialog.OnLoad();
        Inventory.OnLoad();
    }
    public void OnSave()
    {
        SaveManager.Instance.SaveData<int>("Date", _Date);
        Dialog.OnSave();
        Inventory.OnSave();
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
        //print("s");
        Dialog.NextStep();
    }
    public void GamePlot(List<string> argv)
    {
        ExecEvent(argv.Skip(1).ToList());
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
            case "Jump":
                {
                    ///Jump-index
                    Dialog.bookMark = int.Parse(eventArgv[1]);
                    break;
                }
            case "Chapter":
                {
                    ///Chapter
                    Dialog.bookChapter = int.Parse(eventArgv[1]);
                    Dialog.bookReader.ChangeBookChapter(Dialog.bookChapter);
                    Dialog.bookMark = 2;
                    break;
                }
            case "Book":
                {
                    Dialog.SetBook(eventArgv[1]);
                    Dialog.bookMark = 2;
                    break;
                }
            case ("Add"):
                {
                    //Add-InventoryID-ItemID-{ProfileJson}-number
                    InventoryManager.Instance.AddItemToInventory(eventArgv[1], int.Parse(eventArgv[2]), eventArgv[3], int.Parse(eventArgv[4]));
                    //print("add");
                    break;
                }
            case ("Trust"):
                {
                    //Trust-InventoryID-number
                    InventoryManager.Instance.Inventorys[eventArgv[1]].Trust = int.Parse(eventArgv[2]);
                    //print("add");
                    break;
                }
            case ("Trade"):
                {
                    //GT-Trade-InventoryID-number
                    Trader.StartTrader(eventArgv[1]);
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

    public void NextStep()
    {
        Date++;
        StartNewDay(Date);
    }
}
