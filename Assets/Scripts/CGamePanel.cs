using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGamePanel : CUI
{
    [SerializeField] private GameObject mainMenu;

    void Start()
    {
        appManager = ApplicationManager.GetLink();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsActive()) UI_manager.OpenUI(mainMenu.GetComponent<CUI>());
            else UI_manager.CloseUI();
        }
    }
}
