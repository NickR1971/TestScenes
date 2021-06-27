﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMenuMain : CMenu
{
    void Start()
    {
        InitMenu();
        AddButton(EnumStringID.ui_save).onClick.AddListener(SaveGame);
        AddButton(EnumStringID.ui_settings).onClick.AddListener(SetSettings);
        AddButton(EnumStringID.ui_mainmenu).onClick.AddListener(GoMainMenu);
        AddButton(EnumStringID.ui_quit).onClick.AddListener(ExitGame);
    }

    public void SaveGame()
    {
        appManager.Save();
    }

    public void GoMainMenu()
    {
        appManager.MainMenuScene();
    }
    public void SetSettings()
    {
        appManager.OpenSettings();
    }

    public void ExitGame()
    {
        appManager.Quit();
    }
}
