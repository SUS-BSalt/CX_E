using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public IInputController currentController;
    public IInputController preController;
    public void GrabControl(IInputController inputController)
    {
        preController = currentController;
        currentController.OnLoseControll();
        currentController = inputController;
        currentController.OnGetControll();
    }
    /// <summary>
    /// 非常危险，可能导致玩家失去控制，应当仅在如“确认菜单”这种，控制权只会从A处获取并还给A处的场景内，使用这个方法
    /// </summary>
    public void RollBack()
    {
        GrabControl(preController);
    }
    /// <summary>
    /// 无视当前控制者，直接设置新的控制者并执行其方法
    /// 不安全，不会执行当前控制者的OnLoseControll方法
    /// </summary>
    /// <param name="inputController"></param>
    public void SetController(IInputController inputController)
    {
        preController = currentController;
        currentController = inputController;
        currentController.OnGetControll();
    }
}
