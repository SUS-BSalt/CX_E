using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConnectLine : MonoBehaviour
{
    /// <summary>
    /// ��Ԥ����ӦΪһ����ƽ�ģ�����������߶�ͷ������
    /// </summary>
    public void SetLine(Vector3 startPoint, Vector3 endPoint, float width)
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        // �Ƕȼ���
        Vector3 dir = endPoint - startPoint;

        float angle = Vector3.Angle(dir, Vector3.right);


        // ���볤�ȣ�ƫת����
        rectTransform.Rotate(0, 0, angle);

        float distance = Vector2.Distance(endPoint, startPoint);

        rectTransform.sizeDelta = new Vector2(distance, width);

        // ����λ��
        rectTransform.localPosition = startPoint;
    }
}
