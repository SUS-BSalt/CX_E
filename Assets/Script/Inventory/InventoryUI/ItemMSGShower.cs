using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMSGShower : MonoBehaviour
{

    public Text ItemName;
    public Text ItemDescribe;
    public Text ItemTabs;

    public void UpdateView(ItemSlotUI slotUI)
    {
        if (slotUI.slot.isClean)
        {
            ItemName.text = "";
            ItemDescribe.text = "";
            ItemTabs.text = "";
            return;
        }
        ItemName.text = DataManager.Instance.GetData<string>("Profile", "ItemName", slotUI.slot.item.ItemID.ToString(), DataManager.Instance.GetData<int>("Profile", "LocalOption", "2", "2").ToString());
        ItemDescribe.text = DataManager.Instance.GetData<string>("Profile", "ItemDiscribe", slotUI.slot.item.ItemID.ToString(), DataManager.Instance.GetData<int>("Profile", "LocalOption", "2", "2").ToString());
        ItemTabs.text = TabFunc(slotUI.slot.item);
    }
    public string TabFunc(ItemBase item)
    {
        string output = "";
        List<string> tabnames = new();
        foreach (int tabIndex in item.ItemTabs)
        {
            output += "[";
            output += $"<color={DataManager.Instance.GetData<string>("Profile", "ItemTabs", tabIndex.ToString(),"1")}>";
            output += DataManager.Instance.GetData<string>("Profile", "ItemTabs", tabIndex.ToString(), DataManager.Instance.GetData<int>("Profile", "LocalOption", "2", "2").ToString());
            output += "</color>";
            output += "]";
        }
        return output;
    }
    public void CleanView()
    {
        ItemName.text = "";
        ItemDescribe.text = "";
        ItemTabs.text = "";
    }
}
