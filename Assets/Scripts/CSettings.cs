using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSettings : MonoBehaviour
{
    private ApplicationManager appManager;
    [SerializeField] private Text language_field;

    private void Awake()
    {
        appManager = ApplicationManager.GetLink();
        if (appManager == null) Debug.Log("ApplicationManager not found");
    }
    void Start()
    {
        ReloadTxt();
    }

    private void OnEnable()
    {
        appManager.reloadText += ReloadTxt;
    }

    private void OnDisable()
    {
        appManager.reloadText -= ReloadTxt;
    }

    public void ReloadTxt()
    {
        language_field.text = CLocalisation.GetString(EnumStringID.current_language) + " " + CLocalisation.GetString(EnumStringID.language);
    }

    public void SetLanguageUA()
    {
        appManager.SetLanguage(UsedLocal.ukrainian);
    }
    public void SetLanguageEN()
    {
        appManager.SetLanguage(UsedLocal.english);
    }
}
