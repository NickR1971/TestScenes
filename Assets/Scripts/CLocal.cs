using System;
using System.IO;
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
#if UNITY_EDITOR
            string WriteToFileName = $"{Application.dataPath}/Scripts/EnumStringID.cs";
            var constants = new List<string>();
#endif
            local_ui = JsonUtility.FromJson<CTest>(text_ui.text);
            foreach (CLocalisationData ldata in local_ui.loc)
            {
                local_str.Add(ldata.key, ldata.value);
#if UNITY_EDITOR
                constants.Add(ldata.key);
#endif           
            }
#if UNITY_EDITOR
            var content = $"public enum EnumStringID \n{{ \n" +
                string.Join(",\n",constants) +
                $" \n}}";

            File.WriteAllText(WriteToFileName, content);
#endif
        }
    }
}
