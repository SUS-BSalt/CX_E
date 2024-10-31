using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WarningWindow : Singleton<WarningWindow>,IInputController
{
    public UnityEvent A_Event;
    public UnityEvent B_Event;
    public Text MainMSG;
    public Text A_MSG;
    public Text B_MSG;
    public Button A_Button;
    public Button B_Button;
    public GameObject 菜单本体;
    public PlayerInputActions inputActions;
    public void Call()
    {
        Init();
        InputManager.Instance.GrabControl(this);
        菜单本体.SetActive(true);
        //EventSystem.firstSelectedGameObject = A_Button.gameObject;
    }

    public void A()
    {
        A_Event?.Invoke();
        菜单本体.SetActive(false);
        InputManager.Instance.RollBack();
    }
    public void B()
    {
        B_Event?.Invoke();
        菜单本体.SetActive(false);
        InputManager.Instance.RollBack();
    }
    public void Init()
    {
        A_Event = new();
        B_Event = new();

    }

    protected override void Awake()
    {
        base.Awake();
        inputActions = new();
        inputActions.Disable();
        inputActions.Dialog.Disable();
        inputActions.PlayerUI.Enable();
        inputActions.PlayerUI.Confirm.canceled += a => A();
        inputActions.PlayerUI.Cancel.canceled += a => B();
    }

    void IInputController.OnGetControll()
    {
        inputActions.Enable();
    }

    void IInputController.OnLoseControll()
    {
        inputActions.Disable();
    }
}
