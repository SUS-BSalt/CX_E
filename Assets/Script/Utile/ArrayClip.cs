using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTool : MonoBehaviour
{
    static int[] SliceArray(int[] array, int start, int end)
    {
        // ��������Ƿ���Ч  
        if (start < 0 || end > array.Length || start > end)
        {
            throw new ArgumentOutOfRangeException();
        }

        // ����һ���µ��������洢��Ƭ���Ԫ��  
        int[] slicedArray = new int[end - start];

        // ��ԭʼ�����е�Ԫ�ظ��Ƶ���������  
        Array.Copy(array, start, slicedArray, 0, slicedArray.Length);

        return slicedArray;
    }
}
