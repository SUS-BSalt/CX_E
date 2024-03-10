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
            ///�������
            if (tempChar == "$")
            {
                Index++;
                tempString += _OriginString[Index];
            }
            ///������
            if (tempChar == "{")
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
                outPut.Add(tempString);
                tempString = "";
            }
            ///��һ���ַ�
            Index++;
        }
        outPut.Add(tempString);
        return outPut;
    }
}
