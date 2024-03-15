using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string testString;
    public List<string> s = new(100);
    private void Start()
    {
        print(s.Count);
        s[10] = "1";
        s[200] = "2";
    }
}


