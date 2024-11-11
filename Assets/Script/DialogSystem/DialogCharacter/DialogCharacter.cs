using DG.Tweening;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogCharacter : MonoBehaviour
{
    public DialogcharacterData data = new(new Vector2(0.5f,0));

    public bool isOnStage { set { data.isOnStage = value; } get { return data.isOnStage; } }

    public Vector2 anchorPos { set { data.anchorPos = value; } get { return data.anchorPos; } }
    public string currentFaceName { set { data.currentFace = value; } get { return data.currentFace; } }

    public string characterIndex;

    public string color;//such like "#FFFFFF"

    //表情相关
    public List<DialogCharacterFace> faceList;
    public Dictionary<string, DialogCharacterFace> faces;
    public DialogCharacterFace currentFace;

    public RectTransform rect;
    public Animator animator;
    public Image image;
    public void Init()
    {
        color = DataManager.Instance.GetData<string>("Profile", "CharacterColor", characterIndex, "2");
    }
    public void Awake()
    {
        rect = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();

        faces = new();
        foreach (DialogCharacterFace face in faceList)
        {
            faces.Add(face.faceName, face);
        }
        currentFace = faceList[0];
        
    }

    public void OnAppear()
    {
        gameObject.SetActive(true);
        image.color = new Color(1, 1, 1, 0);
        image.DOColor(Color.white, 1f).SetEase(Ease.InSine);

        isOnStage = true;
        ChangeFace(currentFaceName);
        ChangePos(anchorPos);
    }
    public void OnDisappear()
    {
        gameObject.SetActive(false);
        image.color = new Color(1, 1, 1, 1);
        image.DOColor(new Color(1, 1, 1, 0), 1f).SetEase(Ease.InSine);

        isOnStage = false;
        currentFaceName = "normal";
        anchorPos = new Vector2(0.5f, 0);
    }

    public void ChangePos(Vector2 _anchorPos)
    {
        Vector2 temp = anchorPos;
        DOTween.To(() => temp, x => temp = x, _anchorPos, 1f)
            .SetEase(Ease.InOutQuad)
            .OnUpdate(() => 
            {   
                rect.anchorMax = temp;
                rect.anchorMin = temp;
            });
        anchorPos = _anchorPos;
        rect.anchorMax = anchorPos;
        rect.anchorMin = anchorPos;
    }
    public void ChangeFace(string facename)
    {
        try
        {
            currentFaceName = facename;
            currentFace = faces[currentFaceName];
        }
        catch
        {
            Debug.Log("no Such Face");
        }
    }

    public void FaceTrans(DialogCharacterFace face)
    {
        currentFace = face;
        currentFaceName = face.faceName;
    }

    public void SpeakController(bool isTalking)
    {
        if (isTalking)
        {
            animator.Play(currentFace.speakName);
        }
        else
        {
            animator.Play(currentFace.normalName);
        }
    }
}
[Serializable]
public class DialogcharacterData
{
    public string currentFace = "normal";
    [JsonIgnore]
    public Vector2 anchorPos;//百分比
    public bool isOnStage = false;
    public float posx = 0;
    public float posy = 0;
    public DialogcharacterData(Vector2 _anchorPos, string _currentFace = "normal", bool _isOnStage = false)
    {
        currentFace = _currentFace;
        anchorPos = _anchorPos;
        isOnStage = _isOnStage;
    }
    public void OnSave()
    {
        posx = anchorPos.x;
        posy = anchorPos.y;
    }
    public void OnLoad()
    {
        anchorPos = new(posx, posy);
    }
}



