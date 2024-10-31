using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class 存档按钮确认菜单设置 : MonoBehaviour
{
    public UnityEvent A_Events;
    public SaveSysMSG saveSysMSG;
    public void OnClick()
    {
        SetWarningWindow();

    }
    public void SetWarningWindow()
    {
        WarningWindow.Instance.Call();
        WarningWindow.Instance.MainMSG.text = $"覆盖至:{saveSysMSG.selectedSaveField.header.DMSG}";
        WarningWindow.Instance.A_MSG.text = "确认";
        WarningWindow.Instance.B_MSG.text = "返回";
        WarningWindow.Instance.A_Event.AddListener(saveSysMSG.Save);
        WarningWindow.Instance.A_Event.AddListener(A);
    }
    public void A()
    {
        A_Events?.Invoke();
    }
}
