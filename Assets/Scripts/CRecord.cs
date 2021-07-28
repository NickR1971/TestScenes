﻿using UnityEngine;
using UnityEngine.UI;

public class CRecord : MonoBehaviour
{
    [SerializeField] private Text saveName;
    [SerializeField] private CSaveLoad manager;
    [SerializeField] private CTextLocalize buttonText;
    [SerializeField] private Button ActionButton;
    [SerializeField] private Button DeleteButton;
    private IDialog dialog;
    private ISaveLoad saveLoad;
    private string strName;

    private void Start()
    {
        dialog = AllServices.Container.Get<IDialog>();
        saveLoad = AllServices.Container.Get<ISaveLoad>();
    }
    public void InitZero()
    {
        saveName.text = CLocalisation.GetString(EnumStringID.ui_newSave);
        buttonText.strID = EnumStringID.ui_save.ToString();
        ActionButton.onClick.AddListener(OnNewSave);
        DeleteButton.gameObject.SetActive(false);
    }

    public void InitSave(string _name)
    {
        saveName.text = _name;
        buttonText.strID = EnumStringID.ui_save.ToString();
        ActionButton.onClick.AddListener(OnSaveOK);
    }

    public void InitLoad(string _name)
    {
        saveName.text = _name;
        buttonText.strID = EnumStringID.ui_load.ToString();
        ActionButton.onClick.AddListener(OnLoadOK);
    }
    public void ResetTemplate()
    {
        ActionButton.onClick.RemoveAllListeners();
        DeleteButton.gameObject.SetActive(true);
    }

    private void NewSave(string _name)
    {
        if (CUtil.CheckNameForSave(_name)) OnSaveCheck(_name.Replace('.','_'));
        else
        {
            dialog.OpenDialog(EDialog.Error, CLocalisation.GetString(EnumStringID.err_invalidName) + " " + _name);
        }
    }
    public void OnNewSave()
    {
        dialog.SetOnInput(NewSave);
        dialog.OpenDialog(EDialog.Input, CLocalisation.GetString(EnumStringID.ui_newSave));
    }

    private void OnSaveOK()
    {
        OnSaveCheck(saveName.text);
    }

    private void DoSave()
    {
        manager.Save(strName);
        strName = null;
    }

    private void OnSaveCheck(string _name)
    {
        strName = _name;
        if (saveLoad.IsSavedGameExist(_name))
        {
            dialog.OpenDialog(EDialog.Question, CLocalisation.GetString(EnumStringID.ui_overwrite) + " " + strName + "?", DoSave);
        }
        else DoSave();
    }
 
    public void OnLoadOK()
    {
        manager.Load(saveName.text);
    }
    private void DeleteOk()
    {
        manager.Remove(saveName.text);
    }
    public void OnDelete()
    {
        dialog.OpenDialog(EDialog.Question, CLocalisation.GetString(EnumStringID.ui_remove) + " " + saveName.text + "?", DeleteOk);
    }
}
