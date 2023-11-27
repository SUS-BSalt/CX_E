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
    public Dictionary<string, DialogCharacter> characterDict;//�ý�ɫ���ַ��ʣ���Ϊ�˷�������д��
    public void RegisterCharacter(string ID)
    {
        int index = FindIDIndex(ID);//�ҵ�ID��Ӧ�ڱ��е�ָ��
        if (characterDict.ContainsKey(bookChapter.Cells[index, 2].Value.ToString()))
        {
            return;//���ֵ����иý�ɫʱ�������н������Ĳ���
        }
        GameObject prefab = AssetDatabase.LoadAssetAtPath(bookChapter.Cells[index, 4].Value.ToString(), typeof(GameObject)) as GameObject;//�õ�Ԥ�Ƽ�
        GameObject _Character = Instantiate(prefab);//ʵ����Ԥ�Ƽ�
        _Character.transform.SetParent(transform);//����Ϊ������
        DialogCharacter newCharacter = new DialogCharacter(ID, bookChapter.Cells[index, 2].Value.ToString(), bookChapter.Cells[index, 3].Value.ToString(), _Character);//����character��
        newCharacter.rect.localScale = new Vector3(40, 40, 40);//�����½�ɫ������
        SetCharacterPos(newCharacter, new Vector2(0.1f, 0.416f));
        characterDict.Add(bookChapter.Cells[index, 2].Value.ToString(), newCharacter);//ע�ᵽ�ֵ��У������ֶ�����ID��Ϊ�˷������
        characterDict[newCharacter.name].body.SetActive(false);//���½�ɫ����Ļ����ʧ
                                                               //characterDict[newCharacter.name].body.SetActive(true);//���½�ɫ����Ļ�ϳ���
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
    }//ֱ��ָ�����ŵĶ���
    public void PlayCharacterSpeakAnimation(string characterName, bool isSpeak)//���ݽ�ɫ��ǰ���飬���ñ����Ӧ��˵������,���߻ָ�ԭ״
    {
        try
        {
            if (isSpeak)
            {
                PlayCharacterAnimation(characterName, characterDict[characterName].currentFace + "Speak");
                //Debug.Log("������");
            }
            else
            {
                PlayCharacterAnimation(characterName, characterDict[characterName].currentFace);
                //Debug.Log("������");
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
                return 2;//���Ϊ�գ����ع�����
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
                return 2;//���Ϊ�գ����ع�����
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
        CharacterData = new BookReader();
    }
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
