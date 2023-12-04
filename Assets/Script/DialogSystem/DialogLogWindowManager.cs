using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogLogWindowManager : MonoBehaviour
{
    public MenuBase LogMenu;
    public void UpdateLogMenu()
    {
        if (!LogMenu.gameObject.activeInHierarchy)//如果LogMenu活跃，才进行更新
        {
            return;
        }

    }
}
