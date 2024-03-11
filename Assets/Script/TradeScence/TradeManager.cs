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
    public int limitValue;
    [Space]
    public InventoryUI TraderLeft;
    public InventoryUI TraderRight;
    public InventoryUI TraderPlayer;
    public InventoryUI TraderTarget;
    [Space]
    private Inventory TraderLeftI;
    private Inventory TraderRightI;
    [Space]
    public UnityEngine.UI.Button FinishButton;
    public UnityEngine.UI.Button RejectButton;
    public delegate void _TradeEnd();
    public _TradeEnd TradeEnd;
    public void StartTrader(string TargetInventoryID)
    {
        TraderObj.SetActive(true);
        LinkToInventory(TargetInventoryID);
    }
    public void CloseTrader()
    {
        TraderObj.SetActive(false);

    }
    public void LinkToInventory(string TargetInventoryID)
    {
        FinishButton.onClick.AddListener(FinishMethod);
        RejectButton.onClick.AddListener(RejectMethod);
        TraderLeftI = new();
        TraderLeft.LinkToInventory(TraderLeftI);

        TraderRightI = new();
        TraderRight.LinkToInventory(TraderRightI);

        TraderTarget.LinkToInventory(InventoryManager.Instance.GetInventory(TargetInventoryID));

        TraderPlayer.LinkToInventory(InventoryManager.Instance.GetInventory("Player"));

        UpdateValueOnScreen();
    }
    public void SlotOnClick(ItemSlotUI slotUI)
    {
        if(currentSelect == TraderLeft.gameObject)
        {
            PushItemFromSelectToTarget(TraderLeft, TraderPlayer);
        }
        else if(currentSelect == TraderPlayer.gameObject)
        {
            PushItemFromSelectToTarget(TraderPlayer, TraderLeft);
        }
        else if (currentSelect == TraderRight.gameObject)
        {
            PushItemFromSelectToTarget(TraderRight, TraderTarget);
        }
        else if (currentSelect == TraderTarget.gameObject)
        {
            PushItemFromSelectToTarget(TraderTarget, TraderRight);
        }
    }
    public void PushItemFromSelectToTarget(InventoryUI Select,InventoryUI Target)
    {
        //print("here");
        ItemBase item = Select.currentSelectSlot.slot.item;
        Target.inventory.AddItemWithAddSlotAuto(item, Select.inventory.RequestItem(item, 1));
        Select.inventory.RemoveCleanSlot();
        Target.inventory.RemoveCleanSlot();
        UpdateValueOnScreen();
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
        if (dif <= -1*limitValue)
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
        //print("end?");
        CombineInventory();
        CloseTrader();
    }
    public void RejectMethod()
    {
        //print("endr?");
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