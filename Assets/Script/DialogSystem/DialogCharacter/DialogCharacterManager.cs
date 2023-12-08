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

    public List<DialogCharacter> characterList;
    public Dictionary<string, DialogCharacter> characterDict;//用角色名字访问，是为了方便作者写文



    public void CharacterAppear(string _characterName)
    {
        characterDict[_characterName].gameObject.SetActive(true);

    }
    public void CharacterDisappear(string _characterName)
    {
        characterDict[_characterName].gameObject.SetActive(false);
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
    public void SetCharacterPos(string _characterName, Vector2 anchorPos)
    {
        try
        {

        }
        catch
        {

        }
    }
    public void SetCharacterFace(string _characterName, string face)
    {

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
    private void Start()
    {
        characterDict = new Dictionary<string, DialogCharacter>();
        foreach (DialogCharacter character in characterList)
        {
            characterDict.Add(DataManager.Instance.CharacterName[character.characterIndex], character);
        }
        //CharacterData = new BookReader();
    }
}
