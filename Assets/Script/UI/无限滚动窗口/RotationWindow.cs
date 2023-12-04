using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RotationWindowController))]
public class RotationWindow : ScrollRect
{
    [Header("必要组件")]
    public Canvas canvas;//组件所在的画布
    private RotationWindowController controller;

    [Header("子物体以及焦点判定")]
    public int FocusObjIndex;//焦点子物体的编号

    [Header("状态")]
    public bool isTouchUpBorder;//是否触摸到上边界
    public bool isTouchBottomBorder;//是否触摸到下边界
    public bool isControllSelf;//是否由这个组件控制滚动窗口

    [Header("控制窗口用参数")]
    private Vector2 previousPos;//处理拖拽用的，记录上一帧鼠标位置

    public class FocusObjectChangedEvent : UnityEvent<bool> { }//true是把头放到尾，false是尾放到头
    public FocusObjectChangedEvent onFocusObjectChanged;
    public new void Awake()
    {
        onValueChanged.AddListener(ChildResortCheck);
        controller = GetComponent<RotationWindowController>();
        SetArgvFromController();
        //scrollRect.
        //scrollRect.onValueChanged.AddListener(TestValue);
    }
    public void SetArgvFromController()
    {
        canvas = controller.canvas;
        FocusObjIndex = controller.FocusObjIndex;
        isControllSelf = controller.isControllSelf;
    }

    public void TestValue(Vector2 normalizedPosition)
    {
        print(normalizedPosition);
    }
    #region 让子物体重新排序的方法们
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
            ChildListChange(false);
            this.normalizedPosition = new Vector2(1 - GetChildSizeProportion(GetChildRectTransform(0), vertical), this.normalizedPosition.y);
        }
        else if (normalizedPosition.x < 0 && !isTouchBottomBorder)
        {
            ChildListChange(true);
            this.normalizedPosition = new Vector2(0 + GetChildSizeProportion(GetChildRectTransform(content.childCount - 1), vertical), this.normalizedPosition.y);
        }
        if (normalizedPosition.y > 1 && !isTouchUpBorder)
        {
            ChildListChange(false);
            this.normalizedPosition = new Vector2(this.normalizedPosition.x, 1 - GetChildSizeProportion(GetChildRectTransform(0), vertical));
        }
        else if (normalizedPosition.y < 0 && !isTouchBottomBorder)
        {
            ChildListChange(true);
            this.normalizedPosition = new Vector2(this.normalizedPosition.x, 0 + GetChildSizeProportion(GetChildRectTransform(content.childCount - 1), vertical));
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
            return (childRect.rect.height / (content.rect.height - viewport.rect.height));
        }
        else
        {
            return childRect.rect.width / (content.rect.height - viewport.rect.height);
        }
    }
    /// <summary>
    /// 通过index取得子物体的RectTransform组件
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public RectTransform GetChildRectTransform(int index)
    {
        return content.GetChild(index).GetComponent<RectTransform>();
    }
    /// <summary>
    /// 修改子物体的顺序，每次调用，将一个物体从头部放到尾部，或者从尾部放到头部
    /// </summary>
    /// <param name="direction"></param>
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
    #endregion
    #region 控制这个RotationWindow的，从EventSystem那里搞来的接口以及配合其使用的方法
    //void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    //{
    //    previousPos = eventData.position;
    //}
    //void IDragHandler.OnDrag(PointerEventData eventData)
    //{
    //    if (!isControllSelf)
    //    {
    //        return;
    //    }
    //    Vector2 movingDistance = CaculateNormalizedDistanceOnScreen(previousPos, eventData.position);
    //    previousPos = eventData.position;
    //    MovingNormalizedDistance(movingDistance);
    //}

    //public new void OnBeginDrag(PointerEventData eventData)
    //{
    //    previousPos = eventData.position;
    //}
    //public new void OnDrag(PointerEventData eventData)
    //{
    //    if (!isControllSelf)
    //    {
    //        return;
    //    }
    //    Vector2 movingDistance = CaculateNormalizedDistanceOnScreen(previousPos, eventData.position);
    //    previousPos = eventData.position;
    //    MovingNormalizedDistance(movingDistance);
    //}
    public override void OnBeginDrag(PointerEventData eventData)
    {
        //print("get");
        previousPos = eventData.position;
    }
    public override void OnDrag(PointerEventData eventData)
    {
        //print("what?");
        Vector2 movingDistance = CaculateNormalizedDistanceOnScreen(previousPos, eventData.position);
        previousPos = eventData.position;
        MovingNormalizedDistance(movingDistance);
    }
    public new void OnEndDrag(PointerEventData eventData)
    {

    }
    public void JumpToNext(bool isToNext)
    {
        float normalizedMoveDistance;
        if (isToNext)
        {
            normalizedMoveDistance = -GetChildSizeProportion(GetChildRectTransform(FocusObjIndex), vertical);
        }
        else
        {
            normalizedMoveDistance = GetChildSizeProportion(GetChildRectTransform(GetNeighborObjIndex(FocusObjIndex, -1)), vertical);
        }
        if (vertical)
        {
            normalizedPosition = new Vector2(this.normalizedPosition.x, 0);
            MovingNormalizedDistance(new Vector2(0, normalizedMoveDistance));
            normalizedPosition = new Vector2(this.normalizedPosition.x, 0);
        }
        else
        {
            this.normalizedPosition = new Vector2(0, this.normalizedPosition.y);
            MovingNormalizedDistance(new Vector2(normalizedMoveDistance, 0));
            this.normalizedPosition = new Vector2(0, this.normalizedPosition.y);
        }
        //Debug.Log("jump");
    }
    public void MovingNormalizedDistance(Vector2 distance)
    {
        this.normalizedPosition = this.normalizedPosition + distance;
        onValueChanged?.Invoke(this.normalizedPosition);
    }//负责所有的移动必须走它，然后它会调用valueChange事件

    public Vector2 CaculateNormalizedDistanceOnScreen(Vector2 startPos, Vector2 endPos)
    {
        Vector2 n_starPos = RectTransformUtility.PixelAdjustPoint(startPos, content, canvas);
        Vector2 n_endPos = RectTransformUtility.PixelAdjustPoint(endPos, content, canvas);
        return ((n_starPos - n_endPos) / (content.rect.size - viewport.rect.size));
    }
    public void MoveToNormalizePos(Vector2 normalizedPosition)
    {
        this.velocity = new Vector2(0, 0);
        MovingNormalizedDistance(normalizedPosition - this.normalizedPosition);
    }
    public int GetNeighborObjIndex(int currentObjIndex, int relativePos)
    {
        return ((currentObjIndex + relativePos) % content.childCount + content.childCount) % content.childCount;
    }
    #endregion
}
