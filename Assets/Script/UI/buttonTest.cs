using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectableOBJ : Selectable
{
    public UnityEvent<GameObject> BeSelected;
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        BeSelected?.Invoke(gameObject);
        //print(gameObject.name);
    }

    //public override void OnPointerEnter(PointerEventData eventData)
    //{
    //    //base.OnPointerEnter(eventData);
    //    OnSelect(eventData);
    //}
    //public override void OnPointerExit(PointerEventData eventData)
    //{
    //    //base.OnPointerExit(eventData);
    //    OnDeselect(eventData);
    //}
}
