using System;

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

    public bool IsSaveExist(string _name)
    {
        return GetSaveIndex(_name) > -1;
    }
    private int GetSaveIndex(string _name)
    {
        int i;

        for (i = 0; i < savedList.Length; i++)
        {
            if (savedList[i] == _name) return i;
        }

        return -1;
    }

    public bool RemoveSave(string _name)
    {
        int removeIndex = GetSaveIndex(_name);
        if (removeIndex < 0) return false;

        int n = savedList.Length;
        string[] newList = new string[n - 1];
        int i = 0;

        for (; i < removeIndex; i++) newList[i] = savedList[i];
        for (i++; i < n; i++) newList[i - 1] = savedList[i];

        savedList = newList;

        return true;
    }

    public string[] GetSavedList()
    {
        string[] rt = new string[savedList.Length];

        for (int i = 0; i < savedList.Length; i++) rt[i] = savedList[i];

        return rt;
    }
}