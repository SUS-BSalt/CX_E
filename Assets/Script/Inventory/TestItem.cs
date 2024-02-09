using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : ItemBase
{

    public const string ItemName = "TestItem";
    

    public TestItem(string _JsonString)
    {
        SetProfileFromJson(_JsonString);
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
