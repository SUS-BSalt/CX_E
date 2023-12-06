using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogLogWindowManager : MonoBehaviour
{
    public DialogManager Manager;
    public GameObject LogMenu;
    public RotationWindow rotationWindow;
    public int bookMarkLogs;//ר����logs�����bookMakr

    private void Awake()
    {
        rotationWindow.onFocusObjectChanged.AddListener(UpdateLogMenu);
    }

    public void CleanLogMenu()
    {
        for (int i = 0; i < rotationWindow.content.childCount; i++)
        {
            rotationWindow.content.GetChild(i).GetChild(0).GetComponent<Text>().text = "";
        }
        rotationWindow.MoveToNormalizePos(new Vector2(0, 0));
    }
    public void UpdateLogMenu(bool updateDirection)//rotaionWindow�ĸ��·���
    {
        //print("logMenu"+ updateDirection);
        //print("UP" + rotationWindow.isTouchUpBorder);
        //print("BP" + rotationWindow.isTouchBottomBorder);
        if (!LogMenu.activeInHierarchy)//���LogMenu��Ծ���Ž��и���
        {
            return;
        }

        bookMarkLogs = updateDirection ? bookMarkLogs + 1: bookMarkLogs - 1;

        if(bookMarkLogs >= Manager.bookMark)
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
        for (int i = 0; i < rotationWindow.content.childCount; i++)
        {
            rotationWindow.content.GetChild(i).GetChild(0).GetComponent<Text>().text = Manager.GetLogString(bookMarkLogs - rotationWindow.content.childCount + i + 1);
        }
    }//ÿ��rotationWindow��������䶯ʱ���ã����ݱ䶯�������ͷ����β����text��
    public void RefreshLogMenu(int bookMark)
    {
        if (!LogMenu.activeInHierarchy)//���LogMenu��Ծ���Ž��и���
        {
            return;
        }

        rotationWindow.isTouchBottomBorder = true;
        rotationWindow.isTouchUpBorder = false;


        for (int i = 0; i < rotationWindow.content.childCount; i++)
        {
            rotationWindow.content.GetChild(i).GetChild(0).GetComponent<Text>().text = Manager.GetLogString(bookMark - rotationWindow.content.childCount + i + 1);
        }

        rotationWindow.MoveToNormalizePos(new Vector2(0, 0));
        //print(rotationWindow.normalizedPosition);
        bookMarkLogs = bookMark;
    }//ˢ��logs���棬ÿ�ν���logs�˵�ʱ����
}
