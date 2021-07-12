using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
	public uint id;
    //public Color color;
    public float r;
    public float g;
    public float b;
    public float a;

    public void SetColor(Color _color)
    {
        r = _color.r;
        g = _color.g;
        b = _color.b;
        a = _color.a;
    }

    public Color GetColor()
    {
        Color color = new Color(r, g, b, a);

        return color;
    }
}

[Serializable]
public class SettingsData
{
    public string profileName;
    public UsedLocal selected;

    public SettingsData()
    {
        profileName = "Player";
        selected = UsedLocal.english;
    }
}

[Serializable]
public class ProfileData
{
    public string name;
    public int savedGamesNumber;
    public string[] savedList;

    public ProfileData()
    {
        savedGamesNumber = 0;
        savedList = new string[1];
        savedList[0] = "new save";
    }

    public void AddSave(string _name)
    {
        int n = savedList.Length;
        string[] newList = new string[n + 1];

        newList[0] = savedList[0];
        newList[1] = _name;
        int i = 1;
        while (i < n)
        {
            newList[i + 1] = savedList[i];
            i++;
        }
        savedList = newList;
    }

    public bool RemoveSave(string _name)
    {
        int n = savedList.Length;
        string[] newList = new string[n - 1];

        int i1 = 0, i2 = 0;
        while (i1 < n)
        {
            if(savedList[i1]==_name)
            {
                i1++;
                continue;
            }
            if (i2 < (n - 1)) newList[i2++] = savedList[i1++];
            else break;
        }
        if (i2 < i1)
        {
            savedList = newList;
            return true;
        }

        return false;
    }
}