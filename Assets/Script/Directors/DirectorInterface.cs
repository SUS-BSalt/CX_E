using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirector
{
    public IPerformance currentPerformance { get; set; }
    public void NextStep();
}
/// <summary>
/// 这个接口所承担的位置应该像树结构中的分支一样，由IDirector导演这个父节点集中管理，Director调用其方法，并将反馈返回给调用自己的Director
/// </summary>
public interface IPerformance
{
    public IDirector BaseDirector { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Caller"></param>
    public void PerformanceStart(IDirector Caller);//这个方法由导演调用，场景只需要在结束时调用导演的NextStep方法
    public void PerformanceEnd();//这两个方法由导演调用，场景只需要在结束时调用导演的NextStep方法
}