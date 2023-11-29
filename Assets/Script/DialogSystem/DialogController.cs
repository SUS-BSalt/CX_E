using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour, IInputController
{
    public PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new();
    }
    public void OnGetControll()
    {
        inputActions.Enable();
    }

    public void OnLoseControll()
    {
        inputActions.Disable();
    }
    private void OnEnable()
    {
        inputActions.Dialog.Enable();
    }
    private void OnDisable()
    {
        inputActions.Dialog.Disable();
    }
    private void InitActionMap()
    {

    }

}
