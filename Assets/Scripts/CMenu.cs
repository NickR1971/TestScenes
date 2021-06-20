using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenu : MonoBehaviour
{
    protected ApplicationManager appManager;
    protected int sceneID;
    protected const int maxButtons = 10;
    protected Button[] buttons = new Button[maxButtons];
    
    private int numButtons;
    [SerializeField] private Button btnPrefab;

    protected void InitMenu()
    {
        numButtons = 0;
        appManager = FindObjectOfType<ApplicationManager>();
        if (appManager == null) Debug.Log("ApplicationManager not found");
        sceneID = appManager.GetSceneID();
    }

    public int GetNumButtons() { return numButtons; }

    protected Button AddButton(string _name)
    {
        if (numButtons == maxButtons) return null;
        Button btn;

        btn = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity);
        btn.transform.GetChild(0).GetComponent<Text>().text = _name;
        btn.transform.SetParent(transform);
        buttons[numButtons++] = btn;

        return btn;
    }

    protected int LastButtonIndex() { return numButtons - 1; }
    
    public void Hide()
    {
       gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
