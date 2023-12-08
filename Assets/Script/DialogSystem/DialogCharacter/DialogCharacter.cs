using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCharacter : MonoBehaviour
{
    public string characterIndex;

    public string color;

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
    public void ChangeFace(string facename)
    {
        try
        {
            currentFace = faces[facename];
        }
        catch
        {
            Debug.Log("no Such Face");
        }
    }

    public void FaceTrans(DialogCharacterFace face)
    {
        currentFace = face;
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



