using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        print("test 创建读取");
        var reader = QuickSaveReader.Create("Test");
        print("test 创建写入");
        var writer = QuickSaveWriter.Create("Test");
        writer.Write<int>("0", 0);
        writer.Commit();
        print("test 提交");
        print("test 创建读取");
        reader = QuickSaveReader.Create("Test");
        print(reader.Read<int>("0"));
        print("testing");
    }

}
