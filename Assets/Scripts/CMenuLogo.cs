using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenuLogo : CMenu
{
    private Button loadButton = null;
    void Start()
    {
        InitMenu();
        AddButton(EnumStringID.ui_new).onClick.AddListener(NewGame);
        if (appManager.IsGameExist())
            AddButton(EnumStringID.ui_continue).onClick.AddListener(ContinueGame);
        AddButton(EnumStringID.ui_load).onClick.AddListener(LoadGame);
        loadButton = LastButton();
        loadButton.interactable = appManager.IsSavedGameExist();
        AddButton(EnumStringID.ui_settings).onClick.AddListener(SetSettings);
        AddButton(EnumStringID.ui_quit).onClick.AddListener(ExitGame);

    }

    private void OnEnable()
    {
        if (loadButton != null)
        {
            loadButton.interactable = appManager.IsSavedGameExist();
        }
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
    public override void OnCancel()
    {
        // disable close window on ESC by default
    }
}
