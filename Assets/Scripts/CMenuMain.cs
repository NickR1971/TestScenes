using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMenuMain : CMenu
{
    private bool f = true;
    void Start()
    {
        InitMenu();
        AddButton("ui_save").onClick.AddListener(SaveGame);
        AddButton("ui_settings").onClick.AddListener(SetSettings);
        AddButton("ui_mainmenu").onClick.AddListener(GoMainMenu);
        AddButton("ui_quit").onClick.AddListener(ExitGame);
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
        //appManager.ResetData();
        if (f) local.LoadLocal(UsedLocal.ukrainian);
        else local.LoadLocal(UsedLocal.english);
        f = !f;
        RefreshText();
    }

    public void ExitGame()
    {
        appManager.Quit();
    }
}
