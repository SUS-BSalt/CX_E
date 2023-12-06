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
        if (!LogMenu.activeInHierarchy)//���LogMenu��Ծ���Ž��и���
        {
            return;
        }
        
    }//ÿ��rotationWindow��������䶯ʱ���ã����ݱ䶯�������ͷ����β����text��
    public void RefreshLogMenu(int bookMark)
    {
        if (!LogMenu.activeInHierarchy)//���LogMenu��Ծ���Ž��и���
        {
            return;
        }
        rotationWindow.MoveToNormalizePos(new Vector2(0, 0));
        for (int i = 0; i < rotationWindow.content.childCount; i++)
        {
            rotationWindow.content.GetChild(i).GetChild(0).GetComponent<Text>().text = Manager.GetLogString(bookMark - rotationWindow.content.childCount + i + 1);
        }
    }//ˢ��logs���棬ÿ�ν���logs�˵�ʱ����
}
