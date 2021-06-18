using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMenuMain : CMenu
{
    void Start()
    {
        InitMenu(40);
        AddButton("Save");
        AddButton("Settings");
        AddButton("Main menu").onClick.AddListener(GoMainMenu);
        AddButton("Quit").onClick.AddListener(ExitGame);
    }

    public void GoMainMenu()
    {
        appManager.MainMenuScene();
    }

    public void ExitGame()
    {
        appManager.Quit();
    }
}
