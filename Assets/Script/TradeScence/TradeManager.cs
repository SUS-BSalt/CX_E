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
    private void Start()
    {
        FinishButton.onClick.AddListener(FinishMethod);
        RejectButton.onClick.AddListener(RejectMethod);
        TraderLeft.SlotOnClick += SlotOnClick;
        TraderRight.SlotOnClick += SlotOnClick;
        TraderTarget.SlotOnClick += SlotOnClick;
        TraderPlayer.SlotOnClick += SlotOnClick;
    }
    public void LinkToInventory(string TargetInventoryID)
    {
        TraderLeftI = new();
        TraderLeft.LinkToInventory(TraderLeftI);
        
        TraderRightI = new();
        TraderRight.LinkToInventory(TraderRightI);
        
        TraderTarget.LinkToInventory(InventoryManager.Instance.GetInventory(TargetInventoryID));
        TraderPlayer.LinkToInventory(InventoryManager.Instance.GetInventory("Player"));

        limitValue = InventoryManager.Instance.GetInventory(TargetInventoryID).Trust;
        UpdateValueOnScreen();
    }
    public void SlotOnClick(ItemSlotUI slotUI)
    {
        if(slotUI.parent == TraderLeft)
        {
            PushItemFromSelectToTarget(slotUI, TraderPlayer);
        }
        else if(slotUI.parent == TraderPlayer)
        {
            PushItemFromSelectToTarget(slotUI, TraderLeft);
        }
        else if (slotUI.parent == TraderRight)
        {
            PushItemFromSelectToTarget(slotUI, TraderTarget);
        }
        else if (slotUI.parent == TraderTarget)
        {
            PushItemFromSelectToTarget(slotUI, TraderRight);
        }
    }
    public void PushItemFromSelectToTarget(ItemSlotUI slotUI, InventoryUI Target)
    {
        //print("here");
        ItemBase item = slotUI.slot.item;
        Target.inventory.AddItemWithAddSlotAuto(item, slotUI.parent.inventory.RequestItem(item, 1));
        slotUI.parent.inventory.RemoveCleanSlot();
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