using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public delegate void _SlotUnselect();
public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    public ItemMSGShower Shower;

    public InventoryTabUI TabUI;

    public Dictionary<ItemSlot, ItemSlotUI> UIMap;

    public GameObject container;

    public ItemSlotUI CurrentSelect;

    public _ItemSlotOnSelect SlotOnSelect;
    public event _SlotUnselect SlotOnUnselect;
    public event _ItemSlotOnSelect SlotOnClick;
    public UnityEvent<ItemSlotUI> SlotDoubleClick = new();
    public void LinkToInventory(Inventory _inventory)
    {
        SlotDoubleClick.AddListener(x => { print(x.slot.item.ItemID+"选择"); });
        if (inventory == _inventory)
        {
            InitUI();
            return;
        }
        if (inventory != null)
        {
            inventory.SlotAdded.RemoveListener(AddSlot);
            inventory.SlotRemoved.RemoveListener(RemoveSlot);
        }
        inventory = _inventory;
        InitUI();
        //print(_inventory == null);
        inventory.SlotAdded.AddListener(AddSlot);
        inventory.SlotRemoved.AddListener(RemoveSlot);
    }
    public void InitUI()
    {
        if(UIMap != null)
        {
            //清除原实例
            foreach (ItemSlot Slot in UIMap.Keys)
            {
                Destroy(UIMap[Slot].gameObject);
            }
        }
        UIMap = new();

        foreach (ItemSlot slot in inventory.slots)
        {
            //print(slot == null);
            AddSlotUI(slot);
        }
        TabUI.Init(GetAllTab());
    }
    public void CleanSlotBar(GameObject bar)
    {
        Transform[] children = new Transform[bar.transform.childCount];
        for(int i = 0; i < bar.transform.childCount; i++)
        {
            children[i] = bar.transform.GetChild(i);
        }
        foreach(Transform child in children)
        {
            Destroy(child.gameObject);
        }
    }
    public void AddSlotUI(ItemSlot slot)
    {
        GameObject prefab = Resources.Load<GameObject>("Prefab/Inventory/Slot/SlotA");
        GameObject obj = Instantiate(prefab, container.transform);

        var _slotUI = obj.GetComponent<ItemSlotUI>();
        _slotUI.OnSelect += SlotBeSelect;
        _slotUI.OnUnselect += SlotBeUnselect;
        _slotUI.OnClick += SlotBeClick;
        _slotUI.SlotChanged += s => TabUI.Init(GetAllTab());
        _slotUI.parent = this;
        _slotUI.LinkToSlot(slot);
        UIMap.Add(slot, _slotUI);
    }
    public void AddSlot(List<ItemSlot> slots)
    {
        foreach (ItemSlot slot in slots)
        {
            AddSlotUI(slot);
        }
        TabUI.Init(GetAllTab());
    }
    public void RemoveSlot(List<ItemSlot> slots)
    {
        foreach(ItemSlot slot in slots)
        {
            UIMap[slot].OnSelect -= SlotBeSelect;
            UIMap[slot].OnUnselect -= SlotBeUnselect;
            UIMap[slot].OnClick -= SlotBeClick;
            Destroy(UIMap[slot].gameObject);
            UIMap.Remove(slot);
        }
        TabUI.Init(GetAllTab());

    }
    //其实只要UGUI触发Highlight就会触发它
    public void SlotBeSelect(ItemSlotUI slotUI)
    {
        Shower.UpdateView(slotUI);
        SlotOnSelect?.Invoke(slotUI);
       //print("Select here?");
    }
    //同理，这个其实是UnHighlight
    public void SlotBeUnselect(ItemSlotUI slotUI)
    {
        Shower.CleanView();
        SlotOnUnselect?.Invoke();
        //print("Unselect here?");
    }
    public void SlotBeClick(ItemSlotUI slotUI)
    {
        if(CurrentSelect != slotUI)
        {
            CurrentSelect = slotUI;
        }
        else
        {
            SlotDoubleClick?.Invoke(slotUI);
        }
        Shower.UpdateView(slotUI);
        SlotOnClick?.Invoke(slotUI);
        //print("Click here?");
    }

    public void 筛选(int TabInd)
    {
        if(TabInd == 1)
        {
            foreach (ItemSlot slot in UIMap.Keys)
            {
                UIMap[slot].gameObject.SetActive(true);
            }
            return;
        }
        foreach(ItemSlot slot in UIMap.Keys)
        {
            if (slot.isClean)
            {
                UIMap[slot].gameObject.SetActive(false);
            }
            else
            {
                UIMap[slot].gameObject.SetActive(slot.item.IsTabExist(TabInd));
            }
        }
    }
    public List<int> GetAllTab()
    {
        List<int> output = new();
        foreach (ItemSlot slot in UIMap.Keys)
        {
            if (slot.isClean)
            {
            }
            else
            {
                foreach(int Ind in slot.item.ItemTabs)
                {
                    if (!output.Contains(Ind))
                    {
                        output.Add(Ind);
                    }
                }
            }
        }
        output.Add(1);
        output.Sort();
        return output;
    }
    private void Awake()
    {
        TabUI.CallE.AddListener(筛选);
    }
}
