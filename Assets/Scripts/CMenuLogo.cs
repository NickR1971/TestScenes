using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenuLogo : CMenu
{
    private Button loadButton = null;
    private ISaveLoad saveLoad;

    void Start()
    {
        InitMenu();
        saveLoad = AllServices.Container.Get<ISaveLoad>();
        AddButton(EnumStringID.ui_new).onClick.AddListener(NewGame);
        if (iMainMenu.IsGameExist())
            AddButton(EnumStringID.ui_continue).onClick.AddListener(ContinueGame);
        AddButton(EnumStringID.ui_load).onClick.AddListener(LoadGame);
        loadButton = LastButton();
        loadButton.interactable = saveLoad.IsSavedGameExist();
        AddButton(EnumStringID.ui_settings).onClick.AddListener(SetSettings);
        AddButton(EnumStringID.ui_quit).onClick.AddListener(ExitGame);
    }

    private void OnEnable()
    {
        saveLoad = AllServices.Container.Get<ISaveLoad>();
        if (loadButton != null)
        {
            loadButton.interactable = saveLoad.IsSavedGameExist();
        }
    }

    public void NewGame()
    {
        iMainMenu.NewGame();
    }

    public void ContinueGame()
    {
        iMainMenu.GoToMainScene();
    }

    public void LoadGame()
    {
        iMainMenu.Load();
    }

    public void SetSettings()
    {
        iMainMenu.OpenSettings();
    }

    public void ExitGame()
    {
        iMainMenu.Quit();
    }
    public override void OnCancel()
    {
        // disable close window on ESC by default
    }
}
