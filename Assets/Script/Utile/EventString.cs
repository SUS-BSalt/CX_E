using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventString
{
    public static List<string> Unpack(string _OriginString)
    {
        List<string> outPut = new();
        string tempString = "";
        int Index = 0;
        while(Index < _OriginString.Length)
        {
            string tempChar =  _OriginString[Index].ToString();
            ///反编译符
            if (tempChar == "$")
            {
                Index++;
                tempString += _OriginString[Index];
            }
            ///参数包
            if (tempChar == "{")
            {
                ///深度
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
            ///参数分割符
            else if (tempChar == "-")
            {
                outPut.Add(tempString);
                tempString = "";
            }
            ///下一个字符
            Index++;
        }
        outPut.Add(tempString);
        return outPut;
    }
}
