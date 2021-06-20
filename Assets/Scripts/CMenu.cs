using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenu : MonoBehaviour
{
    protected RectTransform panelRT;
    protected ApplicationManager appManager;
    protected int sceneID;
    protected const int maxButtons = 10;
    protected Button[] buttons = new Button[maxButtons];
    
    private Vector3 startPosition;
    private int intervalBetweenButtons;
    private int numButtons;
    private float menuWidth = 200.0f;
    [SerializeField] private Button btnPrefab;

    private void SetStartPosition()
    {
        if (numButtons > 1) startPosition = new Vector3(0, numButtons * 0.5f * intervalBetweenButtons, 0);
        else startPosition = new Vector3(0, 0.5f * intervalBetweenButtons, 0);
    }

    protected void InitMenu(int _interval)
    {
        numButtons = 0;
        intervalBetweenButtons = _interval;
        startPosition = new Vector3(0, 0.5f * intervalBetweenButtons, 0);
        panelRT = GetComponent<RectTransform>();
        appManager = FindObjectOfType<ApplicationManager>();
        if (appManager == null) Debug.Log("ApplicationManager not found");
        sceneID = appManager.GetSceneID();
    }

    public int GetNumButtons() { return numButtons; }

    private void RepositionButtons()
    {
        Vector3 posOffset;
        int i;

        SetStartPosition();
        for(i=0;i<numButtons;i++)
        {
            posOffset = new Vector3(0, -(i + 0.5f) * intervalBetweenButtons, 0);
            buttons[i].GetComponent<RectTransform>().localPosition = startPosition + posOffset;
        }
    }

    protected Button AddButton(string _name)
    {
        if (numButtons == maxButtons) return null;
        Button btn;

        btn = Instantiate(btnPrefab, startPosition, Quaternion.identity);
        btn.transform.GetChild(0).GetComponent<Text>().text = _name;
        btn.transform.SetParent(transform);
        panelRT.sizeDelta = new Vector2(menuWidth, numButtons * intervalBetweenButtons + 100);
        buttons[numButtons++] = btn;
        RepositionButtons();

        return btn;
    }

    protected int LastButtonIndex() { return numButtons - 1; }
    
    public void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }
}
