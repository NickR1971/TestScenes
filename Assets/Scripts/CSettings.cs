using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSettings : CUI
{
    [SerializeField] private Text language_field;

    void Start()
    {
        appManager = ApplicationManager.GetLink();
        ReloadTxt();
    }

    public void ReloadTxt()
    {
        language_field.text = CLocalisation.GetString(EnumStringID.current_language) + " " + CLocalisation.GetString(EnumStringID.language);
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
