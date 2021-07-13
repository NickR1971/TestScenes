using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

public class CProfileView : MonoBehaviour
{
    private Text textField;
    private ApplicationManager appManager;

    void Start()
    {
        appManager = ApplicationManager.GetLink();
        textField = GetComponent<Text>();
        textField.text = appManager.GetProfile();
    }
}
