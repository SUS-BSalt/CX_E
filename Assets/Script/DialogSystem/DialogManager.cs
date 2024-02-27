using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>
{
    public DialogDataClass data;
    public string bookPathLanguageModify;
    public string bookPath { get { return data.currentBookPath; } set { data.currentBookPath = value; } }
    public int bookMark { get { return data.bookMark; } set { data.bookMark = value; } }
    public int bookChapter { get { return data.currentBookChapter; } set { data.currentBookChapter = value;print("sb set chapter"); } }

    public BookReader bookReader;
    public DialogController controller;
    //public
    public DialogLogWindowManager logWindow;
    public DialogPrintingManager printWindow;
    public DialogCharacterManager characterManager;

    [Serializable]
    public class PlotTrigger : UnityEvent<string[]> { }
    public PlotTrigger plotTrigger;

    public void OnLoad()
    {
        SetLanguage();
        data = SaveManager.Instance.LoadData<DialogDataClass>("Dialog");
        SetBook(bookPath,bookMark);
        bookReader.ChangeBookChapter(data.currentBookChapter);
        bookMark -= 1;
        characterManager.OnLoad();
        OnClick();
        print("loadDialog");
    }
    public void OnSave()
    {
        SaveManager.Instance.SaveData<DialogDataClass>("Dialog",data);
        characterManager.OnSave();
        print("saveDialog");
    }

    public void ExecEvent(string EventString)
    {
        //Debug.Log(eventString);
        string[] eventArgv = EventString.Split("-");
        switch (eventArgv[0])
        {
            case "SetPos":
                {
                    characterManager.SetCharacterPos(eventArgv[1], new Vector2(float.Parse(eventArgv[2]), float.Parse(eventArgv[3])));
                    break;
                }
            case "SetFace":
                {
                    characterManager.SetCharacterFace(eventArgv[1], eventArgv[2]);
                    break;
                }
            case "Appear":
                {
                    characterManager.CharacterAppear(eventArgv[1]);
                    break;
                }
            case "Disappear":
                {
                    characterManager.CharacterDisappear(eventArgv[1]);
                    break;
                }
            case "Audio":
                {
                    
                    break;
                }
            case "Music":
                {

                    break;
                }
            case "Trigger":
                {
                    plotTrigger?.Invoke(eventArgv);
                    break;
                }
            case "Jump":
                {
                    bookMark = int.Parse(eventArgv[1]);
                    break;
                }
            case "Chapter":
                {
                    bookChapter = int.Parse(eventArgv[1]);
                    bookReader.ChangeBookChapter(bookChapter);
                    bookMark = 2;
                    break;
                }
            case "Book":
                {
                    SetBook(eventArgv[1]);
                    bookMark = 2;
                    break;
                }
        }
    }

    public void ExecPreEvent(string preEventString)
    {
        //print(preEventString);
        if(preEventString == DataManager_Old.NODATA)
        {
            return;
        }
        string[] events = preEventString.Split("+");
        foreach (string _event in events)
        {
            ExecEvent(_event);
        }
    }
    public void SetLanguage()
    {
        bookPathLanguageModify = "Book/CN/";
        //print("setLan");
    }

    private void OnEnable()
    {
        //print("Lan？");
        SetLanguage();
        //print(bookPathLanguageModify);
        //SetBook(DataManager_Old.Instance.playerSaveData.bookPath, DataManager_Old.Instance.playerSaveData.bookMark);
        
    }
    public void SetBook(string BookPath, int _bookMark = 1)
    {
        //print(Path.Combine(bookPathLanguageModify, BookPath));
        //bookReader = new BookReader(bookPathLanguageModify + BookPath);
        string path = Path.Combine(bookPathLanguageModify, BookPath);
        //print(bookPathLanguageModify);
        //print(path);
        bookPath = BookPath;
        bookReader = new BookReader(path);

        bookMark = _bookMark;
    }

    public void OnClick()
    {
        if (printWindow.isTyping)
        {
            StopTypingWord();
        }
        else
        {
            bookMark += 1;
            ExecPreEvent(bookReader.GetConcept(bookMark, 1));
            StartTypingWord();
            logWindow.RefreshLogMenu(bookMark);
        }
    }
    public string GetLogString(int _bookMark)
    {
        string nameString = CombineSpiltedNameArry(SpiltNameNode(_bookMark));
        string textString = GetCleanMainText(_bookMark);
        return nameString + textString;
    }//干净
    /// <summary>
    /// 开始打印文字
    /// </summary>
    public void StartTypingWord()
    {
        printWindow.StartNewTyping(NameMethod(bookMark) + bookReader.GetConcept(bookMark, 4));
        //printWindow.StartNewTyping();
    }
    public void StopTypingWord()
    {
        characterManager.StopAllCharactersSpeak();
        printWindow.StopCurrentTyping();
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

        if (nameString == "NoData")
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
[Serializable]
public class DialogDataClass
{
    public int bookMark;
    public string currentBookPath;
    public int currentBookChapter;


    public DialogDataClass(int _bookMark = 1, string _currentBookPath = "", int _currentBookChapter = 1)
    {
        this.bookMark = _bookMark;
        currentBookPath = _currentBookPath;
        currentBookChapter = _currentBookChapter;
    }

}