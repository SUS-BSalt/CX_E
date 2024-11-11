using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ���������������Ӷ��ɣ���������Ҫһ��������¼����¼���λ�õ���Ϣ����һ����������¼�����֮�������
/// Ҫ����һ���ߣ���Ҫ��ע�������㣬Ȼ��������������ı������������
/// </summary>
public class LineManager : MonoBehaviour
{
    public GameObject Container;
    public GameObject LinePrefab;
    public List<Vector3> Points = new();
    public List<List<LinkInfo>> LineFrom = new();
    public List<List<LinkInfo>> LineTo = new();

    /// <summary>
    /// ע��һ���µĵ㣬��������µ�ı��
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
        }//���������򲻲���

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

