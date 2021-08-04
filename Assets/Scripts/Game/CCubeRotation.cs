using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCubeRotation : MonoBehaviour
{
    private IGame game;
    private int angle;
    private Material mtrl;
    private IGameConsole gameConsole;
    private const int maxColors = 8;
    private Color[] colorList = new Color[maxColors];

    private void Awake()
    {
        colorList[0] = Color.white;
        colorList[1] = Color.red;
        colorList[2] = new Color(1.0f,0.5f,0);
        colorList[3] = Color.yellow;
        colorList[4] = Color.green;
        colorList[5] = Color.cyan;
        colorList[6] = Color.blue;
        colorList[7] = Color.magenta;
    }

    void Start()
    {
        game = AllServices.Container.Get<IGame>();
        gameConsole = AllServices.Container.Get<IGameConsole>();
        SaveData data = game.GetData();
        game.AddOnSaveAction(OnSave);
 
        angle = 0;
        mtrl = GetComponent<Renderer>().material;
        mtrl.color = data.GetColor();
        CGameConsoleCommand cmd = new CGameConsoleCommand("color",OnConsole,EnumStringID.msg_help);
        gameConsole.AddCommand(cmd);
    }

    private void OnDestroy()
    {
        game.RemoveOnSaveAction(OnSave);
    }

    private void FixedUpdate()
    {
        if (++angle == 360) angle = 0;
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(angle, angle, 0);
    }

    public void OnConsole(string _cmd)
    {
        string str;
        int i;

        str = _cmd;
        if (str.Length > 0)
        {
            str = str.Trim();
            if (int.TryParse(str, out i))
            {
                if (i >= maxColors || i < 0) gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.msg_invalidParam) + " " + i.ToString());
                else
                {
                    mtrl.color = colorList[i];
                    gameConsole.ShowMessage("color=" + colorList[i].ToString());
                }
            }
            else gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.msg_noParam) + " <" + str + ">");
        }
        else gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.msg_noParam) + " [" + str + "]");
    }

    public void OnRed()
    {
        OnConsole("1");
    }

    public void OnGreen()
    {
        OnConsole("4");
    }

    public void OnBlue()
    {
        OnConsole("6");
    }

    public void OnSave()
    {
        SaveData data = game.GetData();

        data.SetColor(mtrl.color);
    }
}
