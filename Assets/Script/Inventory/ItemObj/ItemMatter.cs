using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMatter : ItemBase
{
    public override ItemTypeBase ItemType { get; set; }
    public override int ItemID { get => _ItemID; set => _ItemID = value; }
    private int _ItemID;

    public ItemMatter()
    {
        ItemType = Resources.Load<ItemTypeBase>("SO/Inventory/ItemType/TypeMatter");
        MSG = new();
    }

    public override bool AreTheySame(ItemBase _otherItem)
    {
        return (_otherItem.ItemID == this.ItemID);
    }

    public override ItemBase GetADeepCopy()
    {
        var copy = new ItemMatter();

        return copy;
    }

    public override GameObject GetInstance(string _Profile)
    {
        throw new System.NotImplementedException();
    }

    public override string GetProfileJson()
    {
        throw new System.NotImplementedException();
    }

    public override void SetProfileFromJson(string _JsonString)
    {
        throw new System.NotImplementedException();
    }

    public override void SetProfileFromTable(ITableDataReader tableReader, int rowIndex)
    {
        ItemID = rowIndex;

    }
}

public class ItemMoney : ItemBase
{
    public override ItemTypeBase ItemType { get; set; }
    public override int ItemID { get => _ItemID; set => _ItemID = value; }
    private int _ItemID;
    private ItemMSGBoard _MSG = new();

    public ItemMoney()
    {
        ItemType = Resources.Load<ItemTypeBase>("SO/Inventory/ItemType/TypeMatter");
    }

    public override bool AreTheySame(ItemBase _otherItem)
    {
        return (_otherItem.ItemID == this.ItemID);
    }

    public override ItemBase GetADeepCopy()
    {
        var copy = new ItemMoney();
        copy.MSG = _MSG;
        copy.ItemID = ItemID;
        copy.ItemType = ItemType;
        return copy;
    }

    public override GameObject GetInstance(string _Profile)
    {
        return  Resources.Load<GameObject>("Prefab/Inventory/ItemOBJ/Money");
    }

    public override string GetProfileJson()
    {
        return "{}";
    }

    public override void SetProfileFromJson(string _JsonString)
    {
        
    }

    public override void SetProfileFromTable(ITableDataReader tableReader, int rowIndex)
    {
        base.SetProfileFromTable(tableReader,rowIndex);
    }
}


