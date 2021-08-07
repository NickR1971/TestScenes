using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CCalc : MonoBehaviour
{
    private IGameConsole gameConsole;
    private CCalculator calculator;

    private void Awake()
    {
        calculator = new CCalculator();
        AllServices.Container.Register<ICalc>(calculator);
    }
    private void Start()
    {
        gameConsole = AllServices.Container.Get<IGameConsole>();
        CGameConsoleCommand cmd = new CGameConsoleCommand("=", OnCalc, EnumStringID.msg_calc);
        gameConsole.AddCommand(cmd);
    }

    private void OnCalc(string _str)
    {
        float f;

        gameConsole.ShowMessage($">>{_str}");
        if (calculator.DoCalc(_str, out f)) gameConsole.ShowMessage($"={f}");
        else
        {
            switch(calculator.GetErrorCode())
            {
                case CalcError.invalidExpression:
                    gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.err_error) + ": " + CLocalisation.GetString(EnumStringID.err_invalidExpression));
                    break;
                case CalcError.divZzero:
                    gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.err_error) + ": " + CLocalisation.GetString(EnumStringID.err_divZero));
                    break;
                case CalcError.undefinedOperation:
                    gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.err_error) + ": " + CLocalisation.GetString(EnumStringID.err_undefinedOperation));
                    break;
                default:
                    gameConsole.ShowMessage(CLocalisation.GetString(EnumStringID.err_error) + ": " + CLocalisation.GetString(EnumStringID.err_unknown));
                    break;
            }

        }
    }
}
