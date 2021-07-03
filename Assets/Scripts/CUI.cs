using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUI : MonoBehaviour
{
    protected ApplicationManager appManager;
 
    protected void InitUI()
    {
        appManager = ApplicationManager.GetLink();
    }

    public void Hide() => gameObject.SetActive(false);

    public void Show() => gameObject.SetActive(true);

    public bool IsActive() => gameObject.activeSelf;
}
