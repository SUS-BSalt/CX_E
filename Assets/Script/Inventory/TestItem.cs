using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItem : ItemBase
{

    public new const string ItemName = "TestItem";

    public override bool canStacking { get { return _canStacking; }set { _canStacking = value; } }
    private bool _canStacking = true;



    public TestItem(string _JsonString)
    {

    }

    public override void DeSerializeFromJson(string _JsonString)
    {
        
    }

    public override GameObject GetInstance(string _profile)
    {
        throw new System.NotImplementedException();
    }

    public override string SerializeToJson()
    {
        return "{}";
    }
}
