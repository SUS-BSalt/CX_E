using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class TestClass
{
    public int a = 1;
    [Serialize]
    public TestClassB b;
}



