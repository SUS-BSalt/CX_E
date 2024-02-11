using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : ItemBase
{

    public new const string ItemClassName = "TestItem";
    public int someBrandNewMember = 1;
    public void someBrandNewMethod(string _msg)
    {
        Debug.Log("_msg");
    }

    public TestItem(string _JsonString)
    {
        SetProfileFromJson(_JsonString);
        ItemType = Resources.Load<ItemTypeOther>("SO/ItemTypeOther");
    }
    public TestItem()
    {
        ItemType = Resources.Load<ItemTypeOther>("SO/ItemTypeOther");
    }
    public override void SetProfileFromJson(string _JsonString)
    {
        
    }

    public override GameObject GetInstance()
    {
        throw new System.NotImplementedException();
    }

    public override string GetProfileJson()
    {
        return "{}";
    }

    public override bool AreTheySame(ItemBase _otherItem)
    {
        if (_otherItem.GetType() == GetType())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override ItemBase getADeepCopy()
    {
        return new TestItem("{}");
    }
}
