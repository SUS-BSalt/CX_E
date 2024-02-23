using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : ItemBase
{
    public int someBrandNewMember = 1;
    public override ItemTypeBase ItemType { get; set; }

    public void someBrandNewMethod(string _msg)
    {
        Debug.Log("_msg");
    }

    public TestItem(string _JsonString)
    {
        SetProfileFromJson(_JsonString);
        ItemType = Resources.Load<ItemTypeBase>("SO/Inventory/ItemType/TypeOther");
    }
    public TestItem()
    {
        ItemType = Resources.Load<ItemTypeBase>("SO/Inventory/ItemType/TypeOther");
    }
    public override void SetProfileFromJson(string _JsonString)
    {
        
    }

    public override GameObject GetInstance(string _Profile)
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

    public override ItemBase GetADeepCopy()
    {
        return new TestItem("{}");
    }

    public override void SetProfileFromTable(ITableDataReader tableReader, int rowIndex)
    {
        throw new NotImplementedException();
    }
}
