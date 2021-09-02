﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CMenu : CUI
{
    protected SortedList<EnumStringID,Button> buttons = new SortedList<EnumStringID,Button>();
    protected IMainMenu iMainMenu;

    [SerializeField] private Button btnPrefab;
    private Button lastButton;

    private void OnDestroy()
    {
        CLocalisation.reloadText -= RefreshText;
    }

    protected void InitMenu()
    {
        InitUI();
        iMainMenu = AllServices.Container.Get<IMainMenu>();
        lastButton = null;
        CLocalisation.reloadText += RefreshText;
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

        for (i = 0; i < buttons.Count; i++)
        {
            foreach (KeyValuePair<EnumStringID, Button> kvp in buttons)
            {
                lastButton = kvp.Value;
                SetLastButtonText(kvp.Key);
            }
        }
    }

    protected Button LastButton() => lastButton;
}
