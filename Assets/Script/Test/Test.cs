using CI.QuickSave;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Test : MonoBehaviour
{
    public void Start()
    {
        TestSO so = Resources.Load<TestSO>("SO/testSO");
        TestSO so1 = Resources.Load<TestSO>("SO/testSO");
        print(so.a);
        print(so1.a);
        so.a = 2;
        print(so.a);
        print(so1.a);
    }
}


