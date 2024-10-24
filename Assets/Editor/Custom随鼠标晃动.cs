using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(随鼠标晃动))]
public class Custom随鼠标晃动 : Editor
{
    private SerializedProperty X_range;
    private SerializedProperty Y_range;
    private float X_v;
    private float Y_v;

    private void OnEnable()
    {
        // 获取要监控的字段的SerializedProperty  
        X_range = serializedObject.FindProperty("X_range");
        Y_range = serializedObject.FindProperty("Y_range");

        // 初始化previousFieldValue  
        随鼠标晃动 myScript = (随鼠标晃动)target;
        X_v = myScript.X_range;
        Y_v = myScript.Y_range;
    }

    public override void OnInspectorGUI()
    {
        // 绘制默认的Inspector界面  
        DrawDefaultInspector();

        // 应用对SerializedProperty的更改  
        serializedObject.ApplyModifiedProperties();

        // 检查字段是否更改  
        随鼠标晃动 myScript = (随鼠标晃动)target;
        if (X_range.floatValue != X_v || Y_range.floatValue != Y_v)
        {
            // 更新previousFieldValue以反映新值  
            X_v = X_range.floatValue;
            Y_v = Y_range.floatValue;
            // 字段已更改，执行自定义逻辑  
            myScript.MoveIt(new Vector2(X_v,Y_v));
        }
    }
}
