// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  PADZIKM
// DateTime: 2014-11-29 01:08:48
// UserName: Michal
// Input file <..\..\parser.y - 2014-11-29 01:08:36>

// options: lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace GardensPoint
{
public enum Tokens {error=2,EOF=3,Print=4,Exit=5,Ident=6,
    IntNumber=7,RealNumber=8,Boolean=9,Type=10,Assign=11,Plus=12,
    Minus=13,Multiplies=14,Divides=15,OpenPar=16,ClosePar=17,Endl=18,
    Eof=19,Error=20};

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
  private static Rule[] rules = new Rule[24];
  private static State[] states = new State[42];
  private static string[] nonTerms = new string[] {
      "start", "$accept", "line", "exp", "term", "factor", };

  static Parser() {
    states[0] = new State(new int[]{4,4,10,27,6,30,5,34,2,37,19,40},new int[]{-1,1,-3,41});
    states[1] = new State(new int[]{3,2,4,4,10,27,6,30,5,34,2,37,19,40},new int[]{-3,3});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(new int[]{16,11,6,18,7,24,8,25,9,26},new int[]{-4,5,-5,22,-6,23});
    states[5] = new State(new int[]{18,6,12,7,13,14});
    states[6] = new State(-4);
    states[7] = new State(new int[]{16,11,6,18,7,24,8,25,9,26},new int[]{-5,8,-6,23});
    states[8] = new State(new int[]{14,9,15,16,18,-12,12,-12,13,-12,17,-12});
    states[9] = new State(new int[]{16,11,6,18,7,24,8,25,9,26},new int[]{-6,10});
    states[10] = new State(-15);
    states[11] = new State(new int[]{16,11,6,18,7,24,8,25,9,26},new int[]{-4,12,-5,22,-6,23});
    states[12] = new State(new int[]{17,13,12,7,13,14});
    states[13] = new State(-18);
    states[14] = new State(new int[]{16,11,6,18,7,24,8,25,9,26},new int[]{-5,15,-6,23});
    states[15] = new State(new int[]{14,9,15,16,18,-13,12,-13,13,-13,17,-13});
    states[16] = new State(new int[]{16,11,6,18,7,24,8,25,9,26},new int[]{-6,17});
    states[17] = new State(-16);
    states[18] = new State(new int[]{16,19,14,-23,15,-23,18,-23,12,-23,13,-23,17,-23});
    states[19] = new State(new int[]{16,11,6,18,7,24,8,25,9,26},new int[]{-4,20,-5,22,-6,23});
    states[20] = new State(new int[]{17,21,12,7,13,14});
    states[21] = new State(-19);
    states[22] = new State(new int[]{14,9,15,16,18,-14,12,-14,13,-14,17,-14});
    states[23] = new State(-17);
    states[24] = new State(-20);
    states[25] = new State(-21);
    states[26] = new State(-22);
    states[27] = new State(new int[]{6,28});
    states[28] = new State(new int[]{18,29});
    states[29] = new State(-5);
    states[30] = new State(new int[]{11,31});
    states[31] = new State(new int[]{16,11,6,18,7,24,8,25,9,26},new int[]{-4,32,-5,22,-6,23});
    states[32] = new State(new int[]{18,33,12,7,13,14});
    states[33] = new State(-6);
    states[34] = new State(new int[]{18,35,19,36});
    states[35] = new State(-7);
    states[36] = new State(-8);
    states[37] = new State(new int[]{18,38,19,39});
    states[38] = new State(-9);
    states[39] = new State(-10);
    states[40] = new State(-11);
    states[41] = new State(-3);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{-1,-3});
    rules[3] = new Rule(-1, new int[]{-3});
    rules[4] = new Rule(-3, new int[]{4,-4,18});
    rules[5] = new Rule(-3, new int[]{10,6,18});
    rules[6] = new Rule(-3, new int[]{6,11,-4,18});
    rules[7] = new Rule(-3, new int[]{5,18});
    rules[8] = new Rule(-3, new int[]{5,19});
    rules[9] = new Rule(-3, new int[]{2,18});
    rules[10] = new Rule(-3, new int[]{2,19});
    rules[11] = new Rule(-3, new int[]{19});
    rules[12] = new Rule(-4, new int[]{-4,12,-5});
    rules[13] = new Rule(-4, new int[]{-4,13,-5});
    rules[14] = new Rule(-4, new int[]{-5});
    rules[15] = new Rule(-5, new int[]{-5,14,-6});
    rules[16] = new Rule(-5, new int[]{-5,15,-6});
    rules[17] = new Rule(-5, new int[]{-6});
    rules[18] = new Rule(-6, new int[]{16,-4,17});
    rules[19] = new Rule(-6, new int[]{6,16,-4,17});
    rules[20] = new Rule(-6, new int[]{7});
    rules[21] = new Rule(-6, new int[]{8});
    rules[22] = new Rule(-6, new int[]{9});
    rules[23] = new Rule(-6, new int[]{6});
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
#line 9 "..\..\parser.y"
                       { }
#line default
        break;
      case 3: // start -> line
#line 10 "..\..\parser.y"
                 { }
#line default
        break;
      case 4: // line -> Print, exp, Endl
#line 14 "..\..\parser.y"
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
#line 32 "..\..\parser.y"
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
#line 44 "..\..\parser.y"
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
#line 63 "..\..\parser.y"
            {
               Console.WriteLine("  OK, exited");
               YYAccept();
            }
#line default
        break;
      case 8: // line -> Exit, Eof
#line 68 "..\..\parser.y"
            {
               Console.WriteLine("  OK, exited");
               YYAccept();
            }
#line default
        break;
      case 9: // line -> error, Endl
#line 73 "..\..\parser.y"
            {
               Console.Write("  syntax error\n> ");
               yyerrok();
            }
#line default
        break;
      case 10: // line -> error, Eof
#line 78 "..\..\parser.y"
            {
               Console.Write("  syntax error\n> ");
               yyerrok();
               YYAccept();
            }
#line default
        break;
      case 11: // line -> Eof
#line 84 "..\..\parser.y"
            {
               Console.WriteLine("  unexpected Eof, exited");
               YYAccept();
            }
#line default
        break;
      case 12: // exp -> exp, Plus, term
#line 91 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Plus); }
#line default
        break;
      case 13: // exp -> exp, Minus, term
#line 93 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Minus); }
#line default
        break;
      case 14: // exp -> term
#line 95 "..\..\parser.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 15: // term -> term, Multiplies, factor
#line 99 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Multiplies); }
#line default
        break;
      case 16: // term -> term, Divides, factor
#line 101 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Divides); }
#line default
        break;
      case 17: // term -> factor
#line 103 "..\..\parser.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 18: // factor -> OpenPar, exp, ClosePar
#line 107 "..\..\parser.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-2]; }
#line default
        break;
      case 19: // factor -> Ident, OpenPar, exp, ClosePar
#line 109 "..\..\parser.y"
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
      case 20: // factor -> IntNumber
#line 120 "..\..\parser.y"
               { CurrentSemanticValue.val = ValueStack[ValueStack.Depth-1].val; CurrentSemanticValue.type = 'i'; }
#line default
        break;
      case 21: // factor -> RealNumber
#line 122 "..\..\parser.y"
               { CurrentSemanticValue.val = ValueStack[ValueStack.Depth-1].val; CurrentSemanticValue.type = 'r'; }
#line default
        break;
      case 22: // factor -> Boolean
#line 124 "..\..\parser.y"
               { CurrentSemanticValue.val = ValueStack[ValueStack.Depth-1].val; CurrentSemanticValue.type = 'b'; }
#line default
        break;
      case 23: // factor -> Ident
#line 126 "..\..\parser.y"
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

#line 142 "..\..\parser.y"

public Parser(Scanner scanner) : base(scanner) { }
#line default
}
}
