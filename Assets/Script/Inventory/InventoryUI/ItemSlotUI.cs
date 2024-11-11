using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public delegate void _ItemSlotOnSelect(ItemSlotUI SelectedItemSlotUI);
public class ItemSlotUI : MonoBehaviour
{
    public InventoryUI parent;
    public ItemSlot slot;
    public GameObject itemObj;
    public Text numberText;
    public ButtonOBJ UGUIElement;
    public event _ItemSlotOnSelect OnSelect;
    public event _ItemSlotOnSelect OnUnselect;
    public event _ItemSlotOnSelect OnClick;
    public event _ItemSlotOnSelect SlotChanged;
    private void Awake()
    {
        UGUIElement.BeHighlight.AddListener(ctx=>OnSelect?.Invoke(this));
        //UGUIElement.BeSelected.AddListener(ctx => OnSelect?.Invoke(this));
        UGUIElement.UnHighlight.AddListener(ctx => OnUnselect?.Invoke(this));
        //UGUIElement.UnSelected.AddListener(ctx => OnUnselect?.Invoke(this));
        UGUIElement.onClick.AddListener(() => OnClick?.Invoke(this));
    }
    public void LinkToSlot(ItemSlot _slot)
    {
        if(slot != null)
        {
            //slot.SlotChanged -= UpdateSlotUI;
            slot.SlotChanged.RemoveListener(UpdateSlotUI);
        }
        

        slot = _slot;
        //_slot.SlotChanged += this.UpdateSlotUI;
        slot.SlotChanged.AddListener(UpdateSlotUI);
        UpdateSlotUI();

    }
    public void OnDestroy()
    {
        if (slot != null)
        {
            //slot.SlotChanged -= UpdateSlotUI;
            slot.SlotChanged.RemoveListener(UpdateSlotUI);
        }
    }
    public void UpdateSlotUI()
    {
        if (numberText != null)
        {
            if(slot.stackingQuantity == 0)
            {
                numberText.text = "";
            }
            else
            {
                numberText.text = slot.stackingQuantity.ToString();
            }
        }
        if(itemObj != null)
        {
            Destroy(itemObj);
        }
        itemObj = slot.GetItemUIIsntance();
        if(itemObj != null)
        {
            
            itemObj.transform.SetParent(transform,false);
            itemObj.transform.SetAsFirstSibling();
        }
        SlotChanged?.Invoke(this);
    }
}
