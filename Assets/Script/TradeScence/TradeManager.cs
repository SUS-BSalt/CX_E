using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TradeManager : MonoBehaviour
{
    public GameObject TraderObj;
    public GameObject currentSelect;
    public int leftValue;
    public int rightValue;
    public Text leftValueText;
    public Text rightValueText;
    [Space]
    public InventoryUI TraderLeft;
    public InventoryUI TraderRight;
    public InventoryUI TraderPlayer;
    public InventoryUI TraderTarget;
    [Space]
    private Inventory TraderLeftI;
    private Inventory TraderRightI;

    [Space]
    public ButtonOBJ ButtonTraderLeft;
    public ButtonOBJ ButtonTraderPlayer;
    public ButtonOBJ ButtonTraderRight;
    public ButtonOBJ ButtonTraderTarget;
    [Space]
    public UnityEngine.UI.Button FinishButton;
    public UnityEngine.UI.Button RejectButton;
    public enum TradeEndType { Failed, FavorableToPlayer , Equal, FavorableToNPC}
    public UnityEvent<TradeEndType> TradeEnd;

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
        FinishButton.onClick.AddListener(FinishMethod);
        RejectButton.onClick.AddListener(RejectMethod);
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
        UpdateValueOnScreen();
    }
    public void UIOnSelect(GameObject obj)
    {
        //print(obj.name);
        currentSelect = obj;
    }
    public void PushItemFromSelectToTarget(InventoryUI Select,InventoryUI Target)
    {
        if(currentSelect != Select.gameObject)
        {
            return;
        }
        //print("here");
        ItemBase item = Select.currentSelectSlot.slot.item;
        Target.inventory.AddItemWithAddSlotAuto(item, Select.inventory.RequestItem(item, 1));
        Select.inventory.RemoveCleanSlot();
        Target.inventory.RemoveCleanSlot();
        UpdateValueOnScreen();
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

    public void CombineInventory()
    {
        foreach (ItemSlot slot in TraderLeftI.slots)
        {
            if (slot.item != null)
            {
                TraderPlayer.inventory.AddItemWithAddSlotAuto(slot.item,slot.stackingQuantity);
            }
        }

        foreach (ItemSlot slot in TraderRightI.slots)
        {
            if (slot.item != null)
            {
                TraderTarget.inventory.AddItemWithAddSlotAuto(slot.item, slot.stackingQuantity);
            }
        }
    }
    public void TradeCheck()
    {

        //print("??");
        int dif = leftValue - rightValue;
        if (dif <= -500)
        {
            FinishButton.gameObject.SetActive(false);
            RejectButton.gameObject.SetActive(true);
        }
        else
        {
            FinishButton.gameObject.SetActive(true);
            RejectButton.gameObject.SetActive(false);
        }
    }
    public void FinishMethod()
    {
        int dif = leftValue - rightValue;
        if (dif <= -150)
        {
            TradeEnd?.Invoke(TradeEndType.FavorableToPlayer);
            //print("?");
        }
        else if (dif >= -150 & dif <= 150)
        {
            TradeEnd?.Invoke(TradeEndType.Equal);
            //print("?");
        }
        else if (dif >= 150)
        {
            TradeEnd?.Invoke(TradeEndType.FavorableToNPC);
            //print("?");
        }
        //print("end?");
        CombineInventory();
        CloseTrader();
    }
    public void RejectMethod()
    {
        //print("endr?");
        TradeEnd?.Invoke(TradeEndType.Failed);
        CloseTrader();
    }
    public void UpdateValueOnScreen()
    {
        int tValue = 0;
        foreach(ItemSlot slot in TraderLeftI.slots)
        {
            if (slot.item != null)
            {
                tValue += (TraderPlayer.inventory.GetItemValueAfterBuff(slot.item) * slot.stackingQuantity);
            }
        }
        leftValueText.text = tValue.ToString();
        leftValue = tValue;

        tValue = 0;
        foreach (ItemSlot slot in TraderRightI.slots)
        {
            if (slot.item != null)
            {
                tValue += (TraderTarget.inventory.GetItemValueAfterBuff(slot.item) * slot.stackingQuantity);
            }
        }
        rightValueText.text = tValue.ToString();
        rightValue = tValue;
        TradeCheck();
    }

}