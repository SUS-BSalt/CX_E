using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 线条由两个点连接而成，故我们需要一个容器记录来记录点的位置等信息，另一个容器来记录点与点之间的连线
/// 要生成一条线，需要先注册两个点，然后再用这两个点的编号来生成线条
/// </summary>
public class LineManager : MonoBehaviour
{
    public GameObject Container;
    public GameObject LinePrefab;
    public List<Vector3> Points = new();
    public List<List<LinkInfo>> LineFrom = new();
    public List<List<LinkInfo>> LineTo = new();

    /// <summary>
    /// 注册一个新的点，返回这个新点的编号
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public int RegisterPoint(Vector3 position)
    {
        Points.Add(position);
        LineFrom.Add(new());
        LineTo.Add(new());
        return Points.Count - 1;
    }
    public bool CheckLinkExist(int start ,int end)
    {
        foreach(LinkInfo link in LineFrom[start])
        {
            if(link.endPointIndex == end)
            {
                return true;
            }
        }
        return false;
    }
    public GameObject FindLineInstance(int start,int end)
    {
        foreach (LinkInfo link in LineFrom[start])
        {
            if (link.endPointIndex == end)
            {
                return link.instance;
            }
        }
        return null;
    }
    public bool LinkPoint(int start,int end,float width)
    {
        if (CheckLinkExist(start,end))
        {
            return false;
        }//线条存在则不操作

        GameObject line = Instantiate(LinePrefab, Container.transform);
        line.GetComponent<UIConnectLine>().SetLine(Points[start], Points[end],width);
        LinkInfo link = new(start, end, line);
        LineFrom[start].Add(link);
        LineTo[end].Add(link);
        return true;
    }


};
public class LinkInfo
{
    public int startPointIndex;
    public int endPointIndex;
    public GameObject instance;
    public LinkInfo(int startPint,int endPoint, GameObject instance)
    {
        startPointIndex = startPint;
        endPointIndex = endPoint;
        this.instance = instance;
    }
}

