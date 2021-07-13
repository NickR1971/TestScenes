using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSaveLoad : CUI
{
    [SerializeField] private CRecordContainer container;
    private void OpenSaveLoadWindow(bool _isSave)
    {
        InitUI();
        container.CreateList(appManager.GetSavedList(), _isSave);
        uiManager.OpenUI(this);
    }
    
    public void OpenSaveWindow()
    {
        OpenSaveLoadWindow(_isSave: true);
    }

    public void OpenLoadWindow()
    {
        OpenSaveLoadWindow(_isSave: false);
    }

    public void CloseWindow()
    {
        container.DestroyRecords();
        uiManager.CloseUI();
    }

    public void Save(string _name)
    {
        CloseWindow();
        appManager.Save(_name);
    }

    public void Load(string _name)
    {
        CloseWindow();
        appManager.Load(_name);
    }

    public void Remove(string _name)
    {
        CloseWindow();
        appManager.RemoveSave(_name);
    }
}
