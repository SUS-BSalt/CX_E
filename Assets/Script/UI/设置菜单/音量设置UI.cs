using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class 音量设置UI : MonoBehaviour
{
    public Slider slider;
    public string optionName;
    public void Awake()
    {
        slider.value = DataManager.Instance.GetData<float>("Profile", "LocalOption", optionName, "2");
        slider.onValueChanged.AddListener(SetValue);
    }
    public void SetValue(float value)
    {
        if(optionName == "3")
        {
            SoundManager.Instance.ChangeMasterVolume(value);
        }
        if (optionName == "4")
        {
            SoundManager.Instance.ChangeMusicVolue(value);
        }
        if (optionName == "5")
        {
            SoundManager.Instance.ChangeEffectVolume(value);
        }
        if (optionName == "6")
        {
            SoundManager.Instance.ChangeBackGroundVolume(value);
        }
    }
}
