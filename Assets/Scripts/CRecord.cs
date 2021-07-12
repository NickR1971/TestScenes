using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CRecord : MonoBehaviour
{
    [SerializeField] private Text saveName;
    [SerializeField] private CSaveLoad manager;
    [SerializeField] private CTextLocalize buttonText;
    bool isSave;

    public void Init(string _name, bool _isSave)
    {
        saveName.text = _name;
        isSave = _isSave;
        buttonText.strID = (isSave) ? EnumStringID.ui_save.ToString() : EnumStringID.ui_load.ToString();
        //buttonText.RefreshText();
    }

    public void OnOK()
    {
        if (isSave) manager.Save(saveName.text);
        else manager.Load(saveName.text);
    }
    public void OnDelete()
    {
        manager.Remove(saveName.text);
    }
}
