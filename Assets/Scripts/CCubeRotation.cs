using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCubeRotation : MonoBehaviour
{
    private int angle;
    private Material mtrl;

    void Start()
    {
        SaveData data = CGameManager.GetData();
        angle = 0;
        mtrl = GetComponent<Renderer>().material;
        mtrl.color = data.GetColor();
        CGameManager.onSave += OnSave;
    }

    private void OnDestroy()
    {
        CGameManager.onSave -= OnSave;
    }

    private void FixedUpdate()
    {
        if (++angle == 360) angle = 0;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(angle, angle, 0);
    }

    public void OnRed()
    {
        mtrl.color = Color.red;
    }

    public void OnGreen()
    {
        mtrl.color = Color.green;
    }

    public void OnBlue()
    {
        mtrl.color = Color.blue;
    }

    public void OnSave()
    {
        SaveData data = CGameManager.GetData();

        data.SetColor(mtrl.color);
    }
}
