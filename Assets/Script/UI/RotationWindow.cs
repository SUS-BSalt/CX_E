using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RotationWindow : MonoBehaviour
{
    [Header("必要组件")]

    /// <summary>
    /// 这个组件不要同时勾选垂直与水平移动
    /// </summary>
    public ScrollRect scrollRect;

    public RectTransform content;
    public RectTransform Viewport;
    [Header("子物体以及焦点判定")]
    public int FocusObjIndex;//焦点子物体的编号

    [Header("状态")]
    public bool isTouchUpBorder = false;//是否触摸到上边界
    public bool isTouchBottomBorder = false;//是否触摸到下边界
    public bool isControllSelf = false;

    public class FocusObjectChangedEvent : UnityEvent<bool> { }//true是把头放到尾，false是尾放到头
    public FocusObjectChangedEvent onFocusObjectChanged;
    private void Awake()
    {
        scrollRect.onValueChanged.AddListener(ChildResortCheck);
    }
    /// <summary>
    /// 检查是否需要调整子物体的顺序
    /// </summary>
    /// <param name="normalizedPosition"></param>
    public void ChildResortCheck(Vector2 normalizedPosition)
    {
        if (normalizedPosition.x < 1 && normalizedPosition.x > 0 &&
            normalizedPosition.y < 1 && normalizedPosition.y > 0)
        {
            return;
        }
        if (normalizedPosition.x > 1 && !isTouchUpBorder)
        {
            scrollRect.normalizedPosition = new Vector2(1 - GetChildSizeProportion(GetChildRectTransform(0), scrollRect.vertical), scrollRect.normalizedPosition.y);
            ChildListChange(false);
        }
        else if (normalizedPosition.x < 0 && !isTouchBottomBorder)
        {
            scrollRect.normalizedPosition = new Vector2(0 + GetChildSizeProportion(GetChildRectTransform(content.childCount - 1), scrollRect.vertical), scrollRect.normalizedPosition.y);
            ChildListChange(true);
        }
        if (normalizedPosition.y > 1 && !isTouchUpBorder)
        {
            scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, 1 - GetChildSizeProportion(GetChildRectTransform(0), scrollRect.vertical));
            ChildListChange(false);
        }
        else if (normalizedPosition.y < 0 && !isTouchBottomBorder)
        {
            scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, 0 + GetChildSizeProportion(GetChildRectTransform(content.childCount - 1), scrollRect.vertical));
            ChildListChange(true);
        }
        //Debug.Log(scrollRect.normalizedPosition);
    }
    /// <summary>
    /// 获取子物体在content窗口的占比
    /// </summary>
    /// <param name="childRect"></param>
    /// <param name="isVerticalSort">这个参数决定其返回的是水平方向的比例，还是垂直方向的比例</param>
    /// <returns></returns>
    public float GetChildSizeProportion(RectTransform childRect, bool isVerticalSort)
    {
        if (isVerticalSort)
        {
            //(contentRect.rect.height - Viewport.rect.height) * Return = childRect.rect.height
            return (childRect.rect.height / (content.rect.height - Viewport.rect.height));
        }
        else
        {
            return childRect.rect.width / (content.rect.height - Viewport.rect.height);
        }
    }
    public RectTransform GetChildRectTransform(int index)
    {
        return content.GetChild(index).GetComponent<RectTransform>();
    }

    public void ChildListChange(bool direction)//true是把头放到尾，false是尾放到头，所有改变子物体顺序必须走它，并且调用子物体变动事件
    {
        //Debug.Log("做了！");
        if (direction)
        {
            content.GetChild(0).SetAsLastSibling();
        }
        else
        {
            content.GetChild(content.childCount - 1).SetAsFirstSibling();
        }
        onFocusObjectChanged?.Invoke(direction);
    }
}
