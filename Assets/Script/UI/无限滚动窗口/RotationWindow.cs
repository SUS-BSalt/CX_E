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
    [Header("��Ҫ���")]
    public Canvas canvas;//������ڵĻ���
    private RotationWindowController controller;

    [Header("�������Լ������ж�")]
    public int FocusObjIndex;//����������ı��

    [Header("״̬")]
    public bool isTouchUpBorder;//�Ƿ������ϱ߽�
    public bool isTouchBottomBorder;//�Ƿ������±߽�
    public bool isControllSelf;//�Ƿ������������ƹ�������

    [Header("���ƴ����ò���")]
    private Vector2 previousPos;//������ק�õģ���¼��һ֡���λ��

    public class FocusObjectChangedEvent : UnityEvent<bool> { }//true�ǰ�ͷ�ŵ�β��false��β�ŵ�ͷ
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
    #region ����������������ķ�����
    /// <summary>
    /// ����Ƿ���Ҫ�����������˳��
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
    /// ��ȡ��������content���ڵ�ռ��
    /// </summary>
    /// <param name="childRect"></param>
    /// <param name="isVerticalSort">������������䷵�ص���ˮƽ����ı��������Ǵ�ֱ����ı���</param>
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
    /// ͨ��indexȡ���������RectTransform���
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public RectTransform GetChildRectTransform(int index)
    {
        return content.GetChild(index).GetComponent<RectTransform>();
    }
    /// <summary>
    /// �޸��������˳��ÿ�ε��ã���һ�������ͷ���ŵ�β�������ߴ�β���ŵ�ͷ��
    /// </summary>
    /// <param name="direction"></param>
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
        onFocusObjectChanged?.Invoke(direction);
    }
    #endregion
    #region �������RotationWindow�ģ���EventSystem��������Ľӿ��Լ������ʹ�õķ���
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
    }//�������е��ƶ�����������Ȼ���������valueChange�¼�

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
