using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageSpriteSwitch : MonoBehaviour
{
    public Sprite A;
    public Sprite B;
    public UnityEngine.UI.Image img;
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
        img.sprite = A;
        isA = true;
    }
    public void ToB()
    {
        img.sprite = B;
        isA = false;
    }
}
