using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class 主菜单控制设置 : MonoBehaviour,IInputController
{
    public PlayerInputActions inputActions;
    public AudioClip BackGroundSound;
    public List<Toggle> toggles;
    void IInputController.OnGetControll()
    {
        inputActions.Enable();
    }

    void IInputController.OnLoseControll()
    {
        inputActions.Disable();
    }
    public void CloseAllToggles()
    {
        foreach(Toggle toggle in toggles)
        {
            toggle.isOn = false;
        }
    }
    public void PlayBackGroundEffect()
    {
        SoundManager.Instance.PlayLoopSound(BackGroundSound,0.7f);
    }
    private void Awake()
    {
        PlayBackGroundEffect();
        inputActions = new();
        inputActions.Disable();
        inputActions.Dialog.Disable();
        inputActions.PlayerUI.Enable();
        InputManager.Instance.SetController(this);
        //inputActions.PlayerUI.Confirm.canceled += a => A();
        //inputActions.PlayerUI.Cancel.canceled += a => B();
    }
}
