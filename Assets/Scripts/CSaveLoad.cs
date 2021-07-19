using UnityEngine;

public class CSaveLoad : CUI
{
    [SerializeField] private CRecordContainer container;
    private void OpenSaveLoadWindow()
    {
        uiManager.OpenUI(this);
    }
    
    public void OpenSaveWindow()
    {
        container.CreateListSave(appManager.GetSavedList());
        OpenSaveLoadWindow();
    }

    public void OpenLoadWindow()
    {
        container.CreateListLoad(appManager.GetSavedList());
        OpenSaveLoadWindow();
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
