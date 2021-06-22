using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMenuLogo : CMenu
{
    private bool f = true;
    void Start()
    {
        InitMenu();
        AddButton("ui_new").onClick.AddListener(NewGame);
        if (appManager.IsGameExist())
            AddButton("ui_continue").onClick.AddListener(ContinueGame);
        AddButton("ui_load").onClick.AddListener(LoadGame);
        LastButton().interactable = appManager.IsSavedGameExist();
        AddButton("ui_settings").onClick.AddListener(SetSettings);
        AddButton("ui_quit").onClick.AddListener(ExitGame);
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
        if(f) local.LoadLocal(UsedLocal.ukrainian);
        else local.LoadLocal(UsedLocal.english);
        f = !f;
        RefreshText();
    }

    public void ExitGame()
    {
        appManager.Quit();
    }
}
