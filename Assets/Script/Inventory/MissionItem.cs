using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : ItemObjBase
{
    public string MissionName;
    public Text text;

    public MissionItem()
    {

    }
    public override void InitObj(ItemData itemData)
    {
        data = itemData;
        text.text = data.ItemDiscribe;
    }
}
