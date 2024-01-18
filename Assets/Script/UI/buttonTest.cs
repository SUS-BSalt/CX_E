using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class buttonTest : Selectable
{
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Debug.Log("yes?");
    }
    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        Debug.Log("poyes?");
    }
}
