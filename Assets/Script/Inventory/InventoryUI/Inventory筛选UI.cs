using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InventoryTabUI : MonoBehaviour
{
    public GameObject TabClickPre;

    public GameObject Container;

    public Dictionary<int, Toggle> TabClicks = new();
    public UnityEvent<int> CallE;
    public int currentToggle;
    public void Init(List<int> tabs)
    {
        //删掉没有的
        List<int> keysToRemove = new List<int>();
        foreach (int i in TabClicks.Keys)
        {
            if (!tabs.Contains(i))
            {
                Destroy(TabClicks[i]);
                keysToRemove.Add(i);
            }
        }
        foreach (var key in keysToRemove)
        {
            TabClicks.Remove(key);
        }

        //生成有的
        foreach (int i in tabs)
        {
            if (!TabClicks.ContainsKey(i))
            {
                TabClicks.Add(i, GenTabClick(i));
            }
        }

        TabClicks[1].isOn = true;
    }

    public Toggle GenTabClick(int tabInd)
    {
        GameObject instance = Instantiate(TabClickPre,Container.transform);
        instance.GetComponentInChildren<Text>().text =
            $"<Color={DataManager.Instance.GetData<string>("Profile", "ItemTabs", tabInd.ToString(), "1")}>"
            + DataManager.Instance.GetData<string>("Profile", "ItemTabs", tabInd.ToString(), DataManager.Instance.GetData<int>("Profile", "LocalOption","2","2").ToString())
            + "</Color>";
        instance.gameObject.name = DataManager.Instance.GetData<string>("Profile", "ItemTabs", tabInd.ToString(), DataManager.Instance.GetData<int>("Profile", "LocalOption", "2", "2").ToString());
        //instance.GetComponent<Toggle>().group = toggleGroup;
        instance.GetComponent<Toggle>().onValueChanged.AddListener(t => { CallInventory(tabInd, t); });
        return instance.GetComponent<Toggle>();
    }
    public void CallInventory(int tabInd, bool toggle)
    {
        
        if (!toggle)
        {
            if(currentToggle == tabInd)
            {
                TabClicks[1].isOn = true;
            }
            return;
        }
        else
        {
            currentToggle = tabInd;
            foreach(int t in TabClicks.Keys)
            {
                if (!(t == currentToggle))
                {
                    TabClicks[t].isOn = false;
                }
            }
            CallE?.Invoke(tabInd);
        }
    }
}
