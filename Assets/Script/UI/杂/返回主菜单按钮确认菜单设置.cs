using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class 返回主菜单按钮确认菜单设置 : MonoBehaviour
{
    
    public UnityEvent A_Events;
    public void OnClick()
    {
        SetWarningWindow();

    }
    public void SetWarningWindow()
    {
        WarningWindow.Instance.Call();
        WarningWindow.Instance.MainMSG.text = $"返回主菜单？\n将丢失未保存的内容";
        WarningWindow.Instance.A_MSG.text = "确认";
        WarningWindow.Instance.B_MSG.text = "返回";
        WarningWindow.Instance.A_Event.AddListener(A);
    }
    public void A()
    {
        A_Events?.Invoke();
        MainDirector.Instance.StopEveryThing();
    }
}
