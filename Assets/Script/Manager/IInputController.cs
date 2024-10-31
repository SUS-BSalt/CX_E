using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputController
{
    /// <summary>
    /// 为避免循环调用，禁止在该方法内转移控制权
    /// </summary>
    public void OnGetControll();
    /// <summary>
    /// 为避免循环调用，禁止在该方法内转移控制权
    /// </summary>
    public void OnLoseControll();
}
