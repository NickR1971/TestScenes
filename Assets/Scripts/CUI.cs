using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUI : MonoBehaviour
{
    protected ApplicationManager appManager;
    protected IUI uiManager;
 
    protected void InitUI()
    {
        appManager = ApplicationManager.GetLink();
        uiManager = appManager.GetUImanager();
        if (appManager == null) Debug.Log("[CUI] application manager not found!");
        if (uiManager == null) Debug.Log("[CUI] ui manager not found!");
    }

    public void Hide() => gameObject.SetActive(false);

    public void Show() => gameObject.SetActive(true);

    public bool IsActive() => gameObject.activeSelf;
}
