using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
    public DialogManager Dialog;
    public GameObject LoadingMenu;
    public void StartEveryThing()
    {
        LoadingMenu.SetActive(true);

        Dialog.gameObject.SetActive(true);
        Dialog.SetBook("Conversation/01.xlsx");
        Dialog.OnClick();
        print("fuck");
        LoadingMenu.SetActive(false);
    }
}
