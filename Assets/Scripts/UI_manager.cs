using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_manager : MonoBehaviour
{
    private Stack<CUI> activeUI = new Stack<CUI>();
    private CUI currentUI;
    private static UI_manager thisExemplar = null;

    private void Awake()
    {
        thisExemplar = this;
        currentUI = null;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentUI.IsHideOnESC())
        {
            if (currentUI.IsActive()) currentUI.Hide();
            else currentUI.Show();
        }
    }

    private void OnDestroy()
    {
        thisExemplar = null;
    }

    public static UI_manager GetLink()
    {
        if (thisExemplar == null) Debug.LogError("No created UI manager!");

        return thisExemplar;
    }


    public void SetActiveUI(CUI _ui)
    {
        if (currentUI != null) currentUI.Hide();
        activeUI.Push(currentUI);
        currentUI = _ui;
        if (!currentUI.IsHideOnESC())
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
