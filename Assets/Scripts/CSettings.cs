using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSettings : CUI
{
    [SerializeField] private Text language_field;

    void Start()
    {
        InitUI();
        ReloadTxt();
    }

    public void ReloadTxt()
    {
        language_field.text = CLocalisation.GetString(EnumStringID.txt_currentLanguage) + " " + CLocalisation.GetString(EnumStringID.txt_language);
    }

    public void SetLanguageUA()
    {
        appManager.SetLanguage(UsedLocal.ukrainian);
        ReloadTxt();
    }

    public void SetLanguageEN()
    {
        appManager.SetLanguage(UsedLocal.english);
        ReloadTxt();
    }
}
