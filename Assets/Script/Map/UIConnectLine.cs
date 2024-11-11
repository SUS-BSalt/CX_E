using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConnectLine : MonoBehaviour
{
    /// <summary>
    /// 该预制体应为一条扁平的，且轴心在左边端头的条形
    /// </summary>
    public void SetLine(Vector3 startPoint, Vector3 endPoint, float width)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        // 角度计算
        Vector3 dir = endPoint - startPoint;

        float angle = Vector3.Angle(dir, Vector3.right);


        // 距离长度，偏转设置
        rectTransform.Rotate(0, 0, angle);

        float distance = Vector2.Distance(endPoint, startPoint);

        rectTransform.sizeDelta = new Vector2(distance, width);

        // 设置位置
        rectTransform.localPosition = startPoint;
    }
}
