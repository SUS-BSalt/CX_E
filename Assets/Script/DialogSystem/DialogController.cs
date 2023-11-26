using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour, IInputController
{
    public PlayerInputActions inputActions;
    public void OnGetControll()
    {
        inputActions.Enable();
    }

    public void OnLoseControll()
    {
        inputActions.Disable();
    }
    private void Awake()
    {
        inputActions.Dialog.Enable();
    }
    private void InitActionMap()
    {

    }

}
