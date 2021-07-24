using System;

public interface IGameConsole
{
    void ShowMessage(string _msg);
    void AddCommand(CGameConsoleCommand _command);
    void Show();
    void Hide();
}