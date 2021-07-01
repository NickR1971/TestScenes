using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CTextLocalize : MonoBehaviour
{
    private Text textField;
    private ApplicationManager appManager;
    [SerializeField] private EnumStringID strID;

    private void Start()
    {
        appManager = ApplicationManager.GetLink();
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
        textField.text = CLocalisation.GetString(strID);
    }
}
