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
            printWindow.StartNewTyping();
        }
    }
    public string ReadSentence(int _bookMark, bool isLogView)
    {
        if (isLogView)
        {
            
        }
        //bookReader.GetConcept(_bookMark);
    }
    public string NameMethod(int _bookMark)//处理正文的名字一格
    {
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
            characterController.PlayCharacterSpeakAnimation(names[0], isSpeak);
            return "[" + characterController.GetCharacterColorInRichText(names[0]) + names[0] + "</color>" + "]";
        }
        else
        {
            characterController.PlayCharacterSpeakAnimation(names[0], isSpeak);
            output = "[" + characterController.GetCharacterColorInRichText(names[0]) + names[0] + "</color>";
            for (int i = 1; i < names.Count(); i++)
            {
                characterController.PlayCharacterSpeakAnimation(names[i], isSpeak);
                output = output + "&" + characterController.GetCharacterColorInRichText(names[i]) + names[i] + "</color>";
            }
            return output + "]";
        }
    }
}
