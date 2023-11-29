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
    public Dictionary<string, DialogCharacter> characterDict;//用角色名字访问，是为了方便作者写文
    public void RegisterCharacter(string ID)
    {
        int index = FindIDIndex(ID);//找到ID对应在表中的指数
        if (characterDict.ContainsKey(CharacterData.GetConcept(index, 2)))
        {
            return;//当字典里有该角色时，不进行接下来的步骤
        }
        GameObject prefab = AssetDatabase.LoadAssetAtPath(CharacterData.GetConcept(index, 4), typeof(GameObject)) as GameObject;//得到预制件
        GameObject _Character = Instantiate(prefab);//实例化预制件
        _Character.transform.SetParent(transform);//设置为子物体
        DialogCharacter newCharacter = new DialogCharacter(ID, CharacterData.GetConcept(index, 2), CharacterData.GetConcept(index, 3), _Character);//创建character类
        newCharacter.rect.localScale = new Vector3(40, 40, 40);//调整新角色的缩放
        SetCharacterPos(newCharacter, new Vector2(0.1f, 0.416f));
        characterDict.Add(CharacterData.GetConcept(index, 2), newCharacter);//注册到字典中，用名字而不是ID，为了方便访问
        characterDict[newCharacter.name].body.SetActive(false);//让新角色从屏幕上消失
                                                               //characterDict[newCharacter.name].body.SetActive(true);//让新角色从屏幕上出现
                                                               //newCharacter.body.SetActive(false);
    }

    public void CharacterAppear(string _characterName)
    {
        characterDict[_characterName].body.SetActive(true);

    }
    public void CharacterDisappear(string _characterName)
    {
        characterDict[_characterName].body.SetActive(false);
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
        _character.rect.anchorMax = anchorPos;
        _character.rect.anchorMin = anchorPos;
        _character.rect.offsetMax = new Vector2(0, 0);
        _character.rect.offsetMin = new Vector2(0, 0);
    }
    public void SetCharacterPos(string _characterName, Vector2 anchorPos)
    {
        try
        {
            characterDict[_characterName].rect.anchorMax = anchorPos;
            characterDict[_characterName].rect.anchorMin = anchorPos;
            characterDict[_characterName].rect.offsetMax = new Vector2(0, 0);
            characterDict[_characterName].rect.offsetMin = new Vector2(0, 0);
        }
        catch
        {

        }
    }
    public void SetCharacterFace(string _characterName, string face)
    {
        characterDict[_characterName].currentFace = face;
    }
    public void PlayCharacterAnimation(string characterName, string AnimationName)
    {
        try
        {
            characterDict[characterName].animator.Play(AnimationName);
            //Debug.Log(AnimationName);
        }
        catch
        {
            //Debug.Log("Not Find " + characterName + "!");
        }
    }//直接指定播放的动画
    public void PlayCharacterSpeakAnimation(string characterName, bool isSpeak)//根据角色当前表情，调用表情对应的说话动画,或者恢复原状
    {
        try
        {
            if (isSpeak)
            {
                PlayCharacterAnimation(characterName, characterDict[characterName].currentFace + "Speak");
                //Debug.Log("张嘴了");
            }
            else
            {
                PlayCharacterAnimation(characterName, characterDict[characterName].currentFace);
                //Debug.Log("闭嘴了");
            }
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
        characterDict = new Dictionary<string, DialogCharacter>();
        //CharacterData = new BookReader();
    }
}



public class DialogCharacter
{
    public string ID;
    public string name;
    public string color;
    public GameObject body;
    public Animator animator;
    public RectTransform rect;
    public string currentFace = "Normal";
    public DialogCharacter(string _ID, string _name, string _color, GameObject _body)
    {
        ID = _ID;
        name = _name;
        color = _color;
        body = _body;
        animator = body.GetComponent<Animator>();
        rect = body.GetComponent<RectTransform>();
    }
}
