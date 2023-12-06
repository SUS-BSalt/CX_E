using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Rendering.CameraUI;

public class DialogManager : MonoBehaviour
{
    public string bookPathLanguageModify;
    public BookReader bookReader;
    public int bookMark = 1;

    public DialogController controller;
    //public
    public DialogLogWindowManager logWindow;
    public DialogPrintingManager printWindow;
    public DialogCharacterManager characterManager;

    public void ExecEvent(string EventString)
    {

    }

    public void SetLanguage()
    {
        bookPathLanguageModify = DataManager.Instance.LanguageData[DataManager.Instance.ConfigData["Language"]]["Path"];
    }

    private void Start()
    {
        SetLanguage();
        SetBook("conversation/TestBook.xlsx");
    }
    public void SetBook(string BookPath)
    {
        //print(Path.Combine(bookPathLanguageModify, BookPath));
        //bookReader = new BookReader(bookPathLanguageModify + BookPath);
        bookReader = new BookReader(Path.Combine(bookPathLanguageModify, BookPath));
    }

    public void OnClick()
    {
        if (printWindow.isTyping)
        {
            printWindow.StopCurrentTyping();
        }
        else
        {
            bookMark += 1;
            printWindow.StartNewTyping(NameMethod(bookMark) + bookReader.GetConcept(bookMark, 4));
            logWindow.RefreshLogMenu(bookMark);
        }
    }
    public string GetLogString(int _bookMark)
    {
        string nameString = CombineSpiltedNameArry(SpiltNameNode(_bookMark));
        string textString = GetCleanMainText(_bookMark);
        return nameString + textString;
    }
    public void StartTypingWord()
    {
        //printWindow.StartNewTyping();
    }
    public void StopTypingWord()
    {

    }


    public string ReadSentence(int _bookMark, bool isLogView)
    {
        if (isLogView)
        {
            
        }
        //bookReader.GetConcept(_bookMark);
        return "";
    }
    public string NameMethod(int _bookMark)//处理正文的名字一格
    {
        //TODO isSpeak是临时变量
        bool isSpeak = true;
        
        string[] names = SpiltNameNode(_bookMark);

        foreach(string name in names)
        {
            characterManager.PlayCharacterSpeakAnimation(name, isSpeak);
        }
        return CombineSpiltedNameArry(names);
    }
    public string[] SpiltNameNode(int _bookMark)
    {
        string nameString;
        try
        {
            nameString = bookReader.GetConcept(_bookMark, 3);
        }
        catch
        {
            return new string[0];
        }

        if (nameString == "")
        {
            return new string[0];
        }
        return nameString.Split("+");
    }//将正文名字格拆开
    public string CombineSpiltedNameArry(string[] names)
    {
        string output;
        if (names.Count() == 0)
        {
            return "";
        }
        if (names.Count() == 1)
        {
            return "[" + characterManager.GetCharacterColorInRichText(names[0]) + names[0] + "</color>" + "]";
        }
        else
        {
            output = "[" + characterManager.GetCharacterColorInRichText(names[0]) + names[0] + "</color>";
            for (int i = 1; i < names.Count(); i++)
            {
                output = output + "&" + characterManager.GetCharacterColorInRichText(names[i]) + names[i] + "</color>";
            }
            return output + "]";
        }
    }//将拆开后的名字格合起来

    public string GetCleanMainText(int _bookMark)
    {
        int currentWordIndex = 0;
        string TargetText = bookReader.GetConcept(_bookMark, 4);
        string CurrentText = "";

        while (currentWordIndex < TargetText.Length)
        {
            if (TargetText[currentWordIndex].ToString() == "$")
            {
                currentWordIndex++;
                CurrentText += TargetText[currentWordIndex].ToString();
                currentWordIndex++;//指向下一个字符
            }//当遇到$字符时，直接把下一个字符输出到屏幕上，不做特殊处理
            else if (TargetText[currentWordIndex].ToString() == "[")
            {
                do
                {
                    CurrentText += TargetText[currentWordIndex].ToString();
                    currentWordIndex++;
                }
                while (TargetText[currentWordIndex].ToString() != "]");//
                CurrentText += TargetText[currentWordIndex].ToString();
                currentWordIndex++;//指向下一个字符
            }//当遇到[方括号时，直接把括号内，包括括号本身输出，不对括号内的特殊符号做处理
            else if (TargetText[currentWordIndex].ToString() == "{")
            {
                currentWordIndex++;//跳过{
                while (TargetText[currentWordIndex].ToString() != "}")
                {
                    currentWordIndex++;
                }//跳过所有{}
                currentWordIndex++;//指向下一个字符
            }//当遇到{花括号时，意味着有自定义的方法需要执行，全部跳过
            else
            {
                CurrentText += TargetText[currentWordIndex].ToString();
                currentWordIndex++;//指向下一个字符
            }//不是特殊符号，正常输出
        }
        return CurrentText;
    }
}
