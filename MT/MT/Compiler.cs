using System;
using System.Collections.Generic;
using System.Globalization;
using GardensPoint;

public class Compiler
{
    private static readonly Dictionary<string, Tuple<char, string>> _identificators;

    static Compiler()
    {
        _identificators = new Dictionary<string, Tuple<char, string>> { { "Pi", new Tuple<char, string>('r', Math.PI.ToString()) }, { "E", new Tuple<char, string>('r', Math.E.ToString()) } };
    }

    public static void Main()
    {
        try
        {
            Console.WriteLine("\nMultiline Calculator - Gardens Point");
            Console.Write("\n> ");
            Scanner scanner = new Scanner(Console.OpenStandardInput());
            Parser parser = new Parser(scanner);
            parser.Parse();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
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
        if(_identificators[id].Item2 == null)
            throw new ErrorException(string.Format("  variable {0} not initialized", id));

        return _identificators[id].Item2;
    }

    public static SemanticValue AritmeticalOp(SemanticValue left, SemanticValue right, Tokens t)
    {
        if(left.type == 'b' || right.type == 'b')
            throw new ErrorException("  bool not allowed in arithmetical ops");

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
                if(r != 0)
                    result = l / r;
                else
                    throw new ErrorException("  divide by zero");
                break;
            default:
                Console.WriteLine("  internal error");
                break;
        }

        if (res.type == 'i')
            res.val = ((int) result).ToString();
        else
            res.val = result.ToString();

        return res;
    }
}

public struct SemanticValue
{
    public char type;
    public string val;
}

class ErrorException : ApplicationException
{
    public ErrorException() { }
    public ErrorException(string msg) : base(msg) { }
    public ErrorException(string msg, Exception ex) : base(msg, ex) { }
}
