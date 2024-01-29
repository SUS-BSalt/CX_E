using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
    public DialogManager Dialog;
    public GameObject LoadingMenu;
    public GameObject MainMenu;
    public void StartEveryThing()
    {
        LoadingMenu.SetActive(true);

        Dialog.gameObject.SetActive(true);
        Dialog.SetBook("Conversation/01.xlsx");
        Dialog.OnClick();
        MainMenu.SetActive(false);
        print("fuck");
        LoadingMenu.SetActive(false);
    }
    public void StartTestingScence()
    {
        LoadingMenu.SetActive(true);
        Dialog.gameObject.SetActive(true);
        Dialog.data = new(1,"", 1);
        Dialog.SetBook("test.xlsx");
        Dialog.OnClick();
        //print(Dialog.data.bookMark + "why?");
        //print(Dialog.data.currentBookChapter+"why?");

        MainMenu.SetActive(false);
        //print("fuck");
        LoadingMenu.SetActive(false);
    }
    public void OnLoad()
    {
        print("Director Load");
        Dialog.gameObject.SetActive(true);
        Dialog.OnLoad();
    }
    public void OnSave()
    {
        print("Director Save");
        Dialog.OnSave();
    }
}
