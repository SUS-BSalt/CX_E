using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RotationChartBOU : MonoBehaviour
{
    [Header("��Ҫ���")]
    public ScrollRect scrollRect;
    public RectTransform content;
    public RectTransform Viewport;
    public Canvas canvas;

    [Header("�������Լ������ж�")]
    public List<GameObject> childList;
    public List<RectTransform> childTransformList;
    public int FocusObjIndex;//����������ı��
    [Header("������չʾ���")]
    public bool isVerticalSort;//�����򣬣�1��0����ʾˮƽ���򣬣�0��1����ʾ��ֱ���򣬱����ֵ�����𲻿�Ԥ��Ľ��
    [Header("״̬")]
    public bool isTouchUpBorder = false;//�Ƿ������ϱ߽�
    public bool isTouchBottomBorder = false;//�Ƿ������±߽�
    public bool isControllSelf = false;
    [Header("����")]
    private Vector2 previousPos;//������ק�õģ���¼��һ֡���λ��
    [Range(0,1)] public float scrollSensitivity;//����������
    //[Header("�¼�")]
    [Serializable]
    public class FocusObjectChangedEvent : UnityEvent<bool> { }//true�ǰ�ͷ�ŵ�β��false��β�ŵ�ͷ
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
    public void ChildListChange(bool direction)//true�ǰ�ͷ�ŵ�β��false��β�ŵ�ͷ�����иı�������˳��������������ҵ���������䶯�¼�
    {
        //Debug.Log("���ˣ�");
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
    }//�������е��ƶ�����������Ȼ���������valueChange�¼�

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
    }//���������������ã���OnScrollһģһ��

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
