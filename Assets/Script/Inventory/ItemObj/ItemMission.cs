using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMission : ItemBase , ITimeLimitItem
{
    public override ItemTypeBase ItemType { get; set; }
    public override int ItemID { get => _ItemID; set => _ItemID = value; }
    private int _ItemID;
    public int leftTime { get => data.leftTime; set => data.leftTime = value; }

    public ProfileData data = new();
    public ItemMission()
    {
        ItemType = Resources.Load<ItemTypeBase>("SO/Inventory/ItemType/TypeMission");
        canStacking = false;
    }
    public override bool AreTheySame(ItemBase _otherItem)
    {
        return (_otherItem.ItemID == this.ItemID);
    }

    public override ItemBase GetADeepCopy()
    {
        ///Ã»Ð´Íê
        var copy = new ItemMoney();
        copy.MSG = MSG;
        copy.ItemID = ItemID;
        copy.ItemType = ItemType;
        return copy;
    }

    public override GameObject GetInstance(string _Profile)
    {
        GameObject obj =  Resources.Load<GameObject>("Prefab/Inventory/ItemOBJ/Mission");
        obj.GetComponent<Text>().text = MSG.ItemName;
        return obj;
    }

    public override string GetProfileJson()
    {
        return JsonConvert.SerializeObject(data);
    }

    public override void SetProfileFromJson(string _JsonString)
    {
        data = JsonConvert.DeserializeObject<ProfileData>(_JsonString);
    }

    public override void SetProfileFromTable(ITableDataReader tableReader, int rowIndex)
    {
        base.SetProfileFromTable(tableReader,rowIndex);
        leftTime = tableReader.GetData<int>(rowIndex, 6);
        MSG.ItemLeftTime = leftTime.ToString();
    }

    public void OnTimePass(int _passedDay)
    {
        throw new System.NotImplementedException();
    }

    public void OnTimeExtended(int _extendedDay)
    {
        throw new System.NotImplementedException();
    }
    public struct ProfileData
    {
        public int leftTime;
    }
}
