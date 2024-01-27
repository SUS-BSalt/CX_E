using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColorSwitch : MonoBehaviour
{
    /// <summary>
    /// 注意设置的颜色的透明度
    /// </summary>
    public Color colorA;
    public Color colorB;
    public Text text;
    public bool isColorA = true;
    public void Start()
    {
        ToColorA();
    }
    public void Switch()
    {
        if (isColorA)
        {
            ToColorB();
        }
        else
        {
            ToColorA();
        }
    }
    public void ToColorA()
    {
        text.color = colorA;
        isColorA = true;
    }
    public void ToColorB()
    {
        text.color = colorB;
        isColorA = false;
    }
}
