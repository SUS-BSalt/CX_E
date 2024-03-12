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
        List<List<string>> Events = new();
        List<string> t_event = new();
        if (_OriginString == null)
        {
            return Events;
        }

        Debug.Log(_OriginString);
        string cleanString = _OriginString.Replace("\n", "").Replace("\t", "");
        Debug.Log(cleanString);

        string tempString = "";
        int Index = 0;
        while(Index < cleanString.Length)
        {
            string tempChar = cleanString[Index].ToString();
            ///�������
            if (tempChar == "$")
            {
                Index++;
                tempString += cleanString[Index];
            }
            ///������
            else if (tempChar == "{")
            {
                ///���
                int deepth = 0;
                Index++;
                while (true)
                {
                    if(cleanString[Index].ToString() == "{")
                    {
                        deepth++;
                    }
                    else if(cleanString[Index].ToString() == "}")
                    {
                        if (deepth == 0)
                        {
                            break;
                        }
                        deepth--;
                    }
                    tempString += cleanString[Index];
                    Index++;
                }
            }
            ///�����ָ��
            else if (tempChar == "-")
            {
                t_event.Add(tempString);
                Debug.Log(tempString);
                tempString = "";
            }
            ///�¼��ָ���
            else if (tempChar == "+")
            {
                t_event.Add(tempString);
                Events.Add(t_event);
                t_event = new();
                tempString = "";
            }
            else
            {
                tempString += cleanString[Index];
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
        List<string> t_event = new();
        if (_OriginString == null)
        {
            return t_event;
        }
        Debug.Log(_OriginString);
        string cleanString = _OriginString.Replace("\n", "").Replace("\t", "");
        Debug.Log(cleanString);
        string tempString = "";
        int Index = 0;
        while (Index < cleanString.Length)
        {
            string tempChar = cleanString[Index].ToString();
            ///�������
            if (tempChar == "$")
            {
                Index++;
                tempString += cleanString[Index];
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
            else
            {
                tempString += cleanString[Index];
            }
            ///��һ���ַ�
            Index++;
        }
        t_event.Add(tempString);
        return t_event;
    }
}
