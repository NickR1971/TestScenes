using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMenuLogo : CMenu
{
    void Start()
    {
        InitMenu(40);
        AddButton("New").onClick.AddListener(NewGame);
        AddButton("Load").onClick.AddListener(LoadGame);
        AddButton("Settings").onClick.AddListener(SetSettings);
        AddButton("Quit").onClick.AddListener(ExitGame);
    }

    public void NewGame()
    {
        appManager.NewGame();
    }

    public void LoadGame()
    {
        Debug.Log("Load Game");
    }

    public void SetSettings()
    {
        Debug.Log("Settings");
    }

    public void ExitGame()
    {
        appManager.Quit();
    }
}
