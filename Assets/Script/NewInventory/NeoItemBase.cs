using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Neo;
using System;

namespace Neo
{
    /// <summary>
    /// 
    /// </summary>
    public class NeoItemBase : ScriptableObject
    {
        public string prefabPath;

        public Type type = typeof(NeoItemBase);

        public bool IsUnique;//����Ϊ��ʱ�����������Ʒ�ж����������Ҫ����
                             //����Unity�Դ���Resources.Load������ȡ����һ�����ã����κεط�ʹ�����������õ�ͬһ��ʵ����ֱ���޸Ļ����ȫ�ֵ����ݱ仯����������Ʒ��ζ����Ҫ��ȸ���
                             //������Щ��Ʒ�Ƕ��صģ��������;ö��趨���������д���ʱ���ǵĽ��ƣ����ֶ���ʱ�����س�����SOʵ��������������

        public string ItemDataJson;//��������Ҫ���Ᵽ�������ȫ��ת��json�ַ����ȥ������


        //���µ��ֶ�Ϊ���ڱ���Ŀ��ӵ��ֶ�
        public int value;

        public virtual GameObject GetInstance()//������ʵ�������ֿ�󣬲ֿ��ui���Ե��ø÷���ʵ������Ʒ��ͼ��Ԫ��
        {
            var prefab = Resources.Load<GameObject>(prefabPath);
            GameObject obj = Instantiate(prefab);
            return obj;
        }
        public virtual void OnSave()
        {
            //��׷�ӵ��������л���json��Ž�ItemAdditionalJsonData����
        }
        public virtual void OnLoad()
        {

        }
    }
}
