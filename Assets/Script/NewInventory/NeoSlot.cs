using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Neo{
    public class NeoSlot : ScriptableObject
    {
        public SlotType type;
        public NeoItemBase sortItem;
        public int itemNumber;
    }
    public enum SlotType
    {
        mission,matter,speach,other
    }

}
