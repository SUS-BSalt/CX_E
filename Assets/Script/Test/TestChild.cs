using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public class TestClass
{
    public int a = 1;
    public virtual void Method()
    {
        Debug.Log("ClassA");
    }
}
public class TestClassB : TestClass
{
    public override void Method()
    {
        Debug.Log("ClassB");
    }
}



