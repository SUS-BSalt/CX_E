using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLogWindowManager : MonoBehaviour
{
    public Button LogsEnterButton;
    public DialogManager Manager;
    public GameObject LogMenu;
    public RotationWindow rotationWindow;
    public int bookMarkLogs;//专属于logs界面的bookMakr

    public List<string> Logs = new(500);

    private void Awake()
    {
        rotationWindow.onFocusObjectChanged.AddListener(UpdateLogMenu);
        LogsEnterButton.onClick.AddListener(OnOpenLogMenu);
    }

    public void OnOpenLogMenu()
    {
        RefreshLogMenu();
    }

    public void CleanLogMenu()
    {
        for (int i = 0; i < rotationWindow.content.childCount; i++)
        {
            rotationWindow.content.GetChild(i).GetChild(0).GetComponent<Text>().text = "";
        }
        rotationWindow.MoveToNormalizePos(new Vector2(0, 0));
        Logs.Clear();
    }
    public void UpdateLogMenu(bool updateDirection)//rotaionWindow的更新方向
    {
        //print("logMenu"+ updateDirection);
        //print("UP" + rotationWindow.isTouchUpBorder);
        //print("BP" + rotationWindow.isTouchBottomBorder);
        if (!LogMenu.activeInHierarchy)//如果LogMenu活跃，才进行更新
        {
            return;
        }

        bookMarkLogs = updateDirection ? bookMarkLogs + 1: bookMarkLogs - 1;

        if(bookMarkLogs >= Logs.Count-1)
        {
            rotationWindow.isTouchBottomBorder = true;
            rotationWindow.isTouchUpBorder = false;
        }
        else if(bookMarkLogs <= 1)
        {
            rotationWindow.isTouchBottomBorder = false;
            rotationWindow.isTouchUpBorder = true;
        }
        else
        {
            rotationWindow.isTouchBottomBorder = false;
            rotationWindow.isTouchUpBorder = false;
        }
        //print("UB" + rotationWindow.isTouchUpBorder);
        //print("BB" + rotationWindow.isTouchBottomBorder);

        if (updateDirection)
        {
            rotationWindow.content.GetChild(rotationWindow.content.childCount - 1).GetChild(0).GetComponent<Text>().text =
                GetLogString(bookMarkLogs);
                //Manager.GetLogString(bookMarkLogs - rotationWindow.content.childCount + rotationWindow.content.childCount);
        }
        else
        {
            rotationWindow.content.GetChild(0).GetChild(0).GetComponent<Text>().text =
                GetLogString(bookMarkLogs - rotationWindow.content.childCount + 1);
            //Manager.GetLogString(bookMarkLogs - rotationWindow.content.childCount + 1);
        }
    }//每当rotationWindow焦点物体变动时调用，根据变动方向更新头部或尾部的text框
    public string GetLogString(int bookMark)
    {
        try
        {
            return Logs[bookMark];
        }
        catch
        {
            return "";
        }
    }
    public void RefreshLogMenu()
    {
        if (!LogMenu.activeInHierarchy)//如果LogMenu活跃，才进行更新
        {
            return;
        }

        rotationWindow.isTouchBottomBorder = true;
        rotationWindow.isTouchUpBorder = false;

        bookMarkLogs = Logs.Count - 1;

        for (int i = 0; i < rotationWindow.content.childCount; i++)
        {
            rotationWindow.content.GetChild(i).GetChild(0).GetComponent<Text>().text = GetLogString(bookMarkLogs - rotationWindow.content.childCount + i + 1);
        }
        rotationWindow.MoveToNormalizePos(new Vector2(0, 0));
        //print(rotationWindow.normalizedPosition);

    }//刷新logs界面，每次进入logs菜单时调用
}
