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
    [SerializeField] private Color[] colorList = new Color[maxColors];

    void Start()
    {
        game = AllServices.Container.Get<IGame>();
        gameConsole = AllServices.Container.Get<IGameConsole>();
        SaveData data = game.GetData();
        //CGameManager.onSave += OnSave;
        game.AddOnSaveAction(OnSave);
 
        angle = 0;
        mtrl = GetComponent<Renderer>().material;
        mtrl.color = data.GetColor();
        CGameConsoleCommand cmd = new CGameConsoleCommand("color",OnConsole,EnumStringID.msg_help);
        gameConsole.AddCommand(cmd);
    }

    private void OnDestroy()
    {
        //CGameManager.onSave -= OnSave;
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
        if (str.Length > 1 && str[0] == '=')
        {
            str = str.Substring(1, str.Length - 1).Trim();
            if (int.TryParse(str, out i))
            {
                if (i >= maxColors || i < 0) gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.msg_invalidParam) + " " + i.ToString());
                else
                {
                    mtrl.color = colorList[i];
                    gameConsole.ShowMessage("color=" + colorList[i].ToString());
                }
            }
            else gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.msg_noParam) + " [" + str + "]");
        }
        else gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.msg_noParam) + " [" + str + "]");
    }

    public void OnRed()
    {
        mtrl.color = Color.red;
        gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.ui_red));
    }

    public void OnGreen()
    {
        mtrl.color = Color.green;
        gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.ui_green));
    }

    public void OnBlue()
    {
        mtrl.color = Color.blue;
        gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.ui_blue));
    }

    public void OnSave()
    {
        SaveData data = game.GetData();

        data.SetColor(mtrl.color);
    }
}
