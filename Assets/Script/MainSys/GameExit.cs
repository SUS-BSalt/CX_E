using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    public void ExitThisStupidGame()
    {
        print("QuitGame");
        Application.Quit();
    }
}
