using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCubeRotation : MonoBehaviour
{
    private int angle;
    private ApplicationManager appManager;

    void Start()
    {
        angle = 0;
        appManager = ApplicationManager.GetLink();
        if (appManager == null) Debug.Log("Application manager not found");
    }

    private void FixedUpdate()
    {
        if (++angle == 360) angle = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            appManager.InvertMenu();
        }
        transform.rotation = Quaternion.Euler(angle, angle, 0);
    }
}
