using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        var reader = QuickSaveReader.Create("SaveFiled (1)");
        reader = QuickSaveReader.Create("SaveFiled (1)");
        print("testing");
    }

}
