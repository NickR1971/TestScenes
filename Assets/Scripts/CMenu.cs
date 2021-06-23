using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenu : MonoBehaviour
{
    protected ApplicationManager appManager;
    protected int sceneID;
    protected SortedList<string,Button> buttons = new SortedList<string,Button>();
    protected CLocalisation local;
    
    [SerializeField] private Button btnPrefab;
    private Button lastButton;

    private void OnDestroy()
    {
        appManager.reloadText -= RefreshText;
    }

    protected void InitMenu()
    {
        local = FindObjectOfType<CLocalisation>();
        if (local == null) Debug.Log("Localisation not found");
        appManager = FindObjectOfType<ApplicationManager>();
        if (appManager == null) Debug.Log("ApplicationManager not found");
        sceneID = appManager.GetSceneID();
        lastButton = null;
        appManager.reloadText += RefreshText;
    }

    public int GetNumButtons() => buttons.Count;

    private void SetLastButtonText(string _name)
    {
        lastButton.transform.GetChild(0).GetComponent<Text>().text = local.GetString(_name);
    }
    protected Button AddButton(string _name)
    {
        lastButton = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity, transform);
        buttons.Add(_name,lastButton);
        SetLastButtonText(_name);

        return lastButton;
    }

    public void RefreshText()
    {
        int i;

        for(i=0;i<buttons.Count;i++)
        {
            foreach (KeyValuePair<string, Button> kvp in buttons)
            {
                lastButton = kvp.Value;
                SetLastButtonText(kvp.Key);
            }
        }
    }

    protected Button LastButton() => lastButton;
   
    public void Hide() =>
       gameObject.SetActive(false);

    public void Show() =>
        gameObject.SetActive(true);
}
