using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionItem : ItemObjBase
{
    public string MissionName;
    public Text text;
    public override GameObject GetInstance()
    {
        return base.GetInstance();
    }
}
