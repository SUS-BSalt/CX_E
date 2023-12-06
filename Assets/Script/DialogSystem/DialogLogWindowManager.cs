using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLogWindowManager : MonoBehaviour
{
    public DialogManager Manager;
    public GameObject LogMenu;
    public RotationWindow rotationWindow;
    public int UpdateModifyIndex;

    public void CleanLogMenu()
    {
        for (int i = 0; i < rotationWindow.content.childCount; i++)
        {
            rotationWindow.content.GetChild(i).GetChild(0).GetComponent<Text>().text = "";
        }
        rotationWindow.MoveToNormalizePos(new Vector2(0, 0));
    }
    public void UpdateLogMenu(int bookMark )
    {
        if (!LogMenu.activeInHierarchy)//如果LogMenu活跃，才进行更新
        {
            return;
        }
        
    }//每当rotationWindow焦点物体变动时调用，根据变动方向更新头部或尾部的text框
    public void RefreshLogMenu(int bookMark)
    {
        if (!LogMenu.activeInHierarchy)//如果LogMenu活跃，才进行更新
        {
            return;
        }
        rotationWindow.MoveToNormalizePos(new Vector2(0, 0));
        for (int i = 0; i < rotationWindow.content.childCount; i++)
        {
            rotationWindow.content.GetChild(i).GetChild(0).GetComponent<Text>().text = Manager.GetLogString(bookMark - rotationWindow.content.childCount + i + 1);
        }
    }//刷新logs界面，每次进入logs菜单时调用
}
