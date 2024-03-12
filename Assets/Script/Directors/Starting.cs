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
        Dialog.plotTrigger += GamePlot;
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
        Dialog.plotTrigger += GamePlot;
        Dialog.OnClick();
        //print(Dialog.data.bookMark + "why?");
        //print(Dialog.data.currentBookChapter+"why?");

        MainMenu.SetActive(false);
        //print("fuck");
        LoadingMenu.SetActive(false);
    }
    public void GamePlot(List<string> argv)
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
        }
    }
    public void TraderEndMethod()
    {
        print("?");
        Dialog.NextStep();
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
