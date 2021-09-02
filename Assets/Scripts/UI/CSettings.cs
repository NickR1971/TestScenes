﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSettings : CUI
{
    [SerializeField] private Text profileField;
    [SerializeField] private InputField profileInput;
    [SerializeField] private Button editProfileButton;
    private bool isEditProfile = false;
    private ISaveLoad saveLoad;
    private IDialog dialog;
    private IMainMenu iMainMenu;

    void Start()
    {
        InitUI();
        iMainMenu = AllServices.Container.Get<IMainMenu>();
        saveLoad = AllServices.Container.Get<ISaveLoad>();
        dialog = AllServices.Container.Get<IDialog>();
        profileField.text = saveLoad.GetProfile();
        if (iMainMenu.IsGameExist()) editProfileButton.interactable = false;
    }

    public void SetLanguageUA()
    {
        iMainMenu.SetLanguage(UsedLocal.ukrainian);
    }

    public void SetLanguageEN()
    {
        iMainMenu.SetLanguage(UsedLocal.english);
    }

    public void EditProfile()
    {
        if (isEditProfile) return;
        profileField.gameObject.SetActive(false);
        profileInput.gameObject.SetActive(true);
        profileInput.Select();
        profileInput.ActivateInputField();
        isEditProfile = true;
    }
    public void OnFinishedEdit()
    {
        string str;
        isEditProfile = false;
        str = profileInput.text.Trim().Replace('.','_').Replace('/','_').Replace('\\','_');
        if (str.Length > 0 && saveLoad.SetProfile(str))
        {
            profileField.text = str;
        }
        else ErrorMessage(EnumStringID.err_invalidName);
        profileField.gameObject.SetActive(true);
        profileInput.gameObject.SetActive(false);
    }

    private void ErrorMessage(EnumStringID _strID)
    {
        //IDialog dlg = ApplicationManager.GetDialogManager();
        dialog.OpenDialog(EDialog.Error, CLocalisation.GetString(_strID));
    }

    public override void OnYes()
    {
        iMainMenu.SaveSettings();
    }
}
