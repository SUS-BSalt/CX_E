using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TradeManager : MonoBehaviour
{
    public GameObject TraderObj;
    public int leftValue;
    public int rightValue;
    [Space]
    public InventoryUI TraderLeft;
    public InventoryUI TraderRight;
    public InventoryUI TraderPlayer;
    public InventoryUI TraderTarget;
    [Space]
    private Inventory TraderLeftI;
    private Inventory TraderRightI;
    public GameObject currentSelect;
    [Space]
    public ButtonOBJ ButtonTraderLeft;
    public ButtonOBJ ButtonTraderPlayer;
    public ButtonOBJ ButtonTraderRight;
    public ButtonOBJ ButtonTraderTarget;
    public enum TradeEndType { Failed, FavorableToPlayer , Equal, FavorableToNPC}
    public UnityEvent<TradeEndType> TradeEnd;
    public void Awake()
    {
        TradeEnd = new();
    }
    public void StartTrader(string TargetInventoryID)
    {
        TraderObj.SetActive(true);
        LinkToInventory(TargetInventoryID);
    }
    public void CloseTrader()
    {
        TraderObj.SetActive(false);
        ButtonTraderLeft.onClick.RemoveAllListeners();
        ButtonTraderPlayer.onClick.RemoveAllListeners();
        ButtonTraderRight.onClick.RemoveAllListeners();
        ButtonTraderTarget.onClick.RemoveAllListeners();
        TraderLeft.UIOnSelect.RemoveAllListeners();
        TraderRight.UIOnSelect.RemoveAllListeners();
        TraderTarget.UIOnSelect.RemoveAllListeners();
        TraderPlayer.UIOnSelect.RemoveAllListeners();
    }
    public void LinkToInventory(string TargetInventoryID)
    {
        ButtonTraderLeft.onClick.AddListener(ButtonTraderLeftMethod);
        ButtonTraderPlayer.onClick.AddListener(ButtonTraderPlayerMethod);
        ButtonTraderRight.onClick.AddListener(ButtonTraderRightMethod);
        ButtonTraderTarget.onClick.AddListener(ButtonTraderRightMethod);
        TraderLeftI = new();
        TraderLeft.LinkToInventory(TraderLeftI);
        TraderLeft.UIOnSelect.AddListener(UIOnSelect);
        TraderRightI = new();
        TraderRight.LinkToInventory(TraderRightI);
        TraderRight.UIOnSelect.AddListener(UIOnSelect);
        TraderTarget.LinkToInventory(InventoryManager.Instance.GetInventory(TargetInventoryID));
        TraderTarget.UIOnSelect.AddListener(UIOnSelect);
        TraderPlayer.LinkToInventory(InventoryManager.Instance.GetInventory("Player"));
        TraderPlayer.UIOnSelect.AddListener(UIOnSelect);
    }
    public void UIOnSelect(GameObject obj)
    {
        currentSelect = obj;
    }
    public void PushItemFromSelectToTarget(InventoryUI Select,InventoryUI Target)
    {
        if(currentSelect != Select.gameObject)
        {
            return;
        }
        ItemBase item = Select.currentSelectSlot.slot.item;

        Target.inventory.AddItemWithAddSlotAuto(item, Select.inventory.RequestItem(item, 1));
    }
    public void ButtonTraderLeftMethod()
    {
        PushItemFromSelectToTarget(TraderLeft, TraderPlayer);
    }
    public void ButtonTraderPlayerMethod()
    {
        PushItemFromSelectToTarget(TraderPlayer, TraderLeft);
    }
    public void ButtonTraderRightMethod()
    {
        PushItemFromSelectToTarget(TraderRight, TraderTarget);
    }
    public void ButtonTraderTargetMethod()
    {
        PushItemFromSelectToTarget(TraderTarget, TraderRight);
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