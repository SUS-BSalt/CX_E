using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : Singleton<ItemFactory>
{
    public static ItemBase CreateItem(string _ItemName, string _JsonString)
    {
        switch (_ItemName)
        {
            case TestItem.ItemName:
                {
                    return new TestItem(_JsonString);
                }
            default:
                {
                    throw new Exception("没找到对应物品");
                }
        }
        
    }
}

