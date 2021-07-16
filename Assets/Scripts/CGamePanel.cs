using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGamePanel : CUI
{
    [SerializeField] private GameObject mainMenu;
    private IGameConsole gameConsole;

    void Start()
    {
        InitUI();
        gameConsole = appManager.GetGameConsole();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsActive()) uiManager.OpenUI(mainMenu.GetComponent<CUI>());
        }
        if(Input.GetKeyDown(KeyCode.BackQuote))
        {
            gameConsole.Show();
        }
    }
}
