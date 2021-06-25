using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CSettings : MonoBehaviour
{
    private CLocalisation local;
    private ApplicationManager appManager;
    [SerializeField] private Text language_field;

    private void Awake()
    {
        appManager = FindObjectOfType<ApplicationManager>();
        if (appManager == null) Debug.Log("ApplicationManager not found");
        local = FindObjectOfType<CLocalisation>();
        if (local == null) Debug.Log("Localisation not found");

    }
    void Start()
    {
        ReloadTxt();
    }

    private void OnEnable()
    {
        Debug.Log("OnEnabled Window");
        appManager.reloadText += ReloadTxt;
    }

    private void OnDisable()
    {
        Debug.Log("OnDisabled window");
        appManager.reloadText -= ReloadTxt;
    }

    public void ReloadTxt()
    {
        language_field.text = local.GetString("current_language") + " " + local.GetString("language");
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
