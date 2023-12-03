using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RotationWindow : MonoBehaviour
{
    [Header("��Ҫ���")]

    /// <summary>
    /// ��������Ҫͬʱ��ѡ��ֱ��ˮƽ�ƶ�
    /// </summary>
    public ScrollRect scrollRect;

    public RectTransform content;
    public RectTransform Viewport;
    [Header("�������Լ������ж�")]
    public int FocusObjIndex;//����������ı��

    [Header("״̬")]
    public bool isTouchUpBorder = false;//�Ƿ������ϱ߽�
    public bool isTouchBottomBorder = false;//�Ƿ������±߽�
    public bool isControllSelf = false;

    public class FocusObjectChangedEvent : UnityEvent<bool> { }//true�ǰ�ͷ�ŵ�β��false��β�ŵ�ͷ
    public FocusObjectChangedEvent onFocusObjectChanged;
    private void Awake()
    {
        scrollRect.onValueChanged.AddListener(ChildResortCheck);
    }
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
}
