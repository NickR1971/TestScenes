using System;
using System.Linq;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new local", menuName = "Scriptable/Local")]
public class CSLocal : ScriptableObject
{
    [SerializeField] private TextAsset text_ui;
    [SerializeField] private string enumName;
    private CTest localUI;

    public void GenerateScript()
    {
        //const string name = "EnumStringID";
        string WriteToFileName = $"{Application.dataPath}/Scripts/{enumName}.cs";
        localUI = JsonUtility.FromJson<CTest>(text_ui.text);
        if (localUI == null) Debug.LogError("localUI is null!");
        else Debug.Log("localUI is OK");

        var constants = localUI.loc.Select(item => item.key);

        var content = $"public enum {enumName} \n{{ \n" +
            string.Join(",\n", constants) +
            $" \n}}";

        File.WriteAllText(WriteToFileName, content);
    }
}
