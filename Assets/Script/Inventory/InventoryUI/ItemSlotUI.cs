using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public ItemSlot slot;
    public GameObject itemObj;
    public Text numberText;
    public Selectable UGUIElement;
    public UnityEvent<ItemSlotUI> OnSelect;

    public void LinkToSlot(ItemSlot _slot)
    {
        if(slot != null)
        {
            //slot.SlotChanged -= UpdateSlotUI;
            slot.SlotChanged -= UpdateSlotUI;
        }
        slot = _slot;
        UpdateSlotUI();
        //_slot.SlotChanged += this.UpdateSlotUI;
        slot.SlotChanged += UpdateSlotUI;
    }
    public void UpdateSlotUI()
    {
        if(numberText != null)
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
            itemObj = Instantiate(itemObj, new Vector3(0, 0, 0), Quaternion.identity);
            itemObj.transform.SetParent(transform, false);
            itemObj.transform.SetAsFirstSibling();

        }
    }
    public void BeSelect()
    {
        OnSelect?.Invoke(this);
    }
}
