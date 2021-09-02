using System;

public enum EDialog
{
    Question = 0,
    Message = 1,
    Error = 2,
    Input = 3
}

public interface IDialog : IService
{
    void SetOnYes(Action _onDialogEnd);
    void SetOnNo(Action _onDialogEnd);
    void SetOnCancel(Action _onDialogEnd);
    void SetOnInput(Action<string> _onDialogEnd);
    void SetDialog(EDialog _type, bool _enableOKbutton = true, bool _enableNoButton = false, bool _enableInputField = false);
    void OpenDialog(EDialog _dialogType, string _text, Action _onDialogYes = null);
    void ResetToDefault();
}

