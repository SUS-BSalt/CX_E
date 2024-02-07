using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TM : MonoBehaviour
{
    public List<Test> list;
    void Start()
    {
        list.Add(new Test());
        list.Add(new Test());
        list.Add(new Test());
    }

}
