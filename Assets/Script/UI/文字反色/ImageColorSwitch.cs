using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ImageColorSwitch : MonoBehaviour
{
    /// <summary>
    /// 注意设置的颜色的透明度
    /// </summary>
    public Color A;
    public Color B;
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
    public void Switch(bool isToDefault)
    {
        if ( (isToDefault & isATheDefault) || (!isToDefault & !isATheDefault) )
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
        img.color = A;
        isA = true;
    }
    public void ToB()
    {
        img.color = B;
        isA = false;
    }
}
