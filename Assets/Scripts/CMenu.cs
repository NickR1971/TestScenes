using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenu : MonoBehaviour
{
    protected ApplicationManager appManager;
    protected int sceneID;
    protected List<Button> buttons = new List<Button>();
    
    [SerializeField] private Button btnPrefab;

    protected void InitMenu()
    {
        appManager = FindObjectOfType<ApplicationManager>();
        if (appManager == null) Debug.Log("ApplicationManager not found");
        sceneID = appManager.GetSceneID();
    }

    public int GetNumButtons() => buttons.Count;

    protected Button AddButton(string _name)
    {
        Button btn = Instantiate(btnPrefab, Vector3.zero, Quaternion.identity, transform);
        btn.transform.GetChild(0).GetComponent<Text>().text = _name;
        buttons.Add(btn);

        return btn;
    }

    protected int LastButtonIndex() => GetNumButtons() - 1;
    protected Button LastButton() => buttons[GetNumButtons() - 1];
   
    public void Hide() =>
       gameObject.SetActive(false);

    public void Show() =>
        gameObject.SetActive(true);
}
