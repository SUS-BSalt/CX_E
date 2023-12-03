using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

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

    public void SetLanguage(int LanguageIndex)
    {
        BookReader _LanguagePathReader = new("Book/Maps/LanguageMap.xlsx");
        bookPathLanguageModify = _LanguagePathReader.GetConcept(LanguageIndex, 2);
    }

    private void Start()
    {
        SetLanguage(ConfigManager.Instance.data.Language);
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
            printWindow.StartNewTyping(NameMethod(bookMark)+bookReader.GetConcept(bookMark,4));
        }
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
        
        string nameString;
        try
        {
            nameString = bookReader.GetConcept(_bookMark, 3);
        }
        catch
        {
            return "";
        }

        if (nameString == "")
        {
            return "";
        }
        string output = "";
        string[] names = nameString.Split("+");

        if (names.Count() == 1)
        {
            characterManager.PlayCharacterSpeakAnimation(names[0], isSpeak);
            return "[" + characterManager.GetCharacterColorInRichText(names[0]) + names[0] + "</color>" + "]";
        }
        else
        {
            characterManager.PlayCharacterSpeakAnimation(names[0], isSpeak);
            output = "[" + characterManager.GetCharacterColorInRichText(names[0]) + names[0] + "</color>";
            for (int i = 1; i < names.Count(); i++)
            {
                characterManager.PlayCharacterSpeakAnimation(names[i], isSpeak);
                output = output + "&" + characterManager.GetCharacterColorInRichText(names[i]) + names[i] + "</color>";
            }
            return output + "]";
        }
    }
}
