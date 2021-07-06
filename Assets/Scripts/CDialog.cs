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

    private static CDialog thisExemplar = null;
    private static bool isOpen = false;
    private static bool isResultYes = false;
    private static bool isCancel = false;

    private Action onDialogExit = null;
    private void Awake()
    {
        thisExemplar = this;
    }

    private void Start()
    {
        InitUI();
        isOpen = false;
    }

    private void OnDestroy()
    {
        thisExemplar = null;
    }

    public static CDialog GetLink() => thisExemplar;

    public static bool IsOpen() => isOpen;
    public static bool IsResultYes() => isResultYes;
    public static bool IsCancel() => isCancel;

    public void OpenDialog(EDialog _dialogType, string _text, Action _onDialogExit = null)
    {
        onDialogExit = _onDialogExit;
        icon.sprite = icons[(int)_dialogType];
        buttonNo.gameObject.SetActive(_dialogType == EDialog.Question);
        messageText.text = _text;
        isOpen = true;
        UI_manager.OpenUI(this);
    }

    public void OnYes()
    {
        isOpen = false;
        isResultYes = true;
        isCancel = false;
        UI_manager.CloseUI();
        if (onDialogExit != null) onDialogExit();
        onDialogExit = null;
    }

    public void OnNo()
    {
        isOpen = false;
        isResultYes = false;
        isCancel = false;
        UI_manager.CloseUI();
        if (onDialogExit != null) onDialogExit();
        onDialogExit = null;
    }

    public void OnCancel()
    {
        isOpen = false;
        isResultYes = false;
        isCancel = true;
        UI_manager.CloseUI();
        if (onDialogExit != null) onDialogExit();
        onDialogExit = null;
    }
}
