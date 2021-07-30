using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMenuMain : CMenu
{
    void Start()
    {
        InitMenu();
        AddButton(EnumStringID.ui_continue).onClick.AddListener(ContinueGame);
        AddButton(EnumStringID.ui_save).onClick.AddListener(SaveGame);
        AddButton(EnumStringID.ui_settings).onClick.AddListener(SetSettings);
        AddButton(EnumStringID.ui_mainmenu).onClick.AddListener(GoMainMenu);
        AddButton(EnumStringID.ui_quit).onClick.AddListener(ExitGame);
    }

    public void SaveGame()
    {
        iMainMenu.Save();
    }

    public void ContinueGame()
    {
        uiManager.CloseUI();
    }

    public void GoMainMenu()
    {
        iMainMenu.MainMenuScene();
    }
    public void SetSettings()
    {
        iMainMenu.OpenSettings();
    }

    public void ExitGame()
    {
        iMainMenu.Quit();
    }
}
