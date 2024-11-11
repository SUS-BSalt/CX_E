using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 该类与项目耦合，负责生成地图上的标签
/// </summary>
public class MapLabelManager : MonoBehaviour
{
    public GameObject LabelPrefab;
    public GameObject Container;
    public Dictionary<string, MapLabelInfo> Labels = new();
    public LineManager lineManager;
    public void AddLabel(string LabelName,Vector2 anchroPos)
    {
        GameObject label = Instantiate(LabelPrefab, Container.transform);
        label.GetComponent<MapLabel>().SetLabelName(LabelName);
        RectTransform labelTransform = label.GetComponent<RectTransform>();
        labelTransform.anchoredPosition = anchroPos;
        int PointIndex = lineManager.RegisterPoint(labelTransform.localPosition);
        Labels.Add(LabelName, new(PointIndex, label));
    }
    public void LinkLabel(string LabelA,string LabelB, float width)
    {
        //判断条件会自动执行连接，返回连接是否成功，如果线条已经存在，则将线条设置为active true
        if(!lineManager.LinkPoint(Labels[LabelA].PointIndex, Labels[LabelB].PointIndex, width))
        {
            lineManager.FindLineInstance(Labels[LabelA].PointIndex, Labels[LabelB].PointIndex).SetActive(true);
        }
    }
    public void CutLink(string LabelA, string LabelB)
    {
        lineManager.FindLineInstance(Labels[LabelA].PointIndex, Labels[LabelB].PointIndex).SetActive(false);
    }
}
public class MapLabelInfo
{
    public int PointIndex;
    public GameObject instance;
    public MapLabelInfo(int index,GameObject _instance)
    {
        PointIndex = index;
        instance = _instance;
    }
}
