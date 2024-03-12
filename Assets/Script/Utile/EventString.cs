using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventString
{
    /// <summary>
    /// EventString的层级为"Event""Method"
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
            ///反编译符
            if (tempChar == "$")
            {
                Index++;
                tempString += cleanString[Index];
            }
            ///参数包
            else if (tempChar == "{")
            {
                ///深度
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
            ///参数分割符
            else if (tempChar == "-")
            {
                t_event.Add(tempString);
                Debug.Log(tempString);
                tempString = "";
            }
            ///事件分隔符
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
            ///下一个字符
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
            ///反编译符
            if (tempChar == "$")
            {
                Index++;
                tempString += cleanString[Index];
            }
            ///参数包
            else if (tempChar == "{")
            {
                ///深度
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
            ///参数分割符
            else if (tempChar == "-")
            {
                t_event.Add(tempString);
                tempString = "";
            }
            else
            {
                tempString += cleanString[Index];
            }
            ///下一个字符
            Index++;
        }
        t_event.Add(tempString);
        return t_event;
    }
}
