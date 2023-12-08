using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPrintingManager : MonoBehaviour
{
    public DialogManager dialogManager;

    public bool isTyping;

    public string TargetText;//需要输出到屏幕上的内容（目标文本）
    public string CurrentText;//当前需要输出到屏幕上的文本
    public int currentWordIndex;//当前正在处理的字符的编号
    public Text textBox;//显示文字的地方

    public float typingGapSec;//每个字符出现间隔时间


    #region RichText相关
    public List<string> RichTextModify;//当前正显示在屏幕上的文本，为了完成RichText效果，而需要补充在当前显示文本尾部的格式文本
    public void RichTextModifyMethod(string RichTextGrammar)//处理RichText的方法，如果这里出bug，优先看是不是Substring那里的字符长度对不上，
    {
        #region 从RichTextModify里删除
        if (RichTextGrammar.Substring(1, 1) == "/")
        {
            try
            {
                if (RichTextGrammar.Substring(2, 5) == "color")
                {
                    RichTextModify.Remove("</color>");
                }
                else if (RichTextGrammar.Substring(2, 1) == "i")
                {
                    RichTextModify.Remove("</i>");
                }
                else if (RichTextGrammar.Substring(2, 1) == "b")
                {
                    RichTextModify.Remove("</b>");
                }
                else if (RichTextGrammar.Substring(2, 6) == "cspace")
                {
                    RichTextModify.Remove("</cspace>");
                }
                else if (RichTextGrammar.Substring(2, 4) == "size")
                {
                    RichTextModify.Remove("</size>");
                }
            }
            catch
            {

            }
        }
        #endregion
        #region 往RichTextModify里添加
        else//这里的排序需要以richtext语法的长短，从短到长排序，不然会发生访问越界的错误
        {
            try
            {
                if (RichTextGrammar.Substring(1, 1) == "i")
                {
                    RichTextModify.Add("</i>");
                }
                else if (RichTextGrammar.Substring(1, 1) == "b")
                {
                    RichTextModify.Add("</b>");
                }
                else if (RichTextGrammar.Substring(1, 4) == "size")
                {
                    RichTextModify.Add("</size>");
                }
                else if (RichTextGrammar.Substring(1, 5) == "color")
                {
                    Debug.Log("go it");
                    RichTextModify.Add("</color>");
                }
                else if (RichTextGrammar.Substring(1, 6) == "cspace")
                {
                    RichTextModify.Add("</cspace>");
                }
            }
            catch
            {

            };
        }
        #endregion
    }
    public string CombineTextAndRichTextModify(string text)
    {
        foreach (string modify in RichTextModify)
        {
            text += modify;
        }
        return text;
    }//合并RichText标签到给定文本，返回合并结果
    #endregion
    
    public void ReadNextWord()
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
        else if (TargetText[currentWordIndex].ToString() == "<")
        {
            string RichTextGrammar = "";//开始获取RichText语法
            while (TargetText[currentWordIndex].ToString() != ">")
            {
                RichTextGrammar += TargetText[currentWordIndex].ToString();
                currentWordIndex++;
            }
            RichTextGrammar += ">";//到这里拿到RichText语法
            CurrentText += RichTextGrammar;//把RichText语法加到当前文字里
            RichTextModifyMethod(RichTextGrammar);//richtext修正方法
            currentWordIndex++;//指向下一个字符
        }//当遇到<尖括号时，意味着有RichText语法需要处理，这里处理RichText
        else if (TargetText[currentWordIndex].ToString() == "{")
        {
            string EventString = "";//开始获取Event字符
            currentWordIndex++;//跳过{
            while (TargetText[currentWordIndex].ToString() != "}")
            {
                EventString += TargetText[currentWordIndex].ToString();
                currentWordIndex++;
            }//到这里拿到Event字符
            dialogManager.ExecEvent(EventString);//将事件字符丢给dialogManager；
            currentWordIndex++;//指向下一个字符
        }//当遇到{花括号时，意味着有自定义的方法需要执行，根据传入的ieExec决定是否执行
        else
        {
            CurrentText += TargetText[currentWordIndex].ToString();
            currentWordIndex++;//指向下一个字符
        }//不是特殊符号，正常输出
    }//读取下一个字符，并更新CurrentWordIndex与CurrentText

    public void UpdateText(string text)
    {
        textBox.text = text;
    }

    IEnumerator TypingCoroutine()//逐字输出协程
    {
        while (currentWordIndex < TargetText.Length)
        {
            ReadNextWord();//更新CurrentText
            UpdateText(CombineTextAndRichTextModify(CurrentText));//更新文字到屏幕
            yield return new WaitForSeconds(typingGapSec);
        }
        //TODO 这里可能需要重置部分参数
        isTyping = false;

        dialogManager.StopTypingWord();
    }

    public void StartNewTyping(string _TargetText)
    {
        ResetPrintWindow();
        TargetText = _TargetText;
        StartCoroutine(TypingCoroutine());
        isTyping = true;
    }

    public void StopCurrentTyping()
    {
        StopCoroutine(TypingCoroutine());
        isTyping = false;

        while (currentWordIndex < TargetText.Length)
        {
            ReadNextWord();//更新CurrentText
        }//将剩下的文字读进来
        UpdateText(CombineTextAndRichTextModify(CurrentText));//更新文字到屏幕
    }


    public void ResetPrintWindow()
    {
        textBox.text = "";
        TargetText = "";
        CurrentText = "";
        currentWordIndex = 0;
    }
}
