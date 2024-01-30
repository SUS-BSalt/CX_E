using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorSwitch : MonoBehaviour
{
    public Color A;
    public Color B;
    public Text text;
    public bool isA = true;
    public bool isATheDefault = true;
    private void Start()
    {
        if (isATheDefault)
        {
            ToA();
        }
        else
        {
            ToB();
        }
    }
    public void Switch()
    {
        if (isA)
        {
            ToB();
        }
        else
        {
            ToA();
        }
    }
    public void Switch(bool isToDefault)
    {
        if ((isToDefault & isATheDefault) || (!isToDefault & !isATheDefault))
        {
            ToA();
        }
        else
        {
            ToB();
        }
    }
    public void SwitchFlip(bool isLeaveDefault)//�����е�ѡ��Ĭ��״̬Ϊtrue���е�ѡ��Ĭ��״̬Ϊfalse�������ṩ�������ת�ķ���
    {
        if ((!isLeaveDefault & isATheDefault) || (isLeaveDefault & !isATheDefault))
        {
            ToA();
        }
        else
        {
            ToB();
        }
    }
    public void ToA()
    {
        text.color = A;
        isA = true;
    }
    public void ToB()
    {
        text.color = B;
        isA = false;
    }
}
