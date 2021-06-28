using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenu : MonoBehaviour
{
    protected ApplicationManager appManager;
    protected int sceneID;
    protected SortedList<EnumStringID,Button> buttons = new SortedList<EnumStringID,Button>();
    
    [SerializeField] private Button btnPrefab;
    private Button lastButton;

    private void OnDestroy()
    {
        appManager.reloadText -= RefreshText;
    }

    protected void InitMenu()
    {
        appManager = ApplicationManager.GetLink();
        if (appManager == null) Debug.Log("ApplicationManager not found");
        sceneID = appManager.GetSceneID();
        lastButton = null;
        appManager.reloadText += RefreshText;
    }

    public int GetNumButtons() => buttons.Count;

    private void SetLastButtonText(EnumStringID _id)
    {
        lastButton.transform.GetChild(0).GetComponent<Text>().text = CLocalisation.GetString(_id);
    }

    protected Button AddButton(EnumStringID _id)
    {
        lastButton = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity, transform);
        buttons.Add(_id,lastButton);
        SetLastButtonText(_id);

        return lastButton;
    }

    public void RefreshText()
    {
        int i;

        for(i=0;i<buttons.Count;i++)
        {
            foreach (KeyValuePair<EnumStringID, Button> kvp in buttons)
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
