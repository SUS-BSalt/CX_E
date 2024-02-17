using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeManager : MonoBehaviour
{
    public int leftValue;
    public int rightValue;
    public Inventory TraderLeft;
    public Inventory TraderRight;
    public void PushItemToInventory()
    {

    }
    public void CombineInventory(Inventory src,Inventory target)
    {

    }
    public void TradeCheck()
    {

    }
    public void UpdateValueOnScreen()
    {

    }

}

public class ItemValueBuffBase
{
    public string BuffName;
    public string BuffDescribe;
    public virtual int ValueCheck(ItemBase _item)
    {
        return _item.value;
    }
}