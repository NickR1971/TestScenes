using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CalcError { noErrorCode, divZzero, undefinedOperation, invalidExpression }
public interface ICalc : IService
{
    bool TryCalc(string _expression, out float _result);
    float Calc(string _expression);
    CalcError GetErrorCode();
}

class CalculationException : Exception
{
    private CalcError errorType;
    public CalculationException(CalcError _err)
    {
        errorType = _err;
    }
    public CalcError GetErrorType() => errorType;
}

public class CCalculator : ICalc
{
    private enum Ops { nop = 0, staple = 1, plus = 2, minus = 3, mult = 4, div = 5 };
    private Stack<float> result = new Stack<float>();
    private Stack<Ops> ops = new Stack<Ops>();
    private char currentChar;
    private CalcError errorCode;

    private readonly int[] onEnd = { 0, -1, 4, 4, 4, 4 };
    private readonly int[] onStapleOpen = { 1, 1, 1, 1, 1, 1 };
    private readonly int[] onPlus = { 1, 1, 2, 2, 4, 4 };
    private readonly int[] onMinus = { 1, 1, 2, 2, 4, 4 };
    private readonly int[] onMult = { 1, 1, 1, 1, 2, 2 };
    private readonly int[] onDiv = { 1, 1, 1, 1, 2, 2 };
    private readonly int[] onStapleClose = { -1, 3, 4, 4, 4, 4 };

    private bool IsFloatChar(char _c)
    {
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
            case '.':
                return true;
        }
        return false;
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

    private float Calculate(Ops _o, float _f1, float _f2)
    {
        float f;

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
                    throw new CalculationException(CalcError.divZzero);
                }
                break;
            default:
                throw new CalculationException(CalcError.undefinedOperation);
        }

        return f;
    }

    private bool Act(int[] _doIt)
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
                if (ops.Count == 0) throw new CalculationException(CalcError.invalidExpression);
                else o = ops.Pop();
                if (result.Count == 0) throw new CalculationException(CalcError.invalidExpression);
                else f1 = result.Pop();
                if (result.Count == 0) throw new CalculationException(CalcError.invalidExpression);
                else f2 = result.Pop();
                result.Push(Calculate(o, f1, f2));
                ops.Push(GetOps());
                break;
            case 1:
                ops.Push(GetOps());
                break;
            case 3:
                if (ops.Count == 0) throw new CalculationException(CalcError.invalidExpression);
                else ops.Pop();
                break;
            case 4:
                if (ops.Count == 0) throw new CalculationException(CalcError.invalidExpression);
                else o = ops.Pop();
                if (result.Count == 0) throw new CalculationException(CalcError.invalidExpression);
                else f1 = result.Pop();
                if (result.Count == 0) throw new CalculationException(CalcError.invalidExpression);
                else f2 = result.Pop();
                result.Push(Calculate(o, f1, f2));
                r = true;
                break;
            default:
                throw new CalculationException(CalcError.invalidExpression);
        }
        return r;
    }

    public CalcError GetErrorCode() => errorCode;

    public bool TryCalc(string _expression, out float _result)
    {
        try
        {
            _result = Calc(_expression);
        }
        catch (CalculationException ce)
        {
            errorCode = ce.GetErrorType();
            _result = 0;
            return false;
        }
        return true;
    }

    public float Calc(string _expression)
    {
        int i;

        errorCode = CalcError.noErrorCode;
        result.Clear();
        ops.Clear();

        i = 0;
        while (i < _expression.Length)
        {
            bool b = false;
            currentChar = _expression[i];
            if (IsFloatChar(currentChar))
            {
                int j;
                j = i;
                while (j < _expression.Length && IsFloatChar(_expression[j])) j++;
                result.Push(CUtil.StringToFloat(_expression.Substring(i, j - i)));
                i = j;
                continue;
            }

            switch (currentChar)
            {
                case ')':
                    b = Act(onStapleClose);
                    break;
                case '(':
                    b = Act(onStapleOpen);
                    break;
                case '+':
                    b = Act(onPlus);
                    break;
                case '-':
                    b = Act(onMinus);
                    break;
                case '*':
                    b = Act(onMult);
                    break;
                case '/':
                    b = Act(onDiv);
                    break;
            }
            if (b) continue;
            i++;
        }

        currentChar = ' ';
        while (Act(onEnd)) ;

        return result.Pop();
    }

}
