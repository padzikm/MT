// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  PADZIKM
// DateTime: 2014-11-30 22:59:08
// UserName: Michal
// Input file <..\..\parser.y - 2014-11-30 22:58:58>

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
    Type=13,Assign=14,And=15,Or=16,Equal=17,Diff=18,
    Gt=19,Lt=20,Gte=21,Lte=22,Plus=23,Minus=24,
    Multiplies=25,Divides=26,Negation=27,OpenPar=28,ClosePar=29};

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
  private static Rule[] rules = new Rule[41];
  private static State[] states = new State[69];
  private static string[] nonTerms = new string[] {
      "start", "$accept", "line", "Anon@1", "exp", "Anon@2", "Anon@3", "rel", 
      "Anon@4", "art", "term", "tmpfactor", "factor", };

  static Parser() {
    states[0] = new State(new int[]{4,4,13,53,9,56,5,61,2,64,7,67},new int[]{-1,1,-3,68});
    states[1] = new State(new int[]{3,2,4,4,13,53,9,56,5,61,2,64,7,67},new int[]{-3,3});
    states[2] = new State(-1);
    states[3] = new State(-2);
    states[4] = new State(-4,new int[]{-4,5});
    states[5] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-5,6,-8,42,-10,11,-11,25,-12,34,-13,18});
    states[6] = new State(new int[]{6,7,15,-14,16,-16},new int[]{-7,8,-9,22});
    states[7] = new State(-5);
    states[8] = new State(new int[]{15,9});
    states[9] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-8,10,-10,11,-11,25,-12,34,-13,18});
    states[10] = new State(-15);
    states[11] = new State(new int[]{17,12,23,14,24,32,18,43,19,45,20,47,21,49,22,51,6,-25,15,-25,16,-25,29,-25});
    states[12] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-10,13,-11,25,-12,34,-13,18});
    states[13] = new State(new int[]{23,14,24,32,6,-19,15,-19,16,-19,29,-19});
    states[14] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-11,15,-12,34,-13,18});
    states[15] = new State(new int[]{25,16,26,26,17,-26,23,-26,24,-26,18,-26,19,-26,20,-26,21,-26,22,-26,6,-26,15,-26,16,-26,29,-26});
    states[16] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-12,17,-13,18});
    states[17] = new State(-29);
    states[18] = new State(-32);
    states[19] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-5,20,-8,42,-10,11,-11,25,-12,34,-13,18});
    states[20] = new State(new int[]{29,21,15,-14,16,-16},new int[]{-7,8,-9,22});
    states[21] = new State(-35);
    states[22] = new State(new int[]{16,23});
    states[23] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-8,24,-10,11,-11,25,-12,34,-13,18});
    states[24] = new State(-17);
    states[25] = new State(new int[]{25,16,26,26,17,-28,23,-28,24,-28,18,-28,19,-28,20,-28,21,-28,22,-28,6,-28,15,-28,16,-28,29,-28});
    states[26] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-12,27,-13,18});
    states[27] = new State(-30);
    states[28] = new State(new int[]{28,29,25,-40,26,-40,17,-40,23,-40,24,-40,18,-40,19,-40,20,-40,21,-40,22,-40,6,-40,15,-40,16,-40,29,-40});
    states[29] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-10,30,-11,25,-12,34,-13,18});
    states[30] = new State(new int[]{29,31,23,14,24,32});
    states[31] = new State(-36);
    states[32] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-11,33,-12,34,-13,18});
    states[33] = new State(new int[]{25,16,26,26,17,-27,23,-27,24,-27,18,-27,19,-27,20,-27,21,-27,22,-27,6,-27,15,-27,16,-27,29,-27});
    states[34] = new State(-31);
    states[35] = new State(-37);
    states[36] = new State(-38);
    states[37] = new State(-39);
    states[38] = new State(new int[]{28,19,9,28,10,35,11,36,12,37},new int[]{-13,39});
    states[39] = new State(-33);
    states[40] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-12,41,-13,18});
    states[41] = new State(-34);
    states[42] = new State(-18);
    states[43] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-10,44,-11,25,-12,34,-13,18});
    states[44] = new State(new int[]{23,14,24,32,6,-20,15,-20,16,-20,29,-20});
    states[45] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-10,46,-11,25,-12,34,-13,18});
    states[46] = new State(new int[]{23,14,24,32,6,-21,15,-21,16,-21,29,-21});
    states[47] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-10,48,-11,25,-12,34,-13,18});
    states[48] = new State(new int[]{23,14,24,32,6,-22,15,-22,16,-22,29,-22});
    states[49] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-10,50,-11,25,-12,34,-13,18});
    states[50] = new State(new int[]{23,14,24,32,6,-23,15,-23,16,-23,29,-23});
    states[51] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-10,52,-11,25,-12,34,-13,18});
    states[52] = new State(new int[]{23,14,24,32,6,-24,15,-24,16,-24,29,-24});
    states[53] = new State(new int[]{9,54});
    states[54] = new State(new int[]{6,55});
    states[55] = new State(-6);
    states[56] = new State(new int[]{14,57});
    states[57] = new State(-7,new int[]{-6,58});
    states[58] = new State(new int[]{28,19,9,28,10,35,11,36,12,37,24,38,27,40},new int[]{-5,59,-8,42,-10,11,-11,25,-12,34,-13,18});
    states[59] = new State(new int[]{6,60,15,-14,16,-16},new int[]{-7,8,-9,22});
    states[60] = new State(-8);
    states[61] = new State(new int[]{6,62,7,63});
    states[62] = new State(-9);
    states[63] = new State(-10);
    states[64] = new State(new int[]{6,65,7,66});
    states[65] = new State(-11);
    states[66] = new State(-12);
    states[67] = new State(-13);
    states[68] = new State(-3);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{-1,-3});
    rules[3] = new Rule(-1, new int[]{-3});
    rules[4] = new Rule(-4, new int[]{});
    rules[5] = new Rule(-3, new int[]{4,-4,-5,6});
    rules[6] = new Rule(-3, new int[]{13,9,6});
    rules[7] = new Rule(-6, new int[]{});
    rules[8] = new Rule(-3, new int[]{9,14,-6,-5,6});
    rules[9] = new Rule(-3, new int[]{5,6});
    rules[10] = new Rule(-3, new int[]{5,7});
    rules[11] = new Rule(-3, new int[]{2,6});
    rules[12] = new Rule(-3, new int[]{2,7});
    rules[13] = new Rule(-3, new int[]{7});
    rules[14] = new Rule(-7, new int[]{});
    rules[15] = new Rule(-5, new int[]{-5,-7,15,-8});
    rules[16] = new Rule(-9, new int[]{});
    rules[17] = new Rule(-5, new int[]{-5,-9,16,-8});
    rules[18] = new Rule(-5, new int[]{-8});
    rules[19] = new Rule(-8, new int[]{-10,17,-10});
    rules[20] = new Rule(-8, new int[]{-10,18,-10});
    rules[21] = new Rule(-8, new int[]{-10,19,-10});
    rules[22] = new Rule(-8, new int[]{-10,20,-10});
    rules[23] = new Rule(-8, new int[]{-10,21,-10});
    rules[24] = new Rule(-8, new int[]{-10,22,-10});
    rules[25] = new Rule(-8, new int[]{-10});
    rules[26] = new Rule(-10, new int[]{-10,23,-11});
    rules[27] = new Rule(-10, new int[]{-10,24,-11});
    rules[28] = new Rule(-10, new int[]{-11});
    rules[29] = new Rule(-11, new int[]{-11,25,-12});
    rules[30] = new Rule(-11, new int[]{-11,26,-12});
    rules[31] = new Rule(-11, new int[]{-12});
    rules[32] = new Rule(-12, new int[]{-13});
    rules[33] = new Rule(-12, new int[]{24,-13});
    rules[34] = new Rule(-12, new int[]{27,-12});
    rules[35] = new Rule(-13, new int[]{28,-5,29});
    rules[36] = new Rule(-13, new int[]{9,28,-10,29});
    rules[37] = new Rule(-13, new int[]{10});
    rules[38] = new Rule(-13, new int[]{11});
    rules[39] = new Rule(-13, new int[]{12});
    rules[40] = new Rule(-13, new int[]{9});
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
                       { ++lineno; }
#line default
        break;
      case 3: // start -> line
#line 12 "..\..\parser.y"
                 { ++lineno; }
#line default
        break;
      case 4: // Anon@1 -> /* empty */
#line 15 "..\..\parser.y"
                  { Compiler.EmitCode("// linia {0,3} :  "+Compiler.sourceCode[lineno-1],lineno); }
#line default
        break;
      case 5: // line -> Print, Anon@1, exp, Endl
#line 16 "..\..\parser.y"
            {
				if(ValueStack[ValueStack.Depth-2].error == null)
				{
					Compiler.Print(ValueStack[ValueStack.Depth-2]);
					Compiler.EmitCode("");
				}
				else
				{
					Compiler.errors[lineno] = ValueStack[ValueStack.Depth-2].error;
				}
            }
#line default
        break;
      case 6: // line -> Type, Ident, Endl
#line 28 "..\..\parser.y"
   {
				try
				{
					Compiler.EmitCode("// linia {0,3} :  "+ Compiler.sourceCode[lineno-1],lineno);
					Compiler.Declare(ValueStack[ValueStack.Depth-3].type, ValueStack[ValueStack.Depth-2].val);
					Compiler.EmitCode("");
				}
				catch(ErrorException e)
				{
					Compiler.errors[lineno] = e.Message;
				}
			}
#line default
        break;
      case 7: // Anon@2 -> /* empty */
#line 40 "..\..\parser.y"
                         { Compiler.EmitCode("// linia {0,3} :  "+ Compiler.sourceCode[lineno-1],lineno); }
#line default
        break;
      case 8: // line -> Ident, Assign, Anon@2, exp, Endl
#line 41 "..\..\parser.y"
            {
               try
               {
					if(ValueStack[ValueStack.Depth-2].error == null)
					{
						Compiler.Mem(ValueStack[ValueStack.Depth-5].val, ValueStack[ValueStack.Depth-2]);
						Compiler.EmitCode("");
					}
					else
					{
						Compiler.errors[lineno] = ValueStack[ValueStack.Depth-2].error;
					}
               }
               catch ( ErrorException e)
               {
					Compiler.errors[lineno] = e.Message;
               }
            }
#line default
        break;
      case 9: // line -> Exit, Endl
#line 60 "..\..\parser.y"
            {
               YYAccept();
            }
#line default
        break;
      case 10: // line -> Exit, Eof
#line 64 "..\..\parser.y"
            {
               YYAccept();
            }
#line default
        break;
      case 11: // line -> error, Endl
#line 68 "..\..\parser.y"
            {
               Compiler.errors[lineno] = "  syntax error";
            }
#line default
        break;
      case 12: // line -> error, Eof
#line 72 "..\..\parser.y"
            {
			   Compiler.errors[lineno] = "  syntax error";
               yyerrok();
               YYAccept();
            }
#line default
        break;
      case 13: // line -> Eof
#line 78 "..\..\parser.y"
            {
               YYAccept();
            }
#line default
        break;
      case 14: // Anon@3 -> /* empty */
#line 83 "..\..\parser.y"
             { if(ValueStack[ValueStack.Depth-1].error == null && !bool.Parse(ValueStack[ValueStack.Depth-1].val)){ Compiler.genCode = false; }}
#line default
        break;
      case 15: // exp -> exp, Anon@3, And, rel
#line 84 "..\..\parser.y"
    {
					if(ValueStack[ValueStack.Depth-4].error == null && !bool.Parse(ValueStack[ValueStack.Depth-4].val))
					{
						CurrentSemanticValue = ValueStack[ValueStack.Depth-4];
						Compiler.genCode = true;
					}
					else
					{
						CurrentSemanticValue = Compiler.LogicalOp(ValueStack[ValueStack.Depth-4],ValueStack[ValueStack.Depth-1],Tokens.And); 
					}
				}
#line default
        break;
      case 16: // Anon@4 -> /* empty */
#line 95 "..\..\parser.y"
          { if(ValueStack[ValueStack.Depth-1].error == null && bool.Parse(ValueStack[ValueStack.Depth-1].val)){ Compiler.genCode = false; }}
#line default
        break;
      case 17: // exp -> exp, Anon@4, Or, rel
#line 96 "..\..\parser.y"
    {
					if(ValueStack[ValueStack.Depth-4].error == null && bool.Parse(ValueStack[ValueStack.Depth-4].val))
					{
						CurrentSemanticValue = ValueStack[ValueStack.Depth-4];
						Compiler.genCode = true;
					}
					else
					{
						CurrentSemanticValue = Compiler.LogicalOp(ValueStack[ValueStack.Depth-4],ValueStack[ValueStack.Depth-1],Tokens.Or); 
					}
				}
#line default
        break;
      case 18: // exp -> rel
#line 108 "..\..\parser.y"
    { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 19: // rel -> art, Equal, art
#line 112 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Equal); }
#line default
        break;
      case 20: // rel -> art, Diff, art
#line 114 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Diff); }
#line default
        break;
      case 21: // rel -> art, Gt, art
#line 116 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Gt); }
#line default
        break;
      case 22: // rel -> art, Lt, art
#line 118 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Lt); }
#line default
        break;
      case 23: // rel -> art, Gte, art
#line 120 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Gte); }
#line default
        break;
      case 24: // rel -> art, Lte, art
#line 122 "..\..\parser.y"
    { CurrentSemanticValue = Compiler.RelationalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Lte); }
#line default
        break;
      case 25: // rel -> art
#line 124 "..\..\parser.y"
    { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 26: // art -> art, Plus, term
#line 128 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Plus); }
#line default
        break;
      case 27: // art -> art, Minus, term
#line 130 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Minus); }
#line default
        break;
      case 28: // art -> term
#line 132 "..\..\parser.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 29: // term -> term, Multiplies, tmpfactor
#line 136 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Multiplies); }
#line default
        break;
      case 30: // term -> term, Divides, tmpfactor
#line 138 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.AritmeticalOp(ValueStack[ValueStack.Depth-3],ValueStack[ValueStack.Depth-1],Tokens.Divides); }
#line default
        break;
      case 31: // term -> tmpfactor
#line 140 "..\..\parser.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 32: // tmpfactor -> factor
#line 144 "..\..\parser.y"
      { CurrentSemanticValue = ValueStack[ValueStack.Depth-1]; }
#line default
        break;
      case 33: // tmpfactor -> Minus, factor
#line 146 "..\..\parser.y"
      { CurrentSemanticValue = Compiler.UnaryMinusOp(ValueStack[ValueStack.Depth-1]); }
#line default
        break;
      case 34: // tmpfactor -> Negation, tmpfactor
#line 148 "..\..\parser.y"
      { CurrentSemanticValue = Compiler.NegationOp(ValueStack[ValueStack.Depth-1]); }
#line default
        break;
      case 35: // factor -> OpenPar, exp, ClosePar
#line 152 "..\..\parser.y"
               { CurrentSemanticValue = ValueStack[ValueStack.Depth-2]; }
#line default
        break;
      case 36: // factor -> Ident, OpenPar, art, ClosePar
#line 154 "..\..\parser.y"
      { CurrentSemanticValue = Compiler.Function(ValueStack[ValueStack.Depth-4].val, ValueStack[ValueStack.Depth-2]); }
#line default
        break;
      case 37: // factor -> IntNumber
#line 156 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.Create(ValueStack[ValueStack.Depth-1].val, 'i'); }
#line default
        break;
      case 38: // factor -> RealNumber
#line 158 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.Create(ValueStack[ValueStack.Depth-1].val, 'r'); }
#line default
        break;
      case 39: // factor -> Boolean
#line 160 "..\..\parser.y"
               { CurrentSemanticValue = Compiler.Create(ValueStack[ValueStack.Depth-1].val, 'b'); }
#line default
        break;
      case 40: // factor -> Ident
#line 162 "..\..\parser.y"
      { CurrentSemanticValue = Compiler.GetVariable(ValueStack[ValueStack.Depth-1].val); }
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

#line 166 "..\..\parser.y"

int lineno = 1;

public Parser(Scanner scanner) : base(scanner) { }
#line default
}
}
