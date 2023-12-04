using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotationWindow))]
public class RotationWindowController : MonoBehaviour
{
    [Header("必要组件")]
    public Canvas canvas;//组件所在的画布
    public RotationWindow rotationWindow;
    [Header("子物体以及焦点判定")]
    public int FocusObjIndex;//焦点子物体的编号

    [Header("状态")]
    public bool isTouchUpBorder;//是否触摸到上边界
    public bool isTouchBottomBorder;//是否触摸到下边界
    public bool isControllSelf;//是否由这个组件控制滚动窗口

    private void Awake()
    {
        rotationWindow = GetComponent<RotationWindow>();
    }
}
