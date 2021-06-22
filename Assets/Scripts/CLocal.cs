using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] class CLocalisationData
{
    public string key;
    public string value;
};
[Serializable] class CTest
{
    public CLocalisationData[] loc;
}

public class CLocal : MonoBehaviour
{
    [SerializeField] private TextAsset text_ui;
    private CTest local_ui;

    public void Init(SortedList<string, string> local_str)
    {
        if (text_ui == null) Debug.Log("Localisation file not found!");
        else
        {
            local_ui = JsonUtility.FromJson<CTest>(text_ui.text);
            foreach (CLocalisationData ldata in local_ui.loc)
            {
                local_str.Add(ldata.key, ldata.value);
            }
        }
    }
}
