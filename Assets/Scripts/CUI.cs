using UnityEngine;

public class CUI : MonoBehaviour
{
    protected ApplicationManager appManager;
    protected IUI uiManager;
 
    public void InitUI(ApplicationManager _app)
    {
        appManager = _app;
        uiManager = appManager.GetUImanager();
        if (appManager == null) Debug.Log("[CUI] application manager not found!");
        if (uiManager == null) Debug.Log("[CUI] ui manager not found!");
    }
    public void Hide() => gameObject.SetActive(false);

    public void Show() => gameObject.SetActive(true);

    public bool IsActive() => gameObject.activeSelf;

    public virtual void OnOpen() { }

    public virtual void OnClose() { }

    public virtual void OnYes() { }
    public virtual void OnNo() { }
    public virtual void OnCancel() 
    {
        uiManager.CloseUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            OnYes();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnCancel();
            return;
        }
    }
}
