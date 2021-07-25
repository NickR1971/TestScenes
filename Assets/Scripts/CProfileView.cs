using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class CProfileView : MonoBehaviour
{
    private Text textField;

    void Start()
    {
        textField = GetComponent<Text>();
        textField.text = ApplicationManager.GatSaveLoad().GetProfile();
    }
}
