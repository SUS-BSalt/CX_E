using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLabel : MonoBehaviour
{
    public ImageSpriteSwitch spriteSwitch;
    public TextColorSwitch textColorSwitch;
    public Text text;
    public void SetToActive()
    {
        spriteSwitch.ToA();
        textColorSwitch.ToA();
    }
    public void SetToNormal()
    {
        spriteSwitch.ToB();
        textColorSwitch.ToB();
    }
    public void SetLabelName(string name)
    {
        text.text = name;
    }
}
