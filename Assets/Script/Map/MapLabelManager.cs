using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������Ŀ��ϣ��������ɵ�ͼ�ϵı�ǩ
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
        //�ж��������Զ�ִ�����ӣ����������Ƿ�ɹ�����������Ѿ����ڣ�����������Ϊactive true
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
