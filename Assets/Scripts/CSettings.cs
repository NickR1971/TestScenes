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

    void Start()
    {
        InitUI();
        profileField.text = appManager.GetProfile();
        if (appManager.IsGameExist()) editProfileButton.interactable = false;
    }

    public void SetLanguageUA()
    {
        appManager.SetLanguage(UsedLocal.ukrainian);
    }

    public void SetLanguageEN()
    {
        appManager.SetLanguage(UsedLocal.english);
    }

    public void EditProfile()
    {
        if (isEditProfile) return;
        profileField.gameObject.SetActive(false);
        profileInput.gameObject.SetActive(true);
        isEditProfile = true;
    }
    public void OnFinishedEdit()
    {
        string str;
        isEditProfile = false;
        str = profileInput.text.Trim();
        if (str.Length > 0)
        {
            profileField.text = str;
            appManager.SetProfile(str);
        }
        profileField.gameObject.SetActive(true);
        profileInput.gameObject.SetActive(false);
    }
}
