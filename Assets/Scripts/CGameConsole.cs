﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGameConsoleCommand
{
    public string command;
    public EnumStringID hint;
    public Action<string> action;

    public CGameConsoleCommand()
    {
        command = "none";
        hint = EnumStringID.txt_empty;
        action = null;
    }
    public CGameConsoleCommand(string _cmd, Action<string> _act=null, EnumStringID _hint=EnumStringID.txt_empty)
    {
        command = _cmd;
        hint = _hint;
        action = _act;
    }
}

public class CGameConsole : MonoBehaviour, IGameConsole
{
    [SerializeField] private InputField inputText;
    [SerializeField] private Transform containerTransform;
    [SerializeField] private GameObject consoleString;
    [SerializeField] private Scrollbar scroll;
    private const int maxMsgList = 20;
    private int currentMsg = 0;
    private GameObject[] msgList = new GameObject[maxMsgList];
    private SortedList<string, CGameConsoleCommand> commandsList = new SortedList<string, CGameConsoleCommand>();

    private void Start()
    {
        for (int i = 0; i < maxMsgList; i++)
        {
            msgList[i] = Instantiate(consoleString, containerTransform);
        }
        AddCommand(new CGameConsoleCommand("help", Help));
        AddCommand(new CGameConsoleCommand("quit", Quit,EnumStringID.ui_quit));
    }

    private void OnDestroy()
    {
        commandsList.Clear();
    }

    private void AddMsg(GameObject _msg)
    {
        if (currentMsg == maxMsgList) currentMsg = 0;
        Destroy(msgList[currentMsg]);
        msgList[currentMsg++] = _msg;
    }

    public IGameConsole GetIGameConsole()
    {
        return this;
    }

    public void AddCommand(CGameConsoleCommand _command)
    {
        if (commandsList.ContainsKey(_command.command)) commandsList.Remove(_command.command);
        commandsList.Add(_command.command, _command);
    }

    public void ShowMessage(string _message)
    {
        GameObject newString;
        newString = Instantiate(consoleString, containerTransform);
        newString.GetComponent<Text>().text = _message;
        AddMsg(newString);
        scroll.value = 0.0f;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Quit(string _str)
    {
        ApplicationManager.GetIMainMenu().Quit();
    }

    private void Help(string _str)
    {
        foreach(var cmd in commandsList)
        {
            ShowMessage($"{ cmd.Key} {CLocalisation.GetString(cmd.Value.hint)}");
        }
    }

    private void DoCommand(string _cmd)
    {
        CGameConsoleCommand gcCommand;

        if (commandsList.TryGetValue(CUtil.GetWord(_cmd), out gcCommand))
        {
            gcCommand.action?.Invoke(_cmd.Substring(gcCommand.command.Length).Trim());
        }
        else ShowMessage(CLocalisation.GetString(EnumStringID.msg_noCoomand) + " [" + _cmd + "]");
    }

    public void OnTextEnter()
    {
        string sText = inputText.text;

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (sText.Trim().Length > 0)
            {
                DoCommand(sText.Trim());
                inputText.text = "";
            }
        }
    }

    public void OnButton()
    {
        Hide();
    }
}
