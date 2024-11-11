using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DialogCharacterManager : MonoBehaviour
{
    public BookReader CharacterData;

    public List<DialogCharacter> characterList;//它的目的仅限于方便的批量导入角色信息到下面的字典里

    public Dictionary<string, DialogCharacter> characterDict;//用角色名字访问，是为了方便作者写文

    public void ResetView()
    {
        foreach (string characters in characterDict.Keys)
        {
            if (characterDict[characters].isOnStage)
            {
                characterDict[characters].OnDisappear();
            }
        }
    }
    public void OnSave()
    {
        foreach (string characters in characterDict.Keys)
        {
            characterDict[characters].data.OnSave();
            if (DialogManager.Instance.data.charactersData.ContainsKey(characters))
            {
                DialogManager.Instance.data.charactersData[characters] = characterDict[characters].data;
            }
            else
            {
                DialogManager.Instance.data.charactersData.Add(characters, characterDict[characters].data);
            }
        }

    }
    public void OnLoad()
    {
        Init();
        foreach (string characters in characterDict.Keys)
        {
            characterDict[characters].data = DialogManager.Instance.data.charactersData[characters];
            characterDict[characters].data.OnLoad();
            if (characterDict[characters].isOnStage)
            {
                characterDict[characters].OnAppear();
            }
        }
    }
    public void SetCharacter(string _characterName, string face, Vector2 anchorPos)
    {
        SetCharacterFace(_characterName, face);
        SetCharacterPos(_characterName, anchorPos);
    }

    public void CharacterAppear(string _characterName)
    {
        characterDict[_characterName].OnAppear();

    }
    public void CharacterDisappear(string _characterName)
    {
        characterDict[_characterName].OnDisappear();
    }
    public string GetCharacterColorInRichText(string _characterName)
    {
        try
        {
            return "<color=$>".Replace("$", characterDict[_characterName].color);
        }
        catch
        {
            return "<color=#FFFFFF>";
        }
    }
    public void SetCharacterPos(DialogCharacter _character, Vector2 anchorPos)
    {

    }
    /// <summary>
    /// 设置人物在画面窗口的百分比处位置
    /// </summary>
    /// <param name="_characterName">角色名称</param>
    /// <param name="anchorPos">百分比位置</param>
    public void SetCharacterPos(string _characterName, Vector2 anchorPos)
    {
        try
        {
            characterDict[_characterName].ChangePos(anchorPos);
        }
        catch
        {

        }
    }
    public void SetCharacterFace(string _characterName, string face)
    {
        try
        {
            characterDict[_characterName].ChangeFace(face);

        }
        catch
        {

        }
    }
    public void PlayCharacterSpeakAnimation(string characterName, bool isSpeak)//根据角色当前表情，调用表情对应的说话动画,或者恢复原状
    {
        try
        {
                characterDict[characterName].SpeakController(isSpeak);
                //Debug.Log("张嘴了");
        }
        catch
        {
            //Debug.Log("Not Find " + characterName + "!");
        }
    }
    public void StopAllCharactersSpeak()
    {
        foreach (KeyValuePair<string, DialogCharacter> _character in characterDict)
        {
            PlayCharacterSpeakAnimation(_character.Key, false);
        }
    }
    public int FindIDIndex(string ID)
    {
        int index = 1;
        while (true)
        {
            if (CharacterData.GetConcept(index,1) == "")
            {
                return 2;//如果为空，返回工具人
            }
            else if (ID == CharacterData.GetConcept(index, 1))
            {
                return index;
            }
            //Debug.Log(bookChapter.Cells[index, 1].Value.ToString());
            index++;
        }
    }
    public int FindNameIndex(string characterName)
    {
        int index = 1;
        while (true)
        {
            if (CharacterData.GetConcept(index, 2) == "")
            {
                return 2;//如果为空，返回工具人
            }
            else if (characterName == CharacterData.GetConcept(index, 2))
            {
                return index;
            }
            //Debug.Log(bookChapter.Cells[index, 1].Value.ToString());
            index++;
        }
    }
    private void Awake()
    {
        Init();

        //CharacterData = new BookReader();
    }

    public void Init()
    {
        characterDict = new Dictionary<string, DialogCharacter>();
        foreach (DialogCharacter character in characterList)
        {
            characterDict.Add(DataManager.Instance.GetData<string>("Profile","CharacterName",character.characterIndex, DataManager.Instance.GetData<int>("Profile","LocalOption","2","2").ToString()), character);
            character.Init();
            //characterDict添加数据，key值为角色名字，坐标是角色的编号以及语言的编号
        }
    }
}


