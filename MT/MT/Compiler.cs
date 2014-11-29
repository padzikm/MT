using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Configuration;
using GardensPoint;

public class Compiler
{
    private static readonly Dictionary<string, Tuple<char, string>> _identificators;

    private static List<string> code;

    private static StreamWriter sw;

    public static int errors = 0;

    static Compiler()
    {
        _identificators = new Dictionary<string, Tuple<char, string>>
        {
            { "Pi", new Tuple<char, string>('r', Math.PI.ToString(CultureInfo.InvariantCulture)) },
            { "E", new Tuple<char, string>('r', Math.E.ToString(CultureInfo.InvariantCulture)) }
        };
    }

    public static int Main(string[] args)
    {
        string file;
        FileStream source;
        
        Console.WriteLine("\nCIL Code Generator");

        if (args.Length >= 1)
            file = args[0];
        else
        {
            Console.Write("\nsource file:  ");
            file = Console.ReadLine();
        }

        try
        {
            //var sr = new StreamReader(file);
            //string str = sr.ReadToEnd();
            //sr.Close();
            //Compiler.source = new List<string>(str.Split(new string[] { "\r\n" }, StringSplitOptions.None));
            source = new FileStream(file, FileMode.Open);
        }
        catch (Exception e)
        {
            Console.WriteLine("\n" + e.Message);
            return 1;
        }

        Scanner scanner = new Scanner(source);
        Parser parser = new Parser(scanner);
        
        Console.WriteLine();
        
        parser.Parse();
        source.Close();

        if (errors == 0)
        {
            GenCode(file + ".il");
            Console.WriteLine("  compilation successful\n");
        }
        else
            Console.WriteLine("\n  {0} errors detected\n", errors);

        return errors == 0 ? 0 : 2;
    }

    public static void EmitCode(string instr)
    {
        code.Add(instr);
    }

    public static void EmitCode(string instr, params object[] args)
    {
        code.Add(string.Format(instr, args));
    }

    private static void GenCode(string file)
    {
        sw = new StreamWriter(file);
        sw.WriteLine(".assembly extern mscorlib { }");
        sw.WriteLine(".assembly calculator { }");
        sw.WriteLine(".method static void main()");
        sw.WriteLine("{");
        sw.WriteLine(".entrypoint");
        sw.WriteLine(".try");
        sw.WriteLine("{");
        sw.WriteLine();

        sw.WriteLine("// initializing Pi and E");
        sw.WriteLine(".locals init ( float64 {0} )", "Pi");
        sw.WriteLine(".locals init ( float64 {0} )", "E");
        sw.WriteLine("ldc.r8 {0}", Math.PI);
        sw.WriteLine("stloc {0}", "Pi");
        sw.WriteLine("ldc.r8 {0}", Math.E);
        sw.WriteLine("stloc {0}", "E");
        sw.WriteLine();
        sw.WriteLine("// initializing temporary variables");
        sw.WriteLine(".locals init ( int32 {0} )", "_i");
        sw.WriteLine(".locals init ( int32 {0} )", "_ii");
        sw.WriteLine(".locals init ( float64 {0} )", "_r");
        sw.WriteLine(".locals init ( float64 {0} )", "_rr");
        sw.WriteLine();

        for (int i = 0; i < code.Count; ++i)
        {
            //sw.WriteLine("// linia {0,3} :  " + source[i], i + 1);
            //code[i].GenCode();
            sw.Write(code[i]);
            sw.WriteLine();
        }

        sw.WriteLine("leave EndMain");
        sw.WriteLine("}");
        sw.WriteLine("catch [mscorlib]System.Exception");
        sw.WriteLine("{");
        sw.WriteLine("callvirt instance string [mscorlib]System.Exception::get_Message()");
        sw.WriteLine("call void [mscorlib]System.Console::WriteLine(string)");
        sw.WriteLine("leave EndMain");
        sw.WriteLine("}");
        sw.WriteLine("EndMain: ret");
        sw.WriteLine("}");
        sw.Close();
    }

    public static void Print(SemanticValue value)
    {
        EmitCode("ldstr \"  Result: {0}{1}\"");

        if (value.type == 'i')
        {
            EmitCode("box [mscorlib]System.{0}", "Int32");
            EmitCode("ldstr \"{0}\"", "i");
        }
        else if (value.type == 'r')
        {
            EmitCode("box [mscorlib]System.{0}", "Double");
            EmitCode("ldstr \"{0}\"", "r");
        }
        else if (value.type == 'b')
            EmitCode("box [mscorlib]System.{0}", "Boolean");
        
        EmitCode("call void [mscorlib]System.Console::WriteLine(string,object,object)");

        if(value.type == 'b')
            Console.WriteLine(value.val);
        else
            Console.WriteLine(string.Format("{0}{1}", value.val, value.type));
    }

    public static void Declare(char type, string id)
    {
        if (_identificators.ContainsKey(id))
            throw new ErrorException(string.Format("  variable {0} already declared", id));

        if (type == 'b')
            EmitCode(".locals init ( int32 {0} )", id);
        if (type == 'i')
            EmitCode(".locals init ( int32 {0} )", id);
        if (type == 'r')
            EmitCode(".locals init ( float64 {0} )", id);

        _identificators[id] = new Tuple<char, string>(type, null);
    }

    public static void Mem(string id, SemanticValue value)
    {
        if (!_identificators.ContainsKey(id))
            throw new ErrorException(string.Format("  variable {0} not declared", id));

        if (_identificators[id].Item1 != value.type && !(_identificators[id].Item1 == 'r' && value.type == 'i'))
            throw new ErrorException("  types doesn't match");

        if(_identificators[id].Item1 == 'r' && value.type == 'i')
            EmitCode("conv.r8");

        EmitCode("stloc {0}", id);

        _identificators[id] = new Tuple<char, string>(_identificators[id].Item1, value.val);
    }

    public static SemanticValue Create(string val, char type)
    {
        SemanticValue res = new SemanticValue();
        res.type = type;

        if (type == 'b')
        {
            res.val = val;
            EmitCode("ldc.i4 {0}", bool.Parse(val) ? 1 : 0);
        }
        if (type == 'i')
        {
            res.val = int.Parse(val).ToString();
            EmitCode("ldc.i4 {0}", int.Parse(val));
        }
        if (type == 'r')
        {
            res.val = double.Parse(val).ToString(CultureInfo.InvariantCulture);
            EmitCode("ldc.r8 {0}", double.Parse(val, CultureInfo.InvariantCulture));
        }

        return res;
    }

    public static SemanticValue GetVariable(string id)
    {
        if (!_identificators.ContainsKey(id))
            return new SemanticValue{ error = string.Format("  variable {0} not declared", id), exit = true };
        if (_identificators[id].Item2 == null)
            return new SemanticValue{ error = string.Format("  variable {0} not initialized", id), exit = true};

        EmitCode("ldloc {0}", id);

        return new SemanticValue { type = _identificators[id].Item1, val = _identificators[id].Item2 };
    }

    public static SemanticValue Function(string func, SemanticValue exp)
    {
        if (exp.error != null)
            return exp;
        if (exp.type == 'b')
            return new SemanticValue {error = "  bool not allowed in functions", exit = true};

        SemanticValue res = new SemanticValue();
        double str;
        double val = double.Parse(exp.val, CultureInfo.InvariantCulture);

        EmitCode("conv.r8");

        switch (func)
        {
            case "sin":
                EmitCode("call float64 [mscorlib]System.Math::Sin(float64)");
                str = Math.Sin(val);
                break;
            case "cos":
                EmitCode("call float64 [mscorlib]System.Math::Cos(float64)");
                str = Math.Cos(val);
                break;
            case "tg":
                EmitCode("call float64 [mscorlib]System.Math::Tan(float64)");
                str = Math.Tan(val);
                break;
            case "ctg":
                EmitCode("stloc {0}", "_r");
                EmitCode("ldc.r8 {0}", 1);
                EmitCode("ldloc {0}", "_r");
                EmitCode("call float64 [mscorlib]System.Math::Tan(float64)");
                EmitCode("div");
                str = 1 / Math.Tan(val);
                break;
            case "sqrt":
                if(val < 0)
                    return new SemanticValue { error = "  square from negative number" };
                EmitCode("call float64 [mscorlib]System.Math::Sqrt(float64)");
                str = Math.Sqrt(val);
                break;
            case "lg2":
                if (val <= 0)
                    return new SemanticValue { error = "  log from non positive number" };
                EmitCode("ldc.i4 {0}", 2);
                EmitCode("call float64 [mscorlib]System.Math::Log(float64, int32)");
                str = Math.Log(val,2);
                break;
            default:
                return new SemanticValue { error = "  function not recognized", exit = true };
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
            return new SemanticValue { error = "  bool not allowed in arithmetical ops", exit = true };

        SemanticValue res = new SemanticValue();
        double l = double.Parse(left.val, CultureInfo.InvariantCulture);
        double r = double.Parse(right.val, CultureInfo.InvariantCulture);
        double result = 0;

        res.type = (left.type == 'i' && right.type == 'i') ? 'i' : 'r';

        if(left.type == 'i')
            EmitCode("stloc {0}", "_i");
        else if(left.type == 'r')
            EmitCode("stloc {0}", "_r");
        if(right.type == 'i')
            EmitCode("stloc {0}", "_ii");
        else if (right.type == 'r')
            EmitCode("stloc {0}", "_rr");
        if (left.type == 'i')
        {
            EmitCode("ldloc {0}", "_i");
            if(right.type == 'r')
                EmitCode("conv.r8");
        }
        else if (left.type == 'r')
            EmitCode("ldloc {0}", "_r");
        if (right.type == 'i')
        {
            EmitCode("ldloc {0}", "_ii");
            if(right.type == 'r')
                EmitCode("conv.r8");
        }
        else if (right.type == 'r')
            EmitCode("ldloc {0}", "_rr");

        switch (t)
        {
            case Tokens.Plus:
                EmitCode("add");
                result = l + r;
                break;
            case Tokens.Minus:
                EmitCode("sub");
                result = l - r;
                break;
            case Tokens.Multiplies:
                EmitCode("mul");
                result = l * r;
                break;
            case Tokens.Divides:
                if (r != 0)
                {
                    EmitCode("div");
                    result = l/r;
                }
                else
                    return new SemanticValue {error = "  divide by zero"};
                break;
            default:
                return new SemanticValue { error = "  internal error", exit = true };
        }

        if (res.type == 'i')
            res.val = ((int)result).ToString();
        else
            res.val = result.ToString(CultureInfo.InvariantCulture);

        return res;
    }

    public static SemanticValue RelationalOp(SemanticValue left, SemanticValue right, Tokens t)
    {
        if (right.error != null)
            return right;
        if (left.error != null)
            return left;
        SemanticValue res = new SemanticValue();

        if (t == Tokens.Equal || t == Tokens.Diff)
        {
            if (left.type == 'b' && right.type == 'b')
            {
                EmitCode("ceq");
                if (t == Tokens.Diff)
                {
                    EmitCode("ldc.i4 {0}", 0);
                    EmitCode("ceq");
                }
                bool l = bool.Parse(left.val);
                bool r = bool.Parse(right.val);
                bool result = t == Tokens.Equal ? l == r : l != r;
                res.val = result.ToString();
            }
            else if (left.type != 'b' && right.type != 'b')
            {
                double l = double.Parse(left.val, CultureInfo.InvariantCulture);
                double r = double.Parse(right.val, CultureInfo.InvariantCulture);
                bool result = t == Tokens.Equal ? l == r : l != r;
                res.val = result.ToString();
            }
            else
                return new SemanticValue { error = "  mixed bool and real / int not allowed in equals and diff ops", exit = true };
        }
        else
        {
            if (left.type == 'b' || right.type == 'b')
                return new SemanticValue { error = "  bool not allowed in relational (except equals and diff) ops", exit = true };
            
            double l = double.Parse(left.val, CultureInfo.InvariantCulture);
            double r = double.Parse(right.val, CultureInfo.InvariantCulture);
            bool result = false;

            switch (t)
            {
                case Tokens.Gt:
                    result = l > r;
                    break;
                case Tokens.Lt:
                    result = l < r;
                    break;
                case Tokens.Gte:
                    result = l >= r;
                    break;
                case Tokens.Lte:
                    result = l <= r;
                    break;
                default:
                    return new SemanticValue {error = "  internal error", exit = true};
            }

            res.val = result.ToString();
        }
        
        res.type = 'b';
        return res;
    }

    public static SemanticValue LogicalOp(SemanticValue left, SemanticValue right, Tokens t)
    {
        if (right.error != null)
            return right;
        if (left.error != null)
            return left;
        if (left.type != 'b' || right.type != 'b')
            return new SemanticValue { error = "  int / real not allowed in boolean ops", exit = true };

        SemanticValue res = new SemanticValue();
        bool l = bool.Parse(left.val);
        bool r = bool.Parse(right.val);
        bool result = false;

        switch (t)
        {
            case Tokens.And:
                EmitCode("and");
                EmitCode("ldc.i4 {0}", 0);
                EmitCode("cgt");
                result = l && r;
                break;
            case Tokens.Or:
                EmitCode("Or");
                EmitCode("ldc.i4 {0}", 0);
                EmitCode("cgt");
                result = l || r;
                break;
            default:
                return new SemanticValue { error = "  internal error", exit = true };
        }

        res.type = 'b';
        res.val = result.ToString();

        return res;
    }

    public static SemanticValue UnaryMinusOp(SemanticValue value)
    {
        if (value.error != null)
            return value;
        if (value.type == 'b')
            return new SemanticValue {error = "  bool not allowed in negation ops", exit = true};

        SemanticValue res = new SemanticValue();
        res.type = value.type;

        EmitCode("neg");

        if (value.type == 'i')
        {
            int v = int.Parse(value.val);
            res.val = (-v).ToString();
        }
        else
        {
            double v = double.Parse(value.val);
            res.val = (-v).ToString(CultureInfo.InvariantCulture);
        }

        return res;
    }

    public static SemanticValue NegationOp(SemanticValue value)
    {
        if (value.error != null)
            return value;
        if (value.type != 'b')
            return new SemanticValue { error = "  int / real not allowed in negation ops", exit = true };

        EmitCode("ldc.i4 {0}", 0);
        EmitCode("ceq");

        bool tmp = bool.Parse(value.val);
        SemanticValue res = new SemanticValue();
        res.type = 'b';
        res.val = (!tmp).ToString();

        return res;
    }
}

public struct SemanticValue
{
    public char type;
    public string val;
    public string error;
    public bool exit;
}

class ErrorException : ApplicationException
{
    public ErrorException() { }
    public ErrorException(string msg) : base(msg) { }
    public ErrorException(string msg, Exception ex) : base(msg, ex) { }
}