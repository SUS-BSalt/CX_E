using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBase : MonoBehaviour
{
    public virtual void OnExit(MenuBase targetMenu)
    {
        gameObject.SetActive(false);
        targetMenu.OnEnter(this);
    }
    public virtual void OnEnter(MenuBase initiateMenu)
    {
        gameObject.SetActive(true);
    }
    public virtual void OnToggleEnter()
    {

    }
    public virtual void OnToggleExit()
    {

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
