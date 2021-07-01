using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UI_manager
{
    private static Stack<CUI> activeUI = new Stack<CUI>();
    private static CUI currentUI=null;

    public static void Init()
    {
        activeUI.Clear();
        currentUI = null;
    }

    public static void OpenUI(CUI _ui)
    {
        if (currentUI != null) currentUI.Hide();
        activeUI.Push(currentUI);
        currentUI = _ui;
        currentUI.Show();
    }

    public static void CloseUI()
    {
        if (currentUI == null) return;

        currentUI.Hide();
        currentUI = activeUI.Pop();
        if (currentUI != null) currentUI.Show();
    }
}
