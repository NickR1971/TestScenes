using System;
using System.Linq;
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
#if UNITY_EDITOR
    private void GenerateScript()
    {
        const string name = "EnumStringID";
        string WriteToFileName = $"{Application.dataPath}/Scripts/{name}.cs";
        
        var constants = local_ui.loc.Select(item => item.key);

        var content = $"public enum {name} \n{{ \n" +
            string.Join(",\n",constants) +
            $" \n}}";

        File.WriteAllText(WriteToFileName, content);
    }
#endif

    public void Init(SortedList<string, string> local_str)
    {
        if (text_ui == null)
        {
            Debug.Log("Localisation file not found!");
            return;
        }
        local_ui = JsonUtility.FromJson<CTest>(text_ui.text);
        foreach (CLocalisationData ldata in local_ui.loc)
        {
            local_str.Add(ldata.key, ldata.value);
        }
#if UNITY_EDITOR
        GenerateScript();
#endif
    }
}
