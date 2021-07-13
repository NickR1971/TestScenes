using System;
using UnityEngine;
using UnityEngine.UI;

public enum EDialog
{
    Question=0,
    Message=1,
    Error=2,
    Input=3
}

public interface IDialog
{
    void SetOnYes(Action _onDialogEnd);
    void SetOnNo(Action _onDialogEnd);
    void SetOnCancel(Action _onDialogEnd);
    void SetOnInput(Action<string> _onDialogEnd);
    void SetDialog(EDialog _type, bool _enableOKbutton = true, bool _enableNoButton = false, bool _enableInputField = false);
    void OpenDialog(EDialog _dialogType, string _text, Action _onDialogYes = null);
    void OpenDialog(string _text);
    void ResetToDefault();
}

public class CDialog : CUI, IDialog
{
    [SerializeField] private Image icon;
    [SerializeField] private Button buttonYes;
    [SerializeField] private Button buttonNo;
    [SerializeField] private Button buttonCancel;
    [SerializeField] private Text messageText;
    [SerializeField] private InputField inputText;
    [SerializeField] private Sprite[] icons = new Sprite[4];

    private Action onDialogYes = null;
    private Action onDialogNo = null;
    private Action onDialogCancel = null;
    private Action<string> onDialogInput = null;
    private EDialog currentType;
    private string sText;

   private void Start()
    {
        InitUI();
    }

    public override void OnOpen()
    {
        base.OnOpen();
        if(inputText.gameObject.activeSelf)
        {
            inputText.Select();
            inputText.ActivateInputField();
        }       
    }

    public void OpenDialog(string _text)
    {
        if (appManager == null) InitUI();
        if (uiManager == null) Debug.Log("ui manager not found!");
        messageText.text = _text;
        uiManager.OpenUI(this);
    }
    public void OpenDialog(EDialog _dialogType, string _text, Action _onDialogYes = null)
    {
        SetDialog(_dialogType, true, (_dialogType == EDialog.Question || _dialogType == EDialog.Input), (_dialogType == EDialog.Input));
        SetOnYes(_onDialogYes);
        OpenDialog(_text);
    }
    public void SetDialog(EDialog _type, bool _enableOKbutton = true, bool _enableNoButton = false, bool _enableInputField = false)
    {
        currentType = _type;
        icon.sprite = icons[(int)_type];
        buttonYes.gameObject.SetActive(_enableOKbutton);
        buttonNo.gameObject.SetActive(_enableNoButton);
        inputText.gameObject.SetActive(_enableInputField);
    }
    public void SetOnYes(Action _onDialogEnd)
    {
        onDialogYes = _onDialogEnd;
    }
    public void SetOnNo(Action _onDialogEnd)
    {
        onDialogNo = _onDialogEnd;
    }
    public void SetOnCancel(Action _onDialogEnd)
    {
        onDialogCancel = _onDialogEnd;
    }
    public void SetOnInput(Action<string> _onDialogEnd)
    {
        onDialogInput = _onDialogEnd;
    }

    public void ResetToDefault()
    {
        ClearActions();
        inputText.text = "";
        sText = "";
        SetDialog(EDialog.Message);
    }

    private void ClearActions()
    {
        onDialogYes = null;
        onDialogNo = null;
        onDialogCancel = null;
        onDialogInput = null;
  }

    public void OnYes()
    {
        uiManager.CloseUI();
        if (currentType == EDialog.Input) onDialogInput?.Invoke(sText);
        else onDialogYes?.Invoke();
        ClearActions();
    }

    public void OnNo()
    {
        uiManager.CloseUI();
        onDialogNo?.Invoke();
        ClearActions();
    }

    public void OnCancel()
    {
        uiManager.CloseUI();
        onDialogCancel?.Invoke();
        ClearActions();
    }

    public void OnInputExit()
    {
        sText = inputText.text;
    }
}
