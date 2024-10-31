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
    /// �ǳ�Σ�գ����ܵ������ʧȥ���ƣ�Ӧ�������硰ȷ�ϲ˵������֣�����Ȩֻ���A����ȡ������A���ĳ����ڣ�ʹ���������
    /// </summary>
    public void RollBack()
    {
        GrabControl(preController);
    }
    /// <summary>
    /// ���ӵ�ǰ�����ߣ�ֱ�������µĿ����߲�ִ���䷽��
    /// ����ȫ������ִ�е�ǰ�����ߵ�OnLoseControll����
    /// </summary>
    /// <param name="inputController"></param>
    public void SetController(IInputController inputController)
    {
        preController = currentController;
        currentController = inputController;
        currentController.OnGetControll();
    }
}
