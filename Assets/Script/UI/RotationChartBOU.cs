using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotationChartBOU : MonoBehaviour
{
    [Header("必要组件")]
    public ScrollRect scrollRect;
    public RectTransform content;
    public RectTransform Viewport;
    public Canvas canvas;

    [Header("子物体以及焦点判定")]
    public List<GameObject> childList;
    public List<RectTransform> childTransformList;
    public int FocusObjIndex;//焦点子物体的编号
    [Header("排序与展示相关")]
    public bool isVerticalSort;//排序方向，（1，0）表示水平排序，（0，1）表示垂直排序，别的数值会引起不可预测的结果
    [Header("状态")]
    public bool isTouchUpBorder = false;//是否触摸到上边界
    public bool isTouchBottomBorder = false;//是否触摸到下边界
    public bool isControllSelf = false;
    [Header("控制")]
    private Vector2 previousPos;//处理拖拽用的，记录上一帧鼠标位置
    [Range(0,1)] public float scrollSensitivity;//滚轮灵敏度
    //[Header("事件")]
    [Serializable]
    public class FocusObjectChangedEvent : UnityEvent<bool> { }//true是把头放到尾，false是尾放到头
    public FocusObjectChangedEvent onFocusObjectChanged;
    [Serializable]
    public class ValueChangedEvent : UnityEvent<Vector2> { }
    public ValueChangedEvent OnValueChanged;


    // Start is called before the first frame update
    private void Awake()
    {
        OnValueChanged.AddListener(ChildListChangeCheck);
    }
    public void ChildListChangeCheck(Vector2 normalizedPosition)
    {
        if (normalizedPosition.x < 1 && normalizedPosition.x > 0 &&
            normalizedPosition.y < 1 && normalizedPosition.y > 0)
        {
            return;
        }
        if (normalizedPosition.x > 1 && !isTouchUpBorder)
        {
            scrollRect.normalizedPosition = new Vector2(1 - GetChildSizeProportion(GetChildRectTransform(0), isVerticalSort), scrollRect.normalizedPosition.y);
            ChildListChange(false);
        }
        else if(normalizedPosition.x < 0 && !isTouchBottomBorder)
        {
            scrollRect.normalizedPosition = new Vector2(0 + GetChildSizeProportion(GetChildRectTransform(content.childCount - 1), isVerticalSort), scrollRect.normalizedPosition.y);
            ChildListChange(true);
        }
        if (normalizedPosition.y > 1 && !isTouchUpBorder)
        {
            scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x , 1 - GetChildSizeProportion(GetChildRectTransform(0),  isVerticalSort));
            ChildListChange(false);
        }
        else if (normalizedPosition.y < 0 && !isTouchBottomBorder)
        {
            scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, 0 + GetChildSizeProportion(GetChildRectTransform(content.childCount - 1),  isVerticalSort));
            ChildListChange(true);
        }
        //Debug.Log(scrollRect.normalizedPosition);
    }
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
        onFocusObjectChanged.Invoke(direction);
    }

    public RectTransform GetChildRectTransform(int index)
    {
        return content.GetChild(index).GetComponent<RectTransform>();
    }

    public void JumpToNext(bool isToNext)
    {
        float normalizedMoveDistance;
        if (isToNext)
        {
            normalizedMoveDistance = - GetChildSizeProportion(GetChildRectTransform(FocusObjIndex), isVerticalSort);
        }
        else
        {
            normalizedMoveDistance = GetChildSizeProportion(GetChildRectTransform(GetNeighborObjIndex(FocusObjIndex, -1)), isVerticalSort);
        }
        if (isVerticalSort)
        {
            scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, 0);
            MovingNormalizedDistance(new Vector2(0,normalizedMoveDistance));
            scrollRect.normalizedPosition = new Vector2(scrollRect.normalizedPosition.x, 0);
        }
        else
        {
            scrollRect.normalizedPosition = new Vector2(0, scrollRect.normalizedPosition.y);
            MovingNormalizedDistance(new Vector2(normalizedMoveDistance, 0));
            scrollRect.normalizedPosition = new Vector2(0, scrollRect.normalizedPosition.y);
        }
        Debug.Log("jump");
    }
    public void MovingNormalizedDistance(Vector2 distance)
    {
        scrollRect.normalizedPosition = scrollRect.normalizedPosition + distance;
        OnValueChanged.Invoke(scrollRect.normalizedPosition);
    }//负责所有的移动必须走它，然后它会调用valueChange事件

    public Vector2 CaculateNormalizedDistanceOnScreen(Vector2 startPos, Vector2 endPos)
    {
        Vector2 n_starPos = RectTransformUtility.PixelAdjustPoint(startPos, content, canvas);
        Vector2 n_endPos = RectTransformUtility.PixelAdjustPoint(endPos, content, canvas);
        return ((n_starPos - n_endPos) / (content.rect.size - Viewport.rect.size));
    }
    public void test(Vector2 other)
    {
        Debug.Log(other);
        //scrollRect.normalizedPosition = new Vector2(0, 0.2f);
        //Debug.Log(GetChildSizeProportion(content.GetChild(0).GetComponent<RectTransform>(), content, isVerticalSort));
        //Debug.Log(content.GetChild(0).GetComponent<RectTransform>().rect.height);
        //Debug.Log(content.rect.height);
    }

    public void MoveToNormalizePos(Vector2 normalizedPosition)
    {
        scrollRect.velocity = new Vector2(0, 0);
        MovingNormalizedDistance(normalizedPosition - scrollRect.normalizedPosition);
    }
    public int GetNeighborObjIndex(int currentObjIndex, int relativePos)
    {
        return ((currentObjIndex + relativePos) % content.childCount + content.childCount) % content.childCount;
    }

    public void OnScroll(PointerEventData eventData)
    {
        float movingDistance;
        if (!isControllSelf)
        {
            return;
        }
        if (eventData.scrollDelta.y > 0)
        {
            movingDistance = scrollSensitivity;
            
        }
        else if (eventData.scrollDelta.y < 0)
        {
            movingDistance = -scrollSensitivity;
        }
        else
        {
            movingDistance = 0;
        }
        if (isVerticalSort)
        {
            MovingNormalizedDistance(new Vector2(0, movingDistance));
        }
        else
        {
            MovingNormalizedDistance(new Vector2(movingDistance, 0));
        }
    }
    public void OnScrollCall(float direction)
    {
        float movingDistance;
        if (direction > 0)
        {
            movingDistance = scrollSensitivity;

        }
        else if (direction < 0)
        {
            movingDistance = -scrollSensitivity;
        }
        else
        {
            movingDistance = 0;
        }
        if (isVerticalSort)
        {
            MovingNormalizedDistance(new Vector2(0, movingDistance));
        }
        else
        {
            MovingNormalizedDistance(new Vector2(movingDistance, 0));
        }
    }//方便其他函数调用，与OnScroll一模一样

    public void OnBeginDrag(PointerEventData eventData)
    {
        previousPos = eventData.position;
    }

    public void OnBeginDragCall(Vector2 mousePos)
    {
        previousPos = mousePos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isControllSelf)
        {
            return;
        }
        Vector2 movingDistance = CaculateNormalizedDistanceOnScreen(previousPos,eventData.position);
        previousPos = eventData.position;
        MovingNormalizedDistance(movingDistance);
    }

    public void OnDragCall(Vector2 mousePos)
    {
        Vector2 movingDistance = CaculateNormalizedDistanceOnScreen(previousPos, mousePos);
        previousPos = mousePos;
        MovingNormalizedDistance(movingDistance);
    }
}
