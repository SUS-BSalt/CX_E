using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
/// <summary>
/// π∑ ∫ÕÊ“‚+1
/// </summary>
public class Starting : MonoBehaviour
{
    public DialogManager Dialog;
    public TradeManager Trader;
    public InventoryManager Inventory;
    public GameObject LoadingMenu;
    public GameObject MainMenu;
    public Dictionary<TradeManager.TradeEndType, Branch> branchs;
    public struct Branch
    {
        public string BranchName;
        public string BookPath;
        public int Level;
    }
    public void StartEveryThing()
    {
        LoadingMenu.SetActive(true);

        Dialog.gameObject.SetActive(true);
        Dialog.SetBook("Conversation/01.xlsx");
        Dialog.plotTrigger.AddListener(GamePlot);
        Dialog.OnClick();
        
        MainMenu.SetActive(false);
        print("fuck");
        LoadingMenu.SetActive(false);
        Trader.TradeEnd.AddListener(TraderEndMethod);
    }
    public void StartTestingScence()
    {
        LoadingMenu.SetActive(true);
        Dialog.gameObject.SetActive(true);
        Dialog.data = new(1,"", 1);
        Dialog.SetBook("test.xlsx");
        Dialog.plotTrigger.AddListener(GamePlot);
        Dialog.OnClick();
        //print(Dialog.data.bookMark + "why?");
        //print(Dialog.data.currentBookChapter+"why?");

        MainMenu.SetActive(false);
        //print("fuck");
        LoadingMenu.SetActive(false);
        Trader.TradeEnd.AddListener(TraderEndMethod);
    }
    public void GamePlot(string[] argv)
    {
        switch (argv[1])
        {
            case ("Add"):
                {
                    //Trigger-Add-InventoryID-ItemID-{ProfileJson}-number
                    InventoryManager.Instance.AddItemToInventory(argv[2], int.Parse(argv[3]), argv[4], int.Parse(argv[5]));
                    break;
                }
            case ("Trade"):
                {
                    Trader.StartTrader(argv[2]);
                    break;
                }
            case ("TradeBranch"):
                {
                    //Trigger-TradeBranch-{"BranchName":"A","BookPath":"","Level":"1"}-{"BranchName":"B","BookPath":"","Level":"2"}-{"BranchName":"C","BookPath":"","Level":"3"}
                    RegisteTradeBranch(argv[2..]);
                    break;
                }
        }
    }
    public void RegisteTradeBranch(string[] argv)
    {
        branchs = new()
        {
            { TradeManager.TradeEndType.Failed, JsonConvert.DeserializeObject<Branch>(argv[0]) },
            { TradeManager.TradeEndType.FavorableToPlayer, JsonConvert.DeserializeObject<Branch>(argv[1]) },
            { TradeManager.TradeEndType.Equal, JsonConvert.DeserializeObject<Branch>(argv[2]) },
            { TradeManager.TradeEndType.FavorableToNPC, JsonConvert.DeserializeObject<Branch>(argv[3]) }
        };

    }
    public void TraderEndMethod(TradeManager.TradeEndType endType)
    {
        print("?");
        var branch = branchs[endType];
        print(branch.BranchName);
        Dialog.SetBook(branch.BookPath);
        Dialog.OnClick();
        //Dialog.OnClick();
        //Dialog.OnClick();
    }
    public void OnLoad()
    {
        print("Director Load");
        Dialog.gameObject.SetActive(true);
        Dialog.OnLoad();
        
        Inventory.OnLoad();
    }
    public void OnSave()
    {
        print("Director Save");
        Dialog.OnSave();
        Inventory.OnSave();
    }
    public void StartTradingScence()
    {

    }
}
