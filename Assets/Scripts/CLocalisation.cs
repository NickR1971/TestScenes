using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UsedLocal { english = 0, ukrainian = 1 }

public static class CLocalisation
{
    private static SortedList<string, string> ui_string;

    public static bool Init()
    {
        if (ui_string != null) return false;
        
        ui_string = new SortedList<string, string>();
        return true;
    }

    public static void LoadLocalPrefab(GameObject _localPrefab)
    {
        CLocal local;
        GameObject loc = MonoBehaviour.Instantiate(_localPrefab);
        ui_string.Clear();
        local = loc.GetComponent<CLocal>();
        local.Init(ui_string);
        MonoBehaviour.Destroy(loc);
    }

    public static string GetString(string _key)
    {
        if (ui_string.TryGetValue(_key, out string value))
            return value;
            
        return $"<<empty key[{_key}]>>";
    }
    public static string GetString(EnumStringID _id)
    {
        return GetString(_id.ToString());
    }
}
