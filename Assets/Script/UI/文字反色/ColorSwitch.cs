using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ImageColorSwitch : MonoBehaviour
{
    /// <summary>
    /// ע�����õ���ɫ��͸����
    /// </summary>
    public Color colorA;
    public Color colorB;
    public UnityEngine.UI.Image img;
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
        img.color = colorA;
        isColorA = true;
    }
    public void ToColorB()
    {
        img.color = colorB;
        isColorA = false;
    }
}
