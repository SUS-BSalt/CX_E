using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BuffBase",menuName ="Trader/Buff/BuffBase")]
public class BuffBase : ScriptableObject
{
    public string BuffName;
    public string BuffDescribe;
    public virtual int ValueCheck(ItemBase _item)
    {
        return _item.value;
    }
}
