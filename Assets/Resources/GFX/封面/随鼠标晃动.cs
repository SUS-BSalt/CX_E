using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class 随鼠标晃动 : MonoBehaviour
{
    public float X_move;
    public float Y_move;

    public Vector2 pre_move_range=new(0,0);

    [Range(-0.5f, 0.5f)]
    public float X_range;
    [Range(-0.5f, 0.5f)]
    public float Y_range;


    public void MoveIt(Vector2 range)
    {
        Vector2 move_side = new(X_move, Y_move);
        Vector2 temp = (new Vector2(transform.localPosition.x,transform.localPosition.y) + move_side * (range - pre_move_range));
        transform.localPosition = new Vector3(temp.x, temp.y, transform.localPosition.z);
        pre_move_range = range;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // 获取鼠标在屏幕上的像素位置  
        Vector3 mousePosition = Input.mousePosition;

        // 获取屏幕尺寸（以像素为单位）  
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 将屏幕坐标转换为归一化坐标（0到1之间）  
        Vector2 normalizedPosition = new Vector2(mousePosition.x / screenWidth, mousePosition.y / screenHeight);

        MoveIt(normalizedPosition-new Vector2(0.5f,0.5f));
    }
}
