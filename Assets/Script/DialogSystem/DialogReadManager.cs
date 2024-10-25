using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DialogReadManager : MonoBehaviour
{
    public BookReader bookReader;
    public string bookPathLanguageModify;

    public string bookPath { get { return DialogManager.Instance.bookPath; } set { DialogManager.Instance.bookPath = value; } }
    public int bookMark { get { return DialogManager.Instance.bookMark; } set { DialogManager.Instance.bookMark = value; } }
    public int bookChapter { get { return DialogManager.Instance.bookChapter; } set { DialogManager.Instance.bookChapter = value;} }

    public void SetLanguage()
    {
        bookPathLanguageModify = "Book/CN/";
        //print("setLan");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        //print("Lan��");
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

    public string GetLogString(int _bookMark)
    {
        string nameString = CombineSpiltedNameArry(SpiltNameNode(_bookMark));
        string textString = GetCleanMainText(_bookMark);
        return nameString + textString;
    }//�������Log���ڵ��ַ�

    public string CombineSpiltedNameArry(string[] names)
    {
        string output;
        if (names.Count() == 0)
        {
            return "";
        }
        if (names.Count() == 1)
        {
            return "[" + DialogManager.Instance.characterManager.GetCharacterColorInRichText(names[0]) + names[0] + "</color>" + "]";//DataManager.Instance.GetData<string>("Profile","Character","") 
        }
        else
        {
            output = "[" + DialogManager.Instance.characterManager.GetCharacterColorInRichText(names[0]) + names[0] + "</color>";
            for (int i = 1; i < names.Count(); i++)
            {
                output = output + "&" + DialogManager.Instance.characterManager.GetCharacterColorInRichText(names[i]) + names[i] + "</color>";
            }
            return output + "]";
        }
    }//���𿪺�����ָ������

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
    }//���������ָ��

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
                currentWordIndex++;//ָ����һ���ַ�
            }//������$�ַ�ʱ��ֱ�Ӱ���һ���ַ��������Ļ�ϣ��������⴦��
            else if (TargetText[currentWordIndex].ToString() == "[")
            {
                do
                {
                    CurrentText += TargetText[currentWordIndex].ToString();
                    currentWordIndex++;
                }
                while (TargetText[currentWordIndex].ToString() != "]");//
                CurrentText += TargetText[currentWordIndex].ToString();
                currentWordIndex++;//ָ����һ���ַ�
            }//������[������ʱ��ֱ�Ӱ������ڣ��������ű�����������������ڵ��������������
            else if (TargetText[currentWordIndex].ToString() == "{")
            {
                currentWordIndex++;//����{
                while (TargetText[currentWordIndex].ToString() != "}")
                {
                    currentWordIndex++;
                }//��������{}
                currentWordIndex++;//ָ����һ���ַ�
            }//������{������ʱ����ζ�����Զ���ķ�����Ҫִ�У�ȫ������
            else
            {
                CurrentText += TargetText[currentWordIndex].ToString();
                currentWordIndex++;//ָ����һ���ַ�
            }//����������ţ��������
        }
        return CurrentText;
    }

}
