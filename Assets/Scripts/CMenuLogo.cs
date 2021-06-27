using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMenuLogo : CMenu
{
    void Start()
    {
        InitMenu();
        AddButton(EnumStringID.ui_new).onClick.AddListener(NewGame);
        if (appManager.IsGameExist())
            AddButton(EnumStringID.ui_continue).onClick.AddListener(ContinueGame);
        AddButton(EnumStringID.ui_load).onClick.AddListener(LoadGame);
        LastButton().interactable = appManager.IsSavedGameExist();
        AddButton(EnumStringID.ui_settings).onClick.AddListener(SetSettings);
        AddButton(EnumStringID.ui_quit).onClick.AddListener(ExitGame);

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
        //appManager.ResetData();
        appManager.OpenSettings();
    }

    public void ExitGame()
    {
        appManager.Quit();
    }
}
