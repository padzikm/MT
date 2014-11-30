using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using GardensPoint;

public class Compiler
{
    private static readonly Dictionary<string, Tuple<char, string>> _identificators;

    private static List<string> code;

    private static StreamWriter sw;

    public static List<string> sourceCode;

    public static bool genCode;

    public static Dictionary<int, string> errors;

    static Compiler()
    {
        _identificators = new Dictionary<string, Tuple<char, string>>
        {
            { "Pi", new Tuple<char, string>('r', Math.PI.ToString(CultureInfo.InvariantCulture)) },
            { "E", new Tuple<char, string>('r', Math.E.ToString(CultureInfo.InvariantCulture)) }
        };

        genCode = true;

        errors = new Dictionary<int, string>();
    }

    public static int Main(string[] args)
    {
        try
        {
            string file;
            FileStream source;

            Console.WriteLine("\n  CIL Code Generator");

            if (args.Length >= 1)
                file = args[0];
            else
            {
                Console.Write("\nsource file:  ");
                file = Console.ReadLine();
            }

            try
            {
                var sr = new StreamReader(file);
                string str = sr.ReadToEnd();
                sr.Close();
                sourceCode = new List<string>(str.Split(new string[] {"\r\n"}, StringSplitOptions.None));
                code = new List<string>();
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

            if (errors.Count == 0)
            {
                GenCode(file + ".il");
                Console.WriteLine("  Compilation successful");
            }
            else
            {
                Console.WriteLine("  Compilation aborted");
                Console.WriteLine("\n  {0} errors detected", errors.Count);

                foreach (var error in errors)
                    Console.WriteLine("\nline: {0} {1}", error.Key, error.Value);
            }

            return errors.Count == 0 ? 0 : 2;
        }
        catch (Exception ex)
        {
            Console.WriteLine("  Compilation aborted\n");
            Console.WriteLine("  Internal error: {0}\n", ex.Message);

            return 2;
        }
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
        sw.WriteLine(".assembly padzikm { }");
        sw.WriteLine(".method static void main()");
        sw.WriteLine("{");
        sw.WriteLine(".entrypoint");
        sw.WriteLine(".try");
        sw.WriteLine("{");
        sw.WriteLine();

        sw.WriteLine("// initializing Pi and E");
        sw.WriteLine(".locals init ( float64 {0} )", "Pi");
        sw.WriteLine(".locals init ( float64 {0} )", "E");
        sw.WriteLine(string.Format(CultureInfo.InvariantCulture, "ldc.r8 {0}", Math.PI));
        sw.WriteLine("stloc {0}", "Pi");
        sw.WriteLine(string.Format(CultureInfo.InvariantCulture, "ldc.r8 {0}", Math.E));
        sw.WriteLine("stloc {0}", "E");
        sw.WriteLine();
        sw.WriteLine("// initializing temporary variables");
        sw.WriteLine(".locals init ( int32 {0} )", "_i");
        sw.WriteLine(".locals init ( float64 {0} )", "_r");
        sw.WriteLine();

        for (int i = 0; i < code.Count; ++i)
            sw.WriteLine(code[i]);

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
        if (value.type == 'i')
        {
            EmitCode("stloc {0}", "_i");
            EmitCode("ldstr \"  Result: {0}{1}\"");
            EmitCode("ldloc {0}", "_i");
            EmitCode("box [mscorlib]System.{0}", "Int32");
            EmitCode("ldstr \"{0}\"", "i");
            EmitCode("call void [mscorlib]System.Console::WriteLine(string,object,object)");
        }
        else if (value.type == 'r')
        {
            EmitCode("stloc {0}", "_r");
            EmitCode("ldstr \"  Result: {0}{1}\"");
            EmitCode("ldloc {0}", "_r");
            EmitCode("box [mscorlib]System.{0}", "Double");
            EmitCode("ldstr \"{0}\"", "r");
            EmitCode("call void [mscorlib]System.Console::WriteLine(string,object,object)");
        }
        else if (value.type == 'b')
        {
            EmitCode("stloc {0}", "_i");
            EmitCode("ldstr \"  Result: {0}\"");
            EmitCode("ldloc {0}", "_i");
            EmitCode("box [mscorlib]System.{0}", "Boolean");
            EmitCode("call void [mscorlib]System.Console::WriteLine(string,object)");
        }

        //if (value.type == 'b')
        //    Console.WriteLine(value.val);
        //else
        //    Console.WriteLine("{0}{1}", value.val, value.type);
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
        if (!genCode)
            return new SemanticValue();
        
        SemanticValue res = new SemanticValue();
        res.type = type;

        if (type == 'b')
        {
            res.val = val;
            EmitCode("ldc.i4 {0}", bool.Parse(val) ? 1 : 0);
        }
        if (type == 'i')
        {
            int tmp;
            if (int.TryParse(val, out tmp))
            {
                res.val = tmp.ToString(CultureInfo.InvariantCulture);
                EmitCode("ldc.i4 {0}", res.val);
            }
            else
                return new SemanticValue {error = string.Format("  value {0} not fitted in int", val)};
        }
        if (type == 'r')
        {
            double tmp;
            if (double.TryParse(val, NumberStyles.Float, CultureInfo.InvariantCulture, out tmp))
            {
                res.val = tmp.ToString(CultureInfo.InvariantCulture);
                EmitCode("ldc.r8 {0}", res.val);
            }
            else
                return new SemanticValue { error = string.Format("  value {0} not fitted in double", val) };
        }

        return res;
    }

    public static SemanticValue GetVariable(string id)
    {
        if (!genCode)
            return new SemanticValue();
        if (!_identificators.ContainsKey(id))
            return new SemanticValue{ error = string.Format("  variable {0} not declared", id) };
        if (_identificators[id].Item2 == null)
            return new SemanticValue{ error = string.Format("  variable {0} not initialized", id)};

        EmitCode("ldloc {0}", id);

        return new SemanticValue { type = _identificators[id].Item1, val = _identificators[id].Item2 };
    }

    public static SemanticValue Function(string func, SemanticValue exp)
    {
        if (!genCode)
            return new SemanticValue();
        if (exp.error != null)
            return exp;
        if (exp.type == 'b')
            return new SemanticValue {error = "  bool not allowed in functions"};

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
                EmitCode("ldc.r8 {0}", 1.0);
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
                return new SemanticValue { error = "  function not recognized" };
        }

        res.type = 'r';
        res.val = str.ToString(CultureInfo.InvariantCulture);

        return res;
    }

    public static SemanticValue AritmeticalOp(SemanticValue left, SemanticValue right, Tokens t)
    {
        if (!genCode)
            return new SemanticValue();
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

        if (left.type == 'r' && right.type == 'i')
            EmitCode("conv.r8");
        else if(left.type == 'i' && right.type == 'r')
        {
            EmitCode("stloc {0}", "_r");
            EmitCode("conv.r8");
            EmitCode("ldloc {0}", "_r");
        }

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
                return new SemanticValue { error = "  internal error" };
        }

        if (res.type == 'i')
            res.val = ((int)result).ToString(CultureInfo.InvariantCulture);
        else
            res.val = result.ToString(CultureInfo.InvariantCulture);

        return res;
    }

    public static SemanticValue RelationalOp(SemanticValue left, SemanticValue right, Tokens t)
    {
        if (!genCode)
            return new SemanticValue();
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
                if (left.type == 'r' && right.type == 'i')
                    EmitCode("conv.r8");
                else if (left.type == 'i' && right.type == 'r')
                {
                    EmitCode("stloc {0}", "_r");
                    EmitCode("conv.r8");
                    EmitCode("ldloc {0}", "_r");
                }
                EmitCode("ceq");
                if (t == Tokens.Diff)
                {
                    EmitCode("ldc.i4 {0}", 0);
                    EmitCode("ceq");
                }
                double l = double.Parse(left.val, CultureInfo.InvariantCulture);
                double r = double.Parse(right.val, CultureInfo.InvariantCulture);
                bool result = t == Tokens.Equal ? l == r : l != r;
                res.val = result.ToString();
            }
            else
                return new SemanticValue { error = "  mixed bool and real / int not allowed in equals and diff ops" };
        }
        else
        {
            if (left.type == 'b' || right.type == 'b')
                return new SemanticValue { error = "  bool not allowed in relational (except equals and diff) ops" };
            
            double l = double.Parse(left.val, CultureInfo.InvariantCulture);
            double r = double.Parse(right.val, CultureInfo.InvariantCulture);
            bool result = false;

            if (left.type == 'r' && right.type == 'i')
                EmitCode("conv.r8");
            else if (left.type == 'i' && right.type == 'r')
            {
                EmitCode("stloc {0}", "_r");
                EmitCode("conv.r8");
                EmitCode("ldloc {0}", "_r");
            }

            switch (t)
            {
                case Tokens.Gt:
                    EmitCode("cgt");
                    result = l > r;
                    break;
                case Tokens.Lt:
                    EmitCode("clt");
                    result = l < r;
                    break;
                case Tokens.Gte:
                    EmitCode("clt");
                    EmitCode("ldc.i4 {0}", 0);
                    EmitCode("ceq");
                    result = l >= r;
                    break;
                case Tokens.Lte:
                    EmitCode("cgt");
                    EmitCode("ldc.i4 {0}", 0);
                    EmitCode("ceq");
                    result = l <= r;
                    break;
                default:
                    return new SemanticValue {error = "  internal error"};
            }

            res.val = result.ToString();
        }
        
        res.type = 'b';
        return res;
    }

    public static SemanticValue LogicalOp(SemanticValue left, SemanticValue right, Tokens t)
    {
        if (!genCode)
            return new SemanticValue();
        if (right.error != null)
            return right;
        if (left.error != null)
            return left;
        if (left.type != 'b' || right.type != 'b')
            return new SemanticValue { error = "  int / real not allowed in boolean ops" };

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
                return new SemanticValue { error = "  internal error" };
        }

        res.type = 'b';
        res.val = result.ToString();

        return res;
    }

    public static SemanticValue UnaryMinusOp(SemanticValue value)
    {
        if (!genCode)
            return new SemanticValue();
        if (value.error != null)
            return value;
        if (value.type == 'b')
            return new SemanticValue {error = "  bool not allowed in negation ops"};

        SemanticValue res = new SemanticValue();
        res.type = value.type;

        EmitCode("neg");

        if (value.type == 'i')
        {
            int v = int.Parse(value.val);
            res.val = (-v).ToString(CultureInfo.InvariantCulture);
        }
        else
        {
            double v = double.Parse(value.val, CultureInfo.InvariantCulture);
            res.val = (-v).ToString(CultureInfo.InvariantCulture);
        }

        return res;
    }

    public static SemanticValue NegationOp(SemanticValue value)
    {
        if(!genCode)
            return new SemanticValue();
        if (value.error != null)
            return value;
        if (value.type != 'b')
            return new SemanticValue { error = "  int / real not allowed in negation ops"};

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
}

class ErrorException : ApplicationException
{
    public ErrorException() { }
    public ErrorException(string msg) : base(msg) { }
    public ErrorException(string msg, Exception ex) : base(msg, ex) { }
}