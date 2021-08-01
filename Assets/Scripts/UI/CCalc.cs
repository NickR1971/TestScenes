using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICalc : IService
{
    bool DoCalc(string _expression, out float _result);
    string GetErrorMessage();
}

public class CCalc : MonoBehaviour, ICalc
{
    private enum Ops { nop=0, staple=1, plus=2, minus=3, mult=4, div=5 };
    private IGameConsole gameConsole;
    private Stack<float> result = new Stack<float>();
    private Stack<Ops> ops = new Stack<Ops>();
    private char currentChar;
    private bool isError;
    private string msgError;

    private int[] onEnd = { 0, -1, 4, 4, 4, 4 };
    private int[] onStapleOpen = { 1, 1, 1, 1, 1, 1 };
    private int[] onPlus = { 1, 1, 2, 2, 4, 4 };
    private int[] onMinus = { 1, 1, 2, 2, 4, 4 };
    private int[] onMult = { 1, 1, 1, 1, 2, 2 };
    private int[] onDiv = { 1, 1, 1, 1, 2, 2 };
    private int[] onStapleClose = { -1, 3, 4, 4, 4, 4 };

    private void Awake()
    {
        AllServices.Container.Register<ICalc>(this);
    }
    private void Start()
    {
        gameConsole = AllServices.Container.Get<IGameConsole>();
        CGameConsoleCommand cmd = new CGameConsoleCommand("calc", OnCalc, EnumStringID.msg_calc);
        gameConsole.AddCommand(cmd);
    }

    private bool isFloatChar(char _c)
    {
        bool r;

        switch(_c)
        {
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
            case ',':
                r = true;
                break;
            default:
                r = false;
                break;
       }
        return r;
    }

    private Ops GetOps()
    {
        Ops o = Ops.nop;

        switch(currentChar)
        {
            case '(':
                o = Ops.staple;
                break;
            case '+':
                o = Ops.plus;
                break;
            case '-':
                o = Ops.minus;
                break;
            case '*':
                o = Ops.mult;
                break;
            case '/':
                o = Ops.div;
                break;
        }
        return o;
    }

    private float calculate(Ops _o, float _f1, float _f2)
    {
        float f = 0;

        switch(_o)
        {
            case Ops.plus:
                f = _f2 + _f1;
                break;
            case Ops.minus:
                f = _f2 - _f1;
                break;
            case Ops.mult:
                f = _f2 * _f1;
                break;
            case Ops.div:
                if (_f1 != 0) f = _f2 / _f1;
                else
                {
                    isError = true;
                    msgError = "divide by zero";
                }
                break;
            default:
                Debug.LogError("calculation ops undefined");
                break;
        }

        return f;
    }

    private bool act(int[] _doIt)
    {
        bool r;
        int n;
        float f1, f2;
        Ops o;

        r = false;

        if (ops.Count == 0) n = 0;
        else n = ((int)ops.Peek());

        switch(_doIt[n])
        {
            case 0:
                //f1 = result.Pop();
                //gameConsole.ShowMessage($"={f1}");
                break;
            case 2:
                o = ops.Pop();
                f1 = result.Pop();
                f2 = result.Pop();
                result.Push(calculate(o, f1, f2));
                ops.Push(GetOps());
                break;
            case 1:
                ops.Push(GetOps());
                break;
            case 3:
                ops.Pop();
                break;
            case 4:
                o = ops.Pop();
                f1 = result.Pop();
                f2 = result.Pop();
                result.Push(calculate(o, f1, f2));
                r = true;
                break;
            default:
                //Debug.LogError($"Error detected :{currentChar}:");
                isError = true;
                msgError = "invalid expression";
                break;
        }
        return r;
    }

    public string GetErrorMessage() => msgError;
    public bool DoCalc(string _expression, out float _result)
    {
        int i;
        float f;
        bool b = false;

        result.Clear();
        ops.Clear();
        _result = 0;
        isError = false;
        msgError = "";

        i = 0;
        while (i < _expression.Length)
        {
            b = false;
            switch(currentChar = _expression[i])
            {
                case '0':              case '1':                case '2':                case '3':                case '4':
                case '5':              case '6':                case '7':                case '8':                case '9':
                case ',':
                    int j;
                    j = i;
                    while (j < _expression.Length && isFloatChar(_expression[j])) j++;
                    f = float.Parse(_expression.Substring(i,j-i));
                    result.Push(f);
                    i = j-1;
                    break;
                case ')':
                    b = act(onStapleClose);
                    break;
                case '(':
                    b = act(onStapleOpen);
                    break;
                case '+':
                    b = act(onPlus);
                    break;
                case '-':
                    b = act(onMinus);
                    break;
                case '*':
                    b = act(onMult);
                    break;
                case '/':
                    b = act(onDiv);
                    break;
            }
            if (b) continue;
            if (isError) break;
            i++;
        }

        if (isError) return false;
        currentChar = ' ';
        while (act(onEnd)) ;
        if (isError) return false;
        
        _result = result.Pop();

        return true;
    }

    private void OnCalc(string _str)
    {
        float f;

        gameConsole.ShowMessage($">>{_str}");
        if (DoCalc(_str, out f)) gameConsole.ShowMessage($"={f}");
        else gameConsole.ShowMessage($"Calculation error: {msgError}");
    }
}
