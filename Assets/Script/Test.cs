using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        print("test ������ȡ");
        var reader = QuickSaveReader.Create("Test");
        print("test ����д��");
        var writer = QuickSaveWriter.Create("Test");
        writer.Write<int>("0", 0);
        writer.Commit();
        print("test �ύ");
        print("test ������ȡ");
        reader = QuickSaveReader.Create("Test");
        print(reader.Read<int>("0"));
        print("testing");
    }

}
