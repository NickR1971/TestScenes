﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMenuLogo : CMenu
{
    void Start()
    {
        InitMenu(40);
        AddButton("New").onClick.AddListener(NewGame);
        if (appManager.GetGameID() > 0)
        {
            AddButton("Continue").onClick.AddListener(ContinueGame);
        }
        AddButton("Load").onClick.AddListener(LoadGame);
        buttons[LastButtonIndex()].interactable = appManager.IsSavedGameExist();
        AddButton("Settings").onClick.AddListener(SetSettings);
        AddButton("Quit").onClick.AddListener(ExitGame);
    }

    public void NewGame()
    {
        appManager.NewGame();
    }

    public void ContinueGame()
    {
        appManager.GoToMainScene();
    }

    public void LoadGame()
    {
        appManager.Load();
    }

    public void SetSettings()
    {
        appManager.ResetData();
    }

    public void ExitGame()
    {
        appManager.Quit();
    }
}
