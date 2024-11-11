using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineTest : MonoBehaviour
{
    public LineRenderer Line;
    public List<GameObject> Targets;
    private void Start()
    {
        Init();
    }
    public void Init()
    {
        Line.positionCount = 0;
        foreach(GameObject target in Targets)
        {
            AddPosition(target.transform.localPosition);
        }
    }
    public void AddPosition(Vector3 pos)
    {
        Line.positionCount += 1;
        Line.SetPosition(Line.positionCount-1, pos);
    }
}
