using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLogWindowManager : MonoBehaviour
{
    public MenuBase LogMenu;
    public void UpdateLogMenu()
    {
        if (!LogMenu.gameObject.activeInHierarchy)//���LogMenu��Ծ���Ž��и���
        {
            return;
        }

    }
}
