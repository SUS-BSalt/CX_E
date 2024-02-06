using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemObjBase:MonoBehaviour
{
    public ItemData data;
    
    public ItemObjBase()
    {

    }

    public virtual void OnItemSelect()
    {

    }

    public virtual void InitObj(ItemData itemData)
    {
        data = itemData;
    }
}
