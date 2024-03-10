using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string testString;
    private void Start()
    {
        print(EventString.Unpack(testString)[0]);
    }
}


