%namespace GardensPoint

%union
{
	public string      val;
	public SyntaxTree  st;
}

%token Print Exit Assign Plus Minus Multiplies Divides OpenPar ClosePar Endl Eof Error
%token <val> Ident IntNumber RealNumber Boolean Typ

%type <st> exp term factor

%%

start     : start line { }
          | line { }
          ;

line      : Print exp Endl
            {
               try
               {
                   Console.Write("  Result:  {0}\n> ",$2.Compute());
               }
               catch ( ErrorException e)
               {
                   Console.Write(e.Message+"\n> ");
               }
            }
		  | Typ Ident Endl
			{
				try
				{
					Compiler.Declare($1, $2);
					Console.Write("  OK\n> ");
				}
				catch(ErrorException e)
				{
					Console.Write(e.Message+"\n> ");
				}
			}
          | Ident Assign exp Endl
            {
               try
               {
                   Compiler.Mem($1, $3.Compute());
                   Console.Write("  OK\n> ");
               }
               catch ( ErrorException e)
               {
                   Console.Write(e.Message+"\n> ");
               }
            }
          | Exit Endl
            {
               Console.WriteLine("  OK, exited");
               YYACCEPT;
            }
          | error Endl
            {
               Console.Write("  syntax error\n> ");
               yyerrok();
            }
          | Eof
            {
               Console.WriteLine("  unexpected Eof, exited");
               YYACCEPT;
            }
          ;

exp       : exp Plus term
               { $$ = new AritmeticalOp($1,$3,Tokens.Plus); }
          | exp Minus term
               { $$ = new AritmeticalOp($1,$3,Tokens.Minus); }
          | term
               { $$ = $1; }
          ;

term      : term Multiplies factor
               { $$ = new AritmeticalOp($1,$3,Tokens.Multiplies); }
          | term Divides factor
               { $$ = new AritmeticalOp($1,$3,Tokens.Divides); }
          | factor
               { $$ = $1; }
          ;

factor    : OpenPar exp ClosePar
               { $$ = $2; }
          | IntNumber
               { $$ = new IntNumber(int.Parse($1)); }
		  | RealNumber
               { $$ = new RealNumber(double.Parse($1, CultureInfo.InvariantCulture)); }
		  | Boolean
               { $$ = new Boolean(bool.Parse($1)); }
          | Ident
               { $$ = new Ident($1); }
          ;

%%

public Parser(Scanner scanner) : base(scanner) { }