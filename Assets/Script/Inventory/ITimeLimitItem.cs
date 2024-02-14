using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimeLimitItem
{
    public int leftTime { get; set; }
    public void OnTimePass(int _passedDay);
    public void OnTimeExtended(int _extendedDay);
}
