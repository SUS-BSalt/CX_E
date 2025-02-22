﻿using Newtonsoft.Json;
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

    public IDirector BaseDirector { get; set; }
    public IPerformance currentPerformance { get; set; }

    string IDataUser.PackName => "Dialog";

    bool IDataUser.IndividualizedSave => true;

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
        characterManager.OnSave();
        //print(data.charactersData["南希"].anchorPos);
        newpack.DeserializeData = JsonConvert.SerializeObject(data,Formatting.Indented);
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

    public void PerformanceStart(IDirector Caller)
    {
        BaseDirector = Caller;
        DialogMenu.SetActive(true);
        InputManager.Instance.GrabControl(controller);
        try
        {
            DataManager.Instance.RegisterDataUser(this);
        }
        catch
        {

        }
    }

    public void Load()
    {
        ResetView();
        data = JsonConvert.DeserializeObject<DialogDataClass>(DataManager.Instance.GetDataPack("Dialog").DeserializeData);
        characterManager.OnLoad();
        logWindow.Logs = data.Logs;
        logWindow.RefreshLogMenu();
        ReadManager.SetBook(data.currentBookPath, data.bookMark);
        printWindow.textBox.text = ReadManager.GetLogString(data.bookMark);
    }
    public void Init(string BookPath)
    {
        //print("where are you");
        data = new();
        characterManager.Init();
        ResetView();
        ReadManager.SetBook(BookPath);
    }

    public void PerformanceEnd()
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
            case "Jump":
                {
                    ///Jump-index
                    bookMark = int.Parse(eventArgv[1]);
                    break;
                }
            case "Chapter":
                {
                    ///Chapter
                    bookChapter = int.Parse(eventArgv[1]);
                    ReadManager.bookReader.ChangeBookChapter(bookChapter);
                    bookMark = 2;
                    break;
                }
            case "Book":
                {
                    ReadManager.SetBook(eventArgv[1]);
                    bookMark = 2;
                    break;
                }
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
                    MainDirector.Instance.ExecEvent(eventArgv.Skip(1).ToList());
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

    protected override void Awake()
    {
        base.Awake();
        //print("where are youawake");
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
    public Dictionary<string, DialogcharacterData> charactersData = new();


    public DialogDataClass(int _bookMark = 1, string _currentBookPath = "", int _currentBookChapter = 1)
    {
        this.bookMark = _bookMark;
        currentBookPath = _currentBookPath;
        currentBookChapter = _currentBookChapter;
    }

}