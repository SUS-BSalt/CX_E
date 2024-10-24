using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class 背景滚动 : MonoBehaviour
{
    public Material backgroundMaterial; // 引用背景材质  
    public float scrollSpeed = 0.1f;    // 滚动速度  

    private Vector2 offset;

    void Start()
    {
        if (backgroundMaterial == null)
        {
            backgroundMaterial = GetComponent<Renderer>().material;
        }
    }

    void Update()
    {
        // 更新偏移量  
        offset.x += scrollSpeed * Time.deltaTime;

        // 设置材质的主纹理偏移  
        backgroundMaterial.mainTextureOffset = offset;
    }
}
