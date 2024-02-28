using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

public class Starting : MonoBehaviour
{
    public DialogManager Dialog;
    public TradeManager Trader;
    public InventoryManager Inventory;
    public GameObject LoadingMenu;
    public GameObject MainMenu;
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
    }
    public void GamePlot(string[] argv)
    {
        switch (argv[1])
        {
            case ("Add"):
                {
                    InventoryManager.Instance.AddItemToInventory(argv[2], int.Parse(argv[3]), argv[4], int.Parse(argv[5]));
                    break;
                }
            case ("Trade"):
                {
                    Trader.StartTrader(argv[2]);
                    break;
                }
            case ("Branch"):
                {
                    //Trigger-Branch-{"BranchName":"A","BookPath":""}-
                    RegisterBranch(argv.Skip(3).ToArray());
                    break;
                }
            case ("TradeBranch"):
                {
                    //Trigger-TradeBranch-{"BranchName":"A","BookPath":"","Level":"1"}-{"BranchName":"B","BookPath":"","Level":"2"}-{"BranchName":"C","BookPath":"","Level":"3"}
                    break;
                }
        }
    }
    public void RegisterBranch(string[] argv)
    {

    }
    public void RegisteTradeBranch(string[] argv)
    {

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
