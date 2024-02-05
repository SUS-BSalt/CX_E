using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemObjBase:MonoBehaviour
{
    public int BaseValue;

    public int ModifyedValue;

    public string ID;

    public string ItemDiscribe;

    public virtual GameObject GetInstance()
    {
        var prefab = Resources.Load<GameObject>("Assets/Prefab/Inventory/基本物品.prefab");
        GameObject obj =  Instantiate(prefab);
        return obj;
    }
}
