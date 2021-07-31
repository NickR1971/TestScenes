using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCalc : MonoBehaviour
{
    private enum Ops { nop=0, staple=1, plus=2, minus=3, mult=4, div=5 };
    private IGameConsole gameConsole;
    private Stack<float> result = new Stack<float>();
    private Stack<Ops> ops = new Stack<Ops>();
    private char currentChar;

    private int[] t0 = { 0, -1, 4, 4, 4, 4 };
    private int[] t1 = { 1, 1, 1, 1, 1, 1 };
    private int[] t2 = { 1, 1, 2, 2, 4, 4 };
    private int[] t3 = { 1, 1, 2, 2, 4, 4 };
    private int[] t4 = { 1, 1, 1, 1, 2, 2 };
    private int[] t5 = { 1, 1, 1, 1, 2, 2 };
    private int[] t6 = { -1, 3, 4, 4, 4, 4 };

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
                f = _f2 / _f1;
                break;
            default:
                Debug.LogError("calculation ops undefined");
                break;
        }

        return f;
    }

    private bool act(int[] _t)
    {
        bool r;
        int n;
        float f1, f2;
        Ops o;

        r = false;

        if (ops.Count == 0) n = 0;
        else n = ((int)ops.Peek());

        switch(_t[n])
        {
            case 0:
                f1 = result.Pop();
                gameConsole.ShowMessage($"={f1}");
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
                Debug.LogError($"Error detected :{currentChar}:");
                break;
        }
        return r;
    }

    private void OnCalc(string _str)
    {
        int i;
        float f;
        bool b = false;

        result.Clear();
        ops.Clear();
        gameConsole.ShowMessage($">>{_str}");
        i = 0;
        while (i < _str.Length)
        {
            b = false;
            switch(currentChar = _str[i])
            {
                case '0':              case '1':                case '2':                case '3':                case '4':
                case '5':              case '6':                case '7':                case '8':                case '9':
                case ',':
                    int j;
                    j = i;
                    while (j < _str.Length && isFloatChar(_str[j])) j++;
                    f = float.Parse(_str.Substring(i,j-i));
                    result.Push(f);
                    i = j-1;
                    break;
                case ')':
                    b = act(t6);
                    break;
                case '(':
                    b = act(t1);
                    break;
                case '+':
                    b = act(t2);
                    break;
                case '-':
                    b = act(t3);
                    break;
                case '*':
                    b = act(t4);
                    break;
                case '/':
                    b = act(t5);
                    break;
            }
            if (b) continue;
            i++;
        }
        currentChar = ' ';
        while (act(t0)) ;
    }
}
