using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventString
{
    /// <summary>
    /// EventString�Ĳ㼶Ϊ"Event""Method"
    /// </summary>
    /// <param name="_OriginString"></param>
    /// <returns></returns>
    public static List<List<string>> UnpackComplex(string _OriginString)
    {
        string cleanString = _OriginString.Replace("\n", "").Replace("\t", "");
        List<List<string>> Events = new();
        List<string> t_event = new();
        string tempString = "";
        int Index = 0;
        while(Index < cleanString.Length)
        {
            string tempChar =  _OriginString[Index].ToString();
            ///�������
            if (tempChar == "$")
            {
                Index++;
                tempString += _OriginString[Index];
            }
            ///������
            else if (tempChar == "{")
            {
                ///���
                int deepth = 0;
                Index++;
                while (true)
                {
                    if(_OriginString[Index].ToString() == "{")
                    {
                        deepth++;
                    }
                    else if(_OriginString[Index].ToString() == "}")
                    {
                        if (deepth == 0)
                        {
                            break;
                        }
                        deepth--;
                    }
                    tempString += _OriginString[Index];
                    Index++;
                }
            }
            ///�����ָ��
            else if (tempChar == "-")
            {
                t_event.Add(tempString);
                tempString = "";
            }
            ///�¼��ָ���
            else if (tempChar == "+")
            {
                Events.Add(t_event);
                t_event = new();
            }
            ///��һ���ַ�
            Index++;
        }
        t_event.Add(tempString);
        Events.Add(t_event);
        return Events;
    }
    public static List<string> Unpack(string _OriginString)
    {
        string cleanString = _OriginString.Replace("\n", "").Replace("\t", "");
        List<List<string>> Events = new();
        List<string> t_event = new();
        string tempString = "";
        int Index = 0;
        while (Index < cleanString.Length)
        {
            string tempChar = _OriginString[Index].ToString();
            ///�������
            if (tempChar == "$")
            {
                Index++;
                tempString += _OriginString[Index];
            }
            ///������
            else if (tempChar == "{")
            {
                ///���
                int deepth = 0;
                Index++;
                while (true)
                {
                    if (_OriginString[Index].ToString() == "{")
                    {
                        deepth++;
                    }
                    else if (_OriginString[Index].ToString() == "}")
                    {
                        if (deepth == 0)
                        {
                            break;
                        }
                        deepth--;
                    }
                    tempString += _OriginString[Index];
                    Index++;
                }
            }
            ///�����ָ��
            else if (tempChar == "-")
            {
                t_event.Add(tempString);
                tempString = "";
            }
            ///��һ���ַ�
            Index++;
        }
        t_event.Add(tempString);
        return t_event;
    }
}
