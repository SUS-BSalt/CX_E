using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ToggleOBJ : Toggle
{
    public UnityEvent<GameObject> BeSelected;
    public UnityEvent<GameObject> UnSelected;
    public UnityEvent<GameObject> BeHighlight;
    public UnityEvent<GameObject> UnHighlight;
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        BeSelected?.Invoke(gameObject);
        //print(gameObject.name);
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        UnSelected?.Invoke(gameObject);
    }
    protected override void DoStateTransition(SelectionState state, bool instant)
    {
        base.DoStateTransition(state, instant);
        if (state == SelectionState.Highlighted)
        {
            BeHighlight?.Invoke(gameObject);
        }
        else if (state == SelectionState.Normal)
        {
            UnHighlight?.Invoke(gameObject);
        }
    }
}
