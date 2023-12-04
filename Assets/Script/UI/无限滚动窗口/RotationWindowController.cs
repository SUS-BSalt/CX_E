using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotationWindow))]
public class RotationWindowController : MonoBehaviour
{
    [Header("��Ҫ���")]
    public Canvas canvas;//������ڵĻ���
    public RotationWindow rotationWindow;
    [Header("�������Լ������ж�")]
    public int FocusObjIndex;//����������ı��

    [Header("״̬")]
    public bool isTouchUpBorder;//�Ƿ������ϱ߽�
    public bool isTouchBottomBorder;//�Ƿ������±߽�
    public bool isControllSelf;//�Ƿ������������ƹ�������

    private void Awake()
    {
        rotationWindow = GetComponent<RotationWindow>();
    }
}
