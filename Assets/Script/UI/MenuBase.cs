using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MenuBase : MonoBehaviour
{
    public UnityEvent OnEnterEvent;
    public UnityEvent OnExitEvent;
    public virtual void OnExit(MenuBase targetMenu)
    {
        OnExitEvent?.Invoke();
        gameObject.SetActive(false);
        targetMenu.OnEnter(this);
    }
    public virtual void OnEnter(MenuBase initiateMenu)
    {
        OnEnterEvent?.Invoke();
        gameObject.SetActive(true);
    }
    public virtual void OnToggleEnter()
    {
        OnEnterEvent?.Invoke();
    }
    public virtual void OnToggleExit()
    {
        OnExitEvent?.Invoke();
    }
    public virtual void Toggle(bool toggle)
    {
        if (toggle)
        {
            OnToggleEnter();
        }
        else
        {
            OnToggleExit();
        }
    }
}
