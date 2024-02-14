using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemType", menuName = "Inventory/ItemType")]
public class ItemTypeBase : ScriptableObject
{
    public ItemSlot.SlotType SlotType = ItemSlot.SlotType.other;

}
