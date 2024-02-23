using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class TestMissionItem : ItemBase,ITimeLimitItem
{
    public struct TestMissionItemData
    {
        public string MissionName;
        public string MissionDiscripe;
        public int leftTime;
    }
    private TestMissionItemData data;
    public string MissionName { get { return data.MissionName; } set { data.MissionName = value; } }
    public string MissionDiscripe { get { return data.MissionDiscripe; } set { data.MissionDiscripe = value; } }
    public int leftTime { get { return data.leftTime; } set { data.leftTime = value; } }

    public override ItemTypeBase ItemType { get; set; }
    

    public TestMissionItem()
    {
        canStacking = false;
        data = new();
        ItemType = Resources.Load<ItemTypeBase>("SO/Inventory/ItemType/TypeMission");
    }
    public TestMissionItem(string _MissionName,string _MissionDiscripe,int _leftTime)
    {
        canStacking = false;
        data = new();
        ItemType = Resources.Load<ItemTypeBase>("SO/Inventory/ItemType/TypeMission");
        MissionName = _MissionName;
        MissionDiscripe = _MissionDiscripe;
        leftTime = _leftTime;
    }

    public override bool AreTheySame(ItemBase _otherItem)
    {
        if (_otherItem is TestMissionItem _otherMission)
        {
            if(MissionName != _otherMission.MissionName)
            {
                return false;
            }
            else if(leftTime != _otherMission.leftTime)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public override ItemBase GetADeepCopy()
    {
        TestMissionItem newItem = new();
        newItem.SetProfileFromJson(GetProfileJson());
        return newItem;
    }

    public override GameObject GetInstance(string _Profile)
    {
        var obj = Resources.Load<GameObject>("Prefab/Inventory/Money");
        return obj;
        //throw new System.Exception("该物体暂无实体");
    }

    public override string GetProfileJson()
    {
        string _json = JsonConvert.SerializeObject(data);
        return _json;
    }
    public override void SetProfileFromJson(string _JsonString)
    {
        data = JsonConvert.DeserializeObject<TestMissionItemData>(_JsonString);
    }

    public void OnTimeExtended(int _extendedDay)
    {
        throw new System.NotImplementedException();
    }

    public void OnTimePass(int _passedDay)
    {
        throw new System.NotImplementedException();
    }

    public override void SetProfileFromTable(ITableDataReader tableReader, int rowIndex)
    {
        
    }
}
