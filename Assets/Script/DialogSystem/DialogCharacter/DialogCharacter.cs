using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCharacter : MonoBehaviour
{
    public DialogcharacterDataStruct data = new(new Vector2(0.5f,0));

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
        isOnStage = true;
        ChangeFace(currentFaceName);
        ChangePos(anchorPos);
    }
    public void OnDisappear()
    {
        gameObject.SetActive(false);
        isOnStage = false;
        currentFaceName = "normal";
        anchorPos = new Vector2(0.5f, 0);
    }

    public void ChangePos(Vector2 _anchorPos)
    {
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

[SerializeField]
public struct DialogcharacterDataStruct
{
    public string currentFace;
    public Vector2 anchorPos;//百分比
    public bool isOnStage;
    public DialogcharacterDataStruct(Vector2 _anchorPos, string _currentFace = "normal", bool _isOnStage = false)
    {
        currentFace = _currentFace;
        anchorPos = _anchorPos;
        isOnStage = _isOnStage;
    }
}



