using UnityEngine;
using UnityEngine.UI;

public class CRecord : MonoBehaviour
{
    [SerializeField] private Text saveName;
    [SerializeField] private CSaveLoad manager;
    [SerializeField] private CTextLocalize buttonText;
    [SerializeField] private Button ActionButton;
    [SerializeField] private Button DeleteButton;
    bool isSave;

    public void Init(string _name, bool _isSave, bool _isZero=false)
    {
        saveName.text = _name;
        isSave = _isSave;
        buttonText.strID = (isSave) ? EnumStringID.ui_save.ToString() : EnumStringID.ui_load.ToString();
        if (_isZero) ActionButton.onClick.AddListener(OnNewSave);
        else ActionButton.onClick.AddListener(OnOK);
        if (_isZero) DeleteButton.gameObject.SetActive(false);
    }

    public void ResetTemplate()
    {
        ActionButton.onClick.RemoveAllListeners();
        DeleteButton.gameObject.SetActive(true);
    }

    private void NewSave(string _name)
    {
        if (CUtil.CheckNameForSave(_name)) manager.Save(_name.Replace('.','_'));
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

    public void OnOK()
    {
        Debug.Log("OnOK");
        if (isSave) manager.Save(saveName.text);
        else manager.Load(saveName.text);
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
