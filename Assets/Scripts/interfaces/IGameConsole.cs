using System;

public interface IGameConsole
{
    void ShowMessage(string _msg);
    void SetInputParser(Action<string> _inputParser);
    void Show();
    void Hide();
}