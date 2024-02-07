using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int BaseValue;

    public int ModifyedValue;

    public string ID;

    public string ItemType;

    public string ItemDiscribe;

    public Dictionary<string, string> AdditionalData;

    public ItemData(int _BaseValue, string _ID, string _ItemType, string _ItemDiscribe)
    {
        BaseValue = _BaseValue;
        ID = _ID;
        ItemType = _ItemType;
        ItemDiscribe = _ItemDiscribe;
        AdditionalData = new();
    }
}
