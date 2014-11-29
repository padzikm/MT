// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  PADZIKM
// DateTime: 2014-11-29 11:12:52
// UserName: Michal
// Input file <..\..\parser.y - 2014-11-29 11:12:29>

// options: lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace GardensPoint
{
public enum Tokens {error=2,EOF=3,Print=4,Exit=5,Endl=6,
    Eof=7,Error=8,Ident=9,IntNumber=10,RealNumber=11,Boolean=12,
    Type=13,Assign=14,Equal=15,Diff=16,Gt=17,Lt=18,
    Gte=19,Lte=20,Plus=21,Minus=22,Multiplies=23,Divides=24,
    OpenPar=25,ClosePar=26};

// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<SemanticValue,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public SemanticValue yylval;
  public LexLocation yylloc;
  public ScanObj( int t, SemanticValue val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class Parser: ShiftReduceParser<SemanticValue, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[31];
  private static State[] states = new State[55];
  private static string[] nonTerms = new string[] {
      "start", "$accept", "line", "exp", "art", "term", "factor", };

  static Parser() {
    states[0] = new State(new int[]{4,4,13,40,9,43,5,47,2,50,7,53},new int[]{-1,1,-3,54});
    states[1] = new State(new int[]{3,2,4,4,13,40,9,43,5,47,2,50,7,53},new int[]{-3,3});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-4,5,-5,7,-6,17,-7,26});
    states[5] = new State(new int[]{6,6});
    states[6] = new State(-4);
    states[7] = new State(new int[]{15,8,21,10,22,24,16,30,17,32,18,34,19,36,20,38,6,-18,26,-18});
    states[8] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-5,9,-6,17,-7,26});
    states[9] = new State(new int[]{21,10,22,24,6,-12,26,-12});
    states[10] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-6,11,-7,26});
    states[11] = new State(new int[]{23,12,24,18,15,-19,21,-19,22,-19,16,-19,17,-19,18,-19,19,-19,20,-19,6,-19,26,-19});
    states[12] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-7,13});
    states[13] = new State(-22);
    states[14] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-4,15,-5,7,-6,17,-7,26});
    states[15] = new State(new int[]{26,16});
    states[16] = new State(-25);
    states[17] = new State(new int[]{23,12,24,18,15,-21,21,-21,22,-21,16,-21,17,-21,18,-21,19,-21,20,-21,6,-21,26,-21});
    states[18] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-7,19});
    states[19] = new State(-23);
    states[20] = new State(new int[]{25,21,23,-30,24,-30,15,-30,21,-30,22,-30,16,-30,17,-30,18,-30,19,-30,20,-30,6,-30,26,-30});
    states[21] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-5,22,-6,17,-7,26});
    states[22] = new State(new int[]{26,23,21,10,22,24});
    states[23] = new State(-26);
    states[24] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-6,25,-7,26});
    states[25] = new State(new int[]{23,12,24,18,15,-20,21,-20,22,-20,16,-20,17,-20,18,-20,19,-20,20,-20,6,-20,26,-20});
    states[26] = new State(-24);
    states[27] = new State(-27);
    states[28] = new State(-28);
    states[29] = new State(-29);
    states[30] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-5,31,-6,17,-7,26});
    states[31] = new State(new int[]{21,10,22,24,6,-13,26,-13});
    states[32] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-5,33,-6,17,-7,26});
    states[33] = new State(new int[]{21,10,22,24,6,-14,26,-14});
    states[34] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-5,35,-6,17,-7,26});
    states[35] = new State(new int[]{21,10,22,24,6,-15,26,-15});
    states[36] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-5,37,-6,17,-7,26});
    states[37] = new State(new int[]{21,10,22,24,6,-16,26,-16});
    states[38] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-5,39,-6,17,-7,26});
    states[39] = new State(new int[]{21,10,22,24,6,-17,26,-17});
    states[40] = new State(new int[]{9,41});
    states[41] = new State(new int[]{6,42});
    states[42] = new State(-5);
    states[43] = new State(new int[]{14,44});
    states[44] = new State(new int[]{25,14,9,20,10,27,11,28,12,29},new int[]{-4,45,-5,7,-6,17,-7,26});
    states[45] = new State(new int[]{6,46});
    states[46] = new State(-6);
    states[47] = new State(new int[]{6,48,7,49});
    states[48] = new State(-7);
    states[49] = new State(-8);
    states[50] = new State(new int[]{6,51,7,52});
    states[51] = new State(-9);
    states[52] = new State(-10);
    states[53] = new State(-11);
    states[54] = new State(-3);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{-1,-3});
    rules[3] = new Rule(-1, new int[]{-3});
    rules[4] = new Rule(-3, new int[]{4,-4,6});
    rules[5] = new Rule(-3, new int[]{13,9,6});
    rules[6] = new Rule(-3, new int[]{9,14,-4,6});
    rules[7] = new Rule(-3, new int[]{5,6});
    rules[8] = new Rule(-3, new int[]{5,7});
    rules[9] = new Rule(-3, new int[]{2,6});
    rules[10] = new Rule(-3, new int[]{2,7});
    rules[11] = new Rule(-3, new int[]{7});
    rules[12] = new Rule(-4, new int[]{-5,15,-5});
    rules[13] = new Rule(-4, new int[]{-5,16,-5});
    rules[14] = new Rule(-4, new int[]{-5,17,-5});
    rules[15] = new Rule(-4, new int[]{-5,18,-5});
    rules[16] = new Rule(-4, new int[]{-5,19,-5});
    rules[17] = new Rule(-4, new int[]{-5,20,-5});
    rules[18] = new Rule(-4, new int[]{-5});
    rules[19] = new Rule(-5, new int[]{-5,21,-6});
    rules[20] = new Rule(-5, new int[]{-5,22,-6});
    rules[21] = new Rule(-5, new int[]{-6});
    rules[22] = new Rule(-6, new int[]{-6,23,-7});
    rules[23] = new Rule(-6, new int[]{-6,24,-7});
    rules[24] = new Rule(-6, new int[]{-7});
    rules[25] = new Rule(-7, new int[]{25,-4,26});
    rules[26] = new Rule(-7, new int[]{9,25,-5,26});
    rules[27] = new Rule(-7, new int[]{10});
    rules[28] = new Rule(-7, new int[]{11});
    rules[29] = new Rule(-7, new int[]{12});
    rules[30] = new Rule(-7, new int[]{9});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // start -> start, line
#line 11 "..\..\parser.y"
                       { }
#line default
        break;
      case 3: // start -> line
#line 12 "..\..\parser.y"
                 { }
#line default
        break;
      case 4: // line -> Print, exp, Endl
#line 16 "..\..\parser.y"
            {
               try
               {
					if(ValueStack[ValueStack.Depth-2].error == null)
					{
						Console.Write("  Result:  {0}\n> ", ValueStack[ValueStack.Depth-2].val);
					}
					else if(!ValueStack[ValueStack.Depth-2].errorPrinted)
					{
						Console.Write(ValueStack[ValueStack.Depth-2].error+"\n> ");
					}
               }
               catch ( ErrorException e)
               {
                   Console.Write(e.Message+"\n> ");
               }
            }
#line default
        break;
      case 5: // line -> Type, Ident, Endl
#line 34 "..\..\parser.y"
   {
				try
				{
					Compiler.Declare(ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-2].val);
					Console.Write("  OK\n> ");
				}
				catch(ErrorException e)
				{
					Console.Write(e.Message+"\n> ");
				}
			}
#line default
        break;
      case 6: // line -> Ident, Assign, exp, Endl
#line 46 "..\..\parser.y"
            {
               try
               {
					if(ValueStack[ValueStack.Depth-2].error == null)
					{
						Compiler.Mem(ValueStack[ValueStack.Depth-4].val, ValueStack[ValueStack.Depth-2]);
						Console.Write("  OK\n> ");
					}
					else if(!ValueStack[ValueStack.Depth-2].errorPrinted)
					{
						Console.Write(ValueStack[ValueStack.Depth-2].error+"\n> ");
					}
               }
               catch ( ErrorException e)
               {
                   Console.Write(e.Message+"\n> ");
               }
            }
#line default
        break;
      case 7: // line -> Exit, Endl
#line 65 "..\..\parser.y"
            {
               Console.WriteLine("  OK, exited");
               YYAccept();
            }
#line default
        break;
      case 8: // line -> Exit, Eof
#line 70 "..\..\parser.y"
            {
               Console.WriteLine("  OK, exited");
               YYAccept();
            }
#line default
        break;
      case 9: // line -> error, Endl
#line 75 "..\..\parser.y"
            {
               Console.Write("  syntax error\n> ");
               yyerrok();
            }
#line default
        break;
      case 10: // line -> error, Eof
#line 80 "..\..\parser.y"
            {
               Console.Write("  syntax error\n> ");
               yyerrok();
               YYAccept();
            }
#line default
        break;
      case 11: // line -> Eof
#line 86 "..\..\parser.y"
            {
               Console.WriteLine("  unexpected Eof, exited");
               YYAccept();
            }
#line default
        break;
      case 12: // exp -> art, Equal, art
#line 93 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Equal); }
#line default
        break;
      case 13: // exp -> art, Diff, art
#line 95 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Diff); }
#line default
        break;
      case 14: // exp -> art, Gt, art
#line 97 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Gt); }
#line default
        break;
      case 15: // exp -> art, Lt, art
#line 99 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Lt); }
#line default
        break;
      case 16: // exp -> art, Gte, art
#line 101 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Gte); }
#line default
        break;
      case 17: // exp -> art, Lte, art
#line 103 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Lte); }
#line default
        break;
      case 18: // exp -> art
#line 105 "..\..\parser.y"
    { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 19: // art -> art, Plus, term
#line 109 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Plus); }
#line default
        break;
      case 20: // art -> art, Minus, term
#line 111 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Minus); }
#line default
        break;
      case 21: // art -> term
#line 113 "..\..\parser.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 22: // term -> term, Multiplies, factor
#line 117 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Multiplies); }
#line default
        break;
      case 23: // term -> term, Divides, factor
#line 119 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Divides); }
#line default
        break;
      case 24: // term -> factor
#line 121 "..\..\parser.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 25: // factor -> OpenPar, exp, ClosePar
#line 125 "..\..\parser.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-2]; }
#line default
        break;
      case 26: // factor -> Ident, OpenPar, art, ClosePar
#line 127 "..\..\parser.y"
   {
				if(ValueStack[ValueStack.Depth-2].error == null)
				{
					CurrentSemanticValue = Compiler.Function(ValueStack[ValueStack.Depth-4].val, ValueStack[ValueStack.Depth-2]);
				}
				else
				{
					CurrentSemanticValue = ValueStack[ValueStack.Depth-2];
				}
			}
#line default
        break;
      case 27: // factor -> IntNumber
#line 138 "..\..\parser.y"
               { CurrentSemanticValue.val = ValueStack[ValueStack.Depth-1].val.TrimStart('0'); CurrentSemanticValue.type = 'i'; }
#line default
        break;
      case 28: // factor -> RealNumber
#line 140 "..\..\parser.y"
               { CurrentSemanticValue.val = ValueStack[ValueStack.Depth-1].val.TrimStart('0'); CurrentSemanticValue.type = 'r'; }
#line default
        break;
      case 29: // factor -> Boolean
#line 142 "..\..\parser.y"
               { CurrentSemanticValue.val = ValueStack[ValueStack.Depth-1].val; CurrentSemanticValue.type = 'b'; }
#line default
        break;
      case 30: // factor -> Ident
#line 144 "..\..\parser.y"
            { 
			   try
			   {
					CurrentSemanticValue.val = Compiler.GetValue(ValueStack[ValueStack.Depth-1].val);
					CurrentSemanticValue.type = Compiler.GetType(ValueStack[ValueStack.Depth-1].val);
			   }
			   catch(ErrorException e)
			   {
					CurrentSemanticValue.error = e.Message;
					CurrentSemanticValue.errorPrinted = true;
					Console.Write(e.Message+"\n> ");
			   }
			}
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 160 "..\..\parser.y"

public Parser(Scanner scanner) : base(scanner) { }
#line default
}
}
