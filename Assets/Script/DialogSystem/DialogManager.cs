using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public BookReader bookReader;
    public int bookMark;

    public DialogController controller;
    //public
    public DialogLogWindowManager logWindow;
    public DialogPrintingManager printWindow;
    public DialogCharacterManager characterManager;

    public void ExecEvent(string EventString)
    {

    }
    private void Awake()
    {

    }
    public void SetBook(string BookPath)
    {
        bookReader = new BookReader(BookPath);
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
            //printWindow.StartNewTyping();
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
            nameString = bookReader.GetConcept(_bookMark, 1);
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
