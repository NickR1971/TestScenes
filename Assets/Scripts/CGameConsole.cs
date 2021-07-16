using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IGameConsole
{
    void ShowMessage(string _msg);
    void SetInputParser(Action<string> _inputParser);
    void Show();
    void Hide();
}

public class CGameConsole : MonoBehaviour, IGameConsole
{
    [SerializeField] private InputField inputText;
    [SerializeField] private Transform containerTransform;
    [SerializeField] private GameObject consoleString;
    [SerializeField] private Scrollbar scroll;
    private string sText;
    private const int maxMsgList = 20;
    private int currentMsg = 0;
    private GameObject[] msgList = new GameObject[maxMsgList];
    private Action<string> inputParser = null;

    private void Start()
    {
        sText = "";
        for (int i = 0; i < maxMsgList; i++)
        {
            msgList[i] = Instantiate(consoleString, containerTransform);
        }
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

    public void ShowMessage(string _message)
    {
        GameObject newString;
        newString = Instantiate(consoleString, containerTransform);
        newString.GetComponent<Text>().text = _message;
        AddMsg(newString);
        scroll.value = 0.0f;
    }

    public void SetInputParser(Action<string> _inputParser)
    {
        inputParser = _inputParser;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnTextEnter()
    {
        sText = inputText.text;
    }

    public void OnPlay()
    {
        if (sText.Trim().Length > 0)
        {
            inputParser?.Invoke(sText);
            inputText.text = "";
        }
    }

    public void OnButton()
    {
        Hide();
    }
}
