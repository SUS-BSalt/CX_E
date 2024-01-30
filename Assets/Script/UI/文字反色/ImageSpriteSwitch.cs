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
    public void SwitchFlip(bool isLeaveDefault)//鉴于有的选手默认状态为true，有的选手默认状态为false，所以提供了这个翻转的方法
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
