using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using GardensPoint;

public class Compiler
{
    private static readonly Dictionary<string, Tuple<char, string>> _identificators;

    static Compiler()
    {
        _identificators = new Dictionary<string, Tuple<char, string>> { { "Pi", new Tuple<char, string>('r', Math.PI.ToString(CultureInfo.InvariantCulture)) }, { "E", new Tuple<char, string>('r', Math.E.ToString(CultureInfo.InvariantCulture)) } };
    }

    public static void Main()
    {
        Console.WriteLine("\nMultiline Calculator - Gardens Point");
        Console.Write("\n> ");
        Scanner scanner = new Scanner(Console.OpenStandardInput());
        Parser parser = new Parser(scanner);
        parser.Parse();
    }

    public static void Declare(char type, string id)
    {
        if (_identificators.ContainsKey(id))
            throw new ErrorException(string.Format("  variable {0} already declared", id));

        _identificators[id] = new Tuple<char, string>(type, null);
    }

    public static void Mem(string id, SemanticValue value)
    {
        if (!_identificators.ContainsKey(id))
            throw new ErrorException(string.Format("  variable {0} not declared", id));

        if (_identificators[id].Item1 != value.type)
            throw new ErrorException("  types doesn't match");

        _identificators[id] = new Tuple<char, string>(value.type, value.val);
    }

    public static char GetType(string id)
    {
        if (!_identificators.ContainsKey(id))
            throw new ErrorException(string.Format("  variable {0} not declared", id));

        return _identificators[id].Item1;
    }

    public static string GetValue(string id)
    {
        if (!_identificators.ContainsKey(id))
            throw new ErrorException(string.Format("  variable {0} not declared", id));
        if (_identificators[id].Item2 == null)
            throw new ErrorException(string.Format("  variable {0} not initialized", id));

        return _identificators[id].Item2;
    }

    public static SemanticValue Function(string func, SemanticValue exp)
    {
        if (exp.error != null)
            return exp;
        if (exp.type == 'b')
            return new SemanticValue {error = "  bool not allowed in functions"};

        SemanticValue res = new SemanticValue();
        double str;
        double val = double.Parse(exp.val, CultureInfo.InvariantCulture);

        switch (func)
        {
            case "sin":
                str = Math.Sin(val);
                break;
            case "cos":
                str = Math.Cos(val);
                break;
            case "tg":
                str = Math.Tan(val);
                break;
            case "ctg":
                str = 1 / Math.Tan(val);
                break;
            case "sqrt":
                str = Math.Sqrt(val);
                break;
            case "lg2":
                str = Math.Log(val,2);
                break;
            default:
                return new SemanticValue { error = "  function not recognized" };
        }

        res.type = 'r';
        res.val = str.ToString(CultureInfo.InvariantCulture);

        return res;
    }

    public static SemanticValue AritmeticalOp(SemanticValue left, SemanticValue right, Tokens t)
    {
        if (right.error != null)
            return right;
        if (left.error != null)
            return left;
        if (left.type == 'b' || right.type == 'b')
            return new SemanticValue { error = "  bool not allowed in arithmetical ops" };

        SemanticValue res = new SemanticValue();
        double l = double.Parse(left.val, CultureInfo.InvariantCulture);
        double r = double.Parse(right.val, CultureInfo.InvariantCulture);
        double result = 0;

        res.type = (left.type == 'i' && right.type == 'i') ? 'i' : 'r';

        switch (t)
        {
            case Tokens.Plus:
                result = l + r;
                break;
            case Tokens.Minus:
                result = l - r;
                break;
            case Tokens.Multiplies:
                result = l * r;
                break;
            case Tokens.Divides:
                if (r != 0)
                    result = l / r;
                else
                    return new SemanticValue { error = "  divide by zero" };
                break;
            default:
                Console.WriteLine("  internal error");
                break;
        }

        if (res.type == 'i')
            res.val = ((int)result).ToString();
        else
            res.val = result.ToString(CultureInfo.InvariantCulture);

        return res;
    }
}

public struct SemanticValue
{
    public char type;
    public string val;
    public string error;
    public bool errorPrinted;
}

class ErrorException : ApplicationException
{
    public ErrorException() { }
    public ErrorException(string msg) : base(msg) { }
    public ErrorException(string msg, Exception ex) : base(msg, ex) { }
}