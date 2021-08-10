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
        float resultCalc;

        gameConsole.ShowMessage($">>{_str}");
        if (calculator.TryCalc(_str.Replace('.',','), out resultCalc)) gameConsole.ShowMessage($"={resultCalc}");
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
