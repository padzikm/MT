%namespace GardensPoint

%YYSTYPE SemanticValue

%token Print Exit Ident IntNumber RealNumber Boolean Type Assign Plus Minus Multiplies Divides OpenPar ClosePar Endl Eof Error

%%

start     : start line { }
          | line { }
          ;

line      : Print exp Endl
            {
               try
               {
                   Console.Write("  Result:  {0}\n> ", $2.val);
               }
               catch ( ErrorException e)
               {
                   Console.Write(e.Message+"\n> ");
               }
            }
		  | Type Ident Endl
			{
				try
				{
					Compiler.Declare($1.type, $2.val);
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
                   Compiler.Mem($1.val, $3);
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
		  | Exit Eof
            {
               Console.WriteLine("  OK, exited");
               YYACCEPT;
            }
          | error Endl
            {
               Console.Write("  syntax error\n> ");
               yyerrok();
            }
		  | error Eof
            {
               Console.Write("  syntax error\n> ");
               yyerrok();
               YYACCEPT;
            }
          | Eof
            {
               Console.WriteLine("  unexpected Eof, exited");
               YYACCEPT;
            }
          ;

exp       : exp Plus term
               { $$ = Compiler.AritmeticalOp($1,$3,Tokens.Plus); }
          | exp Minus term
               { $$ = Compiler.AritmeticalOp($1,$3,Tokens.Minus); }
          | term
               { $$ = $1; }
          ;

term      : term Multiplies factor
               { $$ = Compiler.AritmeticalOp($1,$3,Tokens.Multiplies); }
          | term Divides factor
               { $$ = Compiler.AritmeticalOp($1,$3,Tokens.Divides); }
          | factor
               { $$ = $1; }
          ;

factor    : OpenPar exp ClosePar
               { $$ = $2; }
          | IntNumber
               { $$.val = $1.val; $$.type = 'i'; }
		  | RealNumber
               { $$.val = $1.val; $$.type = 'r'; }
		  | Boolean
               { $$.val = $1.val; $$.type = 'b'; }
          | Ident
               { $$.val = Compiler.GetValue($1.val); $$.type = Compiler.GetType($1.val); }
          ;

%%

public Parser(Scanner scanner) : base(scanner) { }