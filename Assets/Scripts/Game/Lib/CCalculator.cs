using System.Collections;
using System.Collections.Generic;

public enum CalcError { divZzero, undefinedOperation, invalidExpression }
public interface ICalc : IService
{
    bool DoCalc(string _expression, out float _result);
    CalcError GetErrorCode();
}


public class CCalculator : ICalc
{
    private enum Ops { nop = 0, staple = 1, plus = 2, minus = 3, mult = 4, div = 5 };
    private Stack<float> result = new Stack<float>();
    private Stack<Ops> ops = new Stack<Ops>();
    private char currentChar;
    private bool isError;
    private CalcError errorCode;

    private int[] onEnd = { 0, -1, 4, 4, 4, 4 };
    private int[] onStapleOpen = { 1, 1, 1, 1, 1, 1 };
    private int[] onPlus = { 1, 1, 2, 2, 4, 4 };
    private int[] onMinus = { 1, 1, 2, 2, 4, 4 };
    private int[] onMult = { 1, 1, 1, 1, 2, 2 };
    private int[] onDiv = { 1, 1, 1, 1, 2, 2 };
    private int[] onStapleClose = { -1, 3, 4, 4, 4, 4 };

    private bool isFloatChar(char _c)
    {
        bool r;

        switch (_c)
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
        switch (currentChar)
        {
            case '(':
                return Ops.staple;
            case '+':
                return Ops.plus;
            case '-':
                return Ops.minus;
            case '*':
                return Ops.mult;
            case '/':
                return Ops.div;
        }
        return Ops.nop;
    }

    private float calculate(Ops _o, float _f1, float _f2)
    {
        float f = 0;

        switch (_o)
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
                    errorCode = CalcError.divZzero;
                }
                break;
            default:
                isError = true;
                errorCode = CalcError.undefinedOperation;
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

        switch (_doIt[n])
        {
            case 0:
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
                isError = true;
                errorCode = CalcError.invalidExpression;
                break;
        }
        return r;
    }

    public CalcError GetErrorCode() => errorCode;

    public bool DoCalc(string _expression, out float _result)
    {
        int i;
        float f;
        bool b = false;

        result.Clear();
        ops.Clear();
        _result = 0;
        isError = false;

        i = 0;
        while (i < _expression.Length)
        {
            b = false;
            switch (currentChar = _expression[i])
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
                    int j;
                    j = i;
                    while (j < _expression.Length && isFloatChar(_expression[j])) j++;
                    f = float.Parse(_expression.Substring(i, j - i));
                    result.Push(f);
                    i = j - 1;
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

}
