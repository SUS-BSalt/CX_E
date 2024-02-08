using Neo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public enum SlotType
    {
        mission, matter, speach, other
    }
    public SlotType type;
    public ItemBase item;
    public int number;
    public ItemSlot(SlotType _type)
    {
        type = _type;
    }
    public bool CompareToSlotedItem(ItemBase _item)
    {
        return item.Equals(_item);
    }

}

