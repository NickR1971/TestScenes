using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTextLocalize : MonoBehaviour
{
    private Text textField;
    private CLocalisation local;
    private ApplicationManager appManager;
    [SerializeField] private EnumStringID strID;

    private void Start()
    {
        appManager = FindObjectOfType<ApplicationManager>();
        if(appManager==null) Debug.Log("Application manager not found in text_field");
        local = FindObjectOfType<CLocalisation>();
        if (local == null) Debug.Log("CLocalisation not found in text_field");
        textField = GetComponent<Text>();
        RefreshText();
        appManager.reloadText += RefreshText;
    }

    private void OnDestroy()
    {
        appManager.reloadText -= RefreshText;
    }
    public void RefreshText()
    {
        textField.text = local.GetString(strID);
    }
}
