using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDirector
{
    public IPerformance currentPerformance { get; set; }
    public void NextStep();
}
/// <summary>
/// ����ӿ����е���λ��Ӧ�������ṹ�еķ�֧һ������IDirector����������ڵ㼯�й���Director�����䷽���������������ظ������Լ���Director
/// </summary>
public interface IPerformance
{
    public IDirector BaseDirector { get; set; }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Caller"></param>
    public void PerformanceStart(IDirector Caller);//��������ɵ��ݵ��ã�����ֻ��Ҫ�ڽ���ʱ���õ��ݵ�NextStep����
    public void PerformanceEnd();//�����������ɵ��ݵ��ã�����ֻ��Ҫ�ڽ���ʱ���õ��ݵ�NextStep����
}