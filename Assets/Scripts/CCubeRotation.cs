using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCubeRotation : MonoBehaviour
{
    private int angle;
    private Material mtrl;
    private IGameConsole gameConsole;
    private const int maxColors = 8;
    [SerializeField] private Color[] colorList = new Color[maxColors];

    void Start()
    {
        SaveData data = CGameManager.GetData();
        angle = 0;
        mtrl = GetComponent<Renderer>().material;
        mtrl.color = data.GetColor();
        CGameManager.onSave += OnSave;
        gameConsole = ApplicationManager.GetGameConsole();
        gameConsole.SetInputParser(OnConsole);
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

    public void OnConsole(string _cmd)
    {
        if (_cmd.Contains("help"))
        {
            gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.msg_help));
        }
        else if (_cmd.Contains("quit"))
        {
            ApplicationManager.GetIMainMenu().Quit();
        }
        else if (_cmd.Contains("color"))
        {
            string str;
            int i = _cmd.IndexOf("color");
            i += 5;
            str = _cmd.Substring(i, _cmd.Length - i).Trim();
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
            else gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.msg_noParam));
        }
        else gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.msg_noCoomand) + " [" + _cmd + "]");
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
        SaveData data = CGameManager.GetData();

        data.SetColor(mtrl.color);
    }
}
