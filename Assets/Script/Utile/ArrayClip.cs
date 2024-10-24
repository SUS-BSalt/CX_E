using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HTool : MonoBehaviour
{
    static int[] SliceArray(int[] array, int start, int end)
    {
        // 检查索引是否有效  
        if (start < 0 || end > array.Length || start > end)
        {
            throw new ArgumentOutOfRangeException();
        }

        // 创建一个新的数组来存储切片后的元素  
        int[] slicedArray = new int[end - start];

        // 将原始数组中的元素复制到新数组中  
        Array.Copy(array, start, slicedArray, 0, slicedArray.Length);

        return slicedArray;
    }
}
