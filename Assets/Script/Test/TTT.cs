using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TestClassB :MonoBehaviour
{
    private void Start()
    {
        ItemBase item = ItemFactory.CreateItem("TestItem", "{}");
        if (item is TestItem _item)
        {
            _item.someBrandNewMethod("win!");
        }
    }
}
