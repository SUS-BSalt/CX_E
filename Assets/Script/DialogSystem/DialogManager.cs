using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : Singleton<DialogManager>,IPerformance,IDirector,IDataUser
{
    public DialogDataClass data;

    public string bookPath { get { return data.currentBookPath; } set { data.currentBookPath = value; } }
    public int bookMark { get { return data.bookMark; } set { data.bookMark = value; } }
    public int bookChapter { get { return data.currentBookChapter; } set { data.currentBookChapter = value;print("sb set chapter"); } }

    public IDirector BaseDirector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public IPerformance currentPerformance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    string IDataUser.PackName => throw new NotImplementedException();

    bool IDataUser.IndividualizedSave => throw new NotImplementedException();

    IDirector IPerformance.BaseDirector { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public DialogController controller;
    //public
    public DialogLogWindowManager logWindow;
    public DialogPrintingManager printWindow;
    public DialogCharacterManager characterManager;
    public DialogReadManager ReadManager;
    public GameObject DialogMenu;

    DataPack IDataUser.SerializedToDataPack()
    {
        DataPack newpack = new();
        newpack.PackName = "Dialog";
        data.Logs = logWindow.Logs;
        newpack.DeserializeData = JsonConvert.SerializeObject(data);
        return newpack;
    }

    T IDataUser.GetData<T>(params string[] argv)
    {
        throw new NotImplementedException();
    }

    bool IDataUser.SetData<T>(T value, params string[] argv)
    {
        throw new NotImplementedException();
    }

    void IPerformance.PerformanceStart()
    {
        DialogMenu.SetActive(true);
    }

    public void Load()
    {
        ResetView();
        data = JsonConvert.DeserializeObject<DialogDataClass>(DataManager.Instance.GetDataPack("Dialog").DeserializeData);
        logWindow.Logs = data.Logs;
    }
    public void NewSet()
    {

    }

    void IPerformance.PerformanceEnd()
    {
        DialogMenu.SetActive(false);
    }


    public void ResetView()
    {
        characterManager.ResetView();
        logWindow.CleanLogMenu();
    }
    public void ExecEvent(string _EventString)
    {
        Debug.Log(_EventString);
        List<string> eventArgv = EventString.Unpack(_EventString);
        ExecEvent(eventArgv);
    }
    public void ExecEvent(List<string> eventArgv)
    {
        //Debug.Log(eventArgv[0]);
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
            case "GT":
                {
                    MainDirector.Instance.ExecEvent(eventArgv);
                    //print("t");
                    break;
                }

        }
    }

    public void ExecPreEvent(string preEventString)
    {
        if(preEventString == DataManager_Old.NODATA)
        {
            return;
        }
        //print(preEventString);
        List<List<string>> Events = EventString.UnpackComplex(preEventString);
        //print(Events.Count);
        foreach (List<string> Eventpach in Events)
        {
            ExecEvent(Eventpach);
        }
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
            ExecPreEvent(ReadManager.bookReader.GetConcept(bookMark, 1));
            StartTypingWord();
            logWindow.Logs.Add(ReadManager.GetLogString(bookMark));
            logWindow.RefreshLogMenu();
        }
    }

    /// <summary>
    /// 开始打印文字
    /// </summary>
    public void StartTypingWord()
    {
        printWindow.StartNewTyping(NameMethod(bookMark) + ReadManager.bookReader.GetConcept(bookMark, 4));
        //printWindow.StartNewTyping();
    }
    public void StopTypingWord()
    {
        characterManager.StopAllCharactersSpeak();
        printWindow.StopCurrentTyping();
    }


    public string NameMethod(int _bookMark)//处理正文的名字一格
    {
        //TODO isSpeak是临时变量
        bool isSpeak = true;
        
        string[] names = ReadManager.SpiltNameNode(_bookMark);

        foreach(string name in names)
        {
            characterManager.PlayCharacterSpeakAnimation(name, isSpeak);
        }
        return ReadManager.CombineSpiltedNameArry(names);
    }
 



    public void NextStep()
    {
        OnClick();
        OnClick();
    }


}
[Serializable]
public class DialogDataClass
{
    public int bookMark;
    public string currentBookPath;
    public int currentBookChapter;
    public List<string> Logs = new(500);


    public DialogDataClass(int _bookMark = 1, string _currentBookPath = "", int _currentBookChapter = 1)
    {
        this.bookMark = _bookMark;
        currentBookPath = _currentBookPath;
        currentBookChapter = _currentBookChapter;
    }

}