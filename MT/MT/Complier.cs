using System;
using System.Collections.Generic;
using GardensPoint;

public class Compiler
{
    private static readonly Dictionary<string, object> _identificators;

    static Compiler()
    {
        _identificators = new Dictionary<string, object> {{"Pi", Math.PI}, {"E", Math.E}};
    }

    public static void Main()
    {
        Console.WriteLine("\nMultiline Calculator - Gardens Point");
        Console.Write("\n> ");
        Scanner scanner = new Scanner(Console.OpenStandardInput());
        Parser parser = new Parser(scanner);
        parser.Parse();
        //object t1 = 1;
        //object t2 = 1.0;
        //object t4 = Activator.CreateInstance(typeof(double));
        //object t3 = true;
        //var t5 = Convert.ToInt32(t2);
        //var t6 = (double) t1;
        ////con
        //Console.WriteLine("intobj: {0}, doubleobj: {1}, boolobj: {2}, double: {3}", t1, t2, t3, t4);
        //Console.WriteLine("int: {0}", t1 is int);
        //Console.WriteLine("int: {0}", t5.GetType());
        //Console.WriteLine("double: {0}", t2.GetType());
        //Console.WriteLine("bool: {0}", t3.GetType());
        //Console.WriteLine("row: {0}", t2.GetType() == t4.GetType());
    }

    public static void Declare(string type, string id)
    {
        if (_identificators.ContainsKey(id))
            throw new ErrorException(string.Format("  variable {0} already declared", id));

        if(type == "int")
            _identificators[id] = default(int);
        else if(type == "real")
            _identificators[id] = default(double);
        else if(type == "bool")
            _identificators[id] = default(bool);
    }

    public static void Mem(string id, object value)
    {
        if (!_identificators.ContainsKey(id))
            throw new ErrorException(string.Format("  variable {0} not declared", id));

        if (_identificators[id].GetType() != value.GetType())
            throw new ErrorException("  types doesn't match");

        _identificators[id] = value;
    }

    public static object GetValue(string id)
    {
        if (!_identificators.ContainsKey(id))
            throw new ErrorException(string.Format("  variable {0} not declared", id));

        return _identificators[id];
    }
}

abstract public class SyntaxTree
{
    public abstract object Compute();
}

class AritmeticalOp : SyntaxTree
{
    private SyntaxTree _left;
    private SyntaxTree _right;
    private Tokens _kind;

    public AritmeticalOp(SyntaxTree l, SyntaxTree r, Tokens k) { _left = l; _right = r; _kind = k; }

    public override object Compute()
    {
        var left = _left.Compute();
        var right = _right.Compute();

        if (left is bool || right is bool)
            throw new ErrorException("  bool arguments in arithmetical op");

        var dLeft = Convert.ToDouble(left);
        var dRight = Convert.ToDouble(right);
        double res;

        switch (_kind)
        {
            case Tokens.Plus:
                res = dLeft + dRight;
                break;
            case Tokens.Minus:
                res = dLeft - dRight;
                break;
            case Tokens.Multiplies:
                res = dLeft * dRight;
                break;
            case Tokens.Divides:
                if (dRight != 0)
                    res = dLeft / dRight;
                else
                    throw new ErrorException("  runtime error - divide by zero");
                break;
            default:
                throw new ErrorException("  runtime internal error");
        }

        var result = (left is int && right is int) ? Convert.ToInt32(res) : res;

        return result;
    }
}

class IntNumber : SyntaxTree
{
    private int _val;

    public IntNumber(int val) { _val = val; }

    public override object Compute()
    {
        return _val;
    }
}

class RealNumber : SyntaxTree
{
    private double _val;

    public RealNumber(double val) { _val = val; }

    public override object Compute()
    {
        return _val;
    }
}

class Boolean : SyntaxTree
{
    private bool _val;

    public Boolean(bool val) { _val = val; }

    public override object Compute()
    {
        return _val;
    }
}

class Ident : SyntaxTree
{
    private string _id;

    public Ident(string id) { _id = id; }

    public override object Compute()
    {
        return Compiler.GetValue(_id);
    }
}

class ErrorException : ApplicationException
{
    public ErrorException() { }
    public ErrorException(string msg) : base(msg) { }
    public ErrorException(string msg, Exception ex) : base(msg, ex) { }
}