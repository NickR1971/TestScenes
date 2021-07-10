﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : IUI
{
    private static Stack<CUI> activeUI = new Stack<CUI>();
    private static CUI currentUI=null;

    public void Init()
    {
        activeUI.Clear();
        currentUI = null;
    }

    public void OpenUI(CUI _ui)
    {
        if (currentUI != null) currentUI.Hide();
        activeUI.Push(currentUI);
        currentUI = _ui;
        currentUI.Show();
    }

    public void CloseUI()
    {
        if (currentUI == null) return;

        currentUI.Hide();
        currentUI = activeUI.Pop();
        if (currentUI != null) currentUI.Show();
    }
}
