using UnityEngine;
using UnityEngine.UI;

public class CRecord : MonoBehaviour
{
    [SerializeField] private Text saveName;
    [SerializeField] private CSaveLoad manager;
    [SerializeField] private CTextLocalize buttonText;
    [SerializeField] private Button ActionButton;
    [SerializeField] private Button DeleteButton;
    private string strName;

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
            IDialog dlg = ApplicationManager.GetLink().GetDialogManager();
            dlg.OpenDialog(EDialog.Error, CLocalisation.GetString(EnumStringID.err_invalidName) + " " + _name);
        }
    }
    public void OnNewSave()
    {
        IDialog dlg = ApplicationManager.GetLink().GetDialogManager();
        dlg.SetOnInput(NewSave);
        dlg.OpenDialog(EDialog.Input, CLocalisation.GetString(EnumStringID.ui_newSave));
    }

    public void OnSaveOK()
    {
        //manager.Save(saveName.text);
        OnSaveCheck(saveName.text);
    }

    private void DoSave()
    {
        manager.Save(strName);
        strName = null;
    }

    private void OnSaveCheck(string _name)
    {
        ApplicationManager app = ApplicationManager.GetLink();
        if (app.IsSavedGameExist(_name))
        {
            IDialog dlg = app.GetDialogManager();
            strName = _name;
            dlg.OpenDialog(EDialog.Question, CLocalisation.GetString(EnumStringID.ui_save) + " " + saveName.text + "?", DoSave);
        }
        else manager.Save(_name);
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
        IDialog dlg = ApplicationManager.GetLink().GetDialogManager();
        dlg.OpenDialog(EDialog.Question, CLocalisation.GetString(EnumStringID.ui_remove) + " " + saveName.text + "?", DeleteOk);
    }
}
