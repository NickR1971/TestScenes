using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UsedLocal { english = 0, ukrainian = 1 }

public class CLocalisation : MonoBehaviour
{
    [SerializeField] private GameObject localPrefab_en;
    [SerializeField] private GameObject localPrefab_ua;
    private CLocal local;
    private static SortedList<string, string> ui_string;

    private void Awake()
    {
        if (ui_string != null) return;
        else ui_string = new SortedList<string, string>();
        LoadLocalPrefab(localPrefab_en);
    }

    private void LoadLocalPrefab(GameObject _localPrefab)
    {
        GameObject loc = Instantiate(_localPrefab);
        ui_string.Clear();
        local = loc.GetComponent<CLocal>();
        local.Init(ui_string);
        Destroy(loc);
    }

    public void LoadLocal(UsedLocal _selectedLocal)
    {
        if (_selectedLocal == UsedLocal.english) LoadLocalPrefab(localPrefab_en);
        else if (_selectedLocal == UsedLocal.ukrainian) LoadLocalPrefab(localPrefab_ua);
        else Debug.Log("No realized localisation!");
    }
    public string GetString(string _key)
    {
        string value;

        if (!ui_string.TryGetValue(_key, out value))
            return $"<<empty key[{_key}]>>";
        return value;
    }
}
