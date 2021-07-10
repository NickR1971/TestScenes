using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EDialog
{
    Question=0,
    Warning=1,
    Error=2
}

public class CDialog : CUI
{
    [SerializeField] private Image icon;
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;
    [SerializeField] private Button buttonCancel;
    [SerializeField] private Text messageText;
    [SerializeField] private Sprite[] icons = new Sprite[3];

    private Action onDialogYes = null;
    private Action onDialogNo = null;
    private Action onDialogCancel = null;

    private void Start()
    {
        InitUI();
    }

     public void OpenDialog(EDialog _dialogType, string _text, Action _onDialogYes = null, Action _onDialogNo = null, Action _onDialogCancel = null)
    {
        onDialogYes = _onDialogYes;
        onDialogNo = _onDialogNo;
        onDialogCancel = _onDialogCancel;
        icon.sprite = icons[(int)_dialogType];
        buttonNo.gameObject.SetActive(_dialogType == EDialog.Question);
        messageText.text = _text;
        UImanager.OpenUI(this);
    }

    private void ClearActions()
    {
        onDialogYes = null;
        onDialogNo = null;
        onDialogCancel = null;
    }

    public void OnYes()
    {
        UImanager.CloseUI();
        onDialogYes?.Invoke();
        ClearActions();
    }

    public void OnNo()
    {
        UImanager.CloseUI();
        onDialogNo?.Invoke();
        ClearActions();
    }

    public void OnCancel()
    {
        UImanager.CloseUI();
        onDialogCancel?.Invoke();
        ClearActions();
    }
}
