%namespace GardensPoint

%YYSTYPE SemanticValue

%token Print Exit Endl Eof Error
%token Ident IntNumber RealNumber Boolean Type
%token Assign And Or Equal Diff Gt Lt Gte Lte Plus Minus Multiplies Divides Negation OpenPar ClosePar

%%

start     : start line { }
          | line { }
          ;

line      : Print exp Endl
            {
				if($2.error == null)
				{
					Console.Write("  Result:  {0}\n> ", $2.val);
				}
				else
				{
					if($2.exit)
					{
						Console.Write($2.error+"\n");
						Console.WriteLine("  Aborting");
						YYABORT;
					}
					else
					{
						Console.Write($2.error+"\n> ");
					}
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
					Console.Write(e.Message+"\n");
					Console.WriteLine("  Aborting");
					YYABORT;
				}
			}
          | Ident Assign exp Endl
            {
               try
               {
					if($3.error == null)
					{
						Compiler.Mem($1.val, $3);
						Console.Write("  OK\n> ");
					}
					else
					{
						if($3.exit)
						{
							Console.Write($3.error+"\n");
							Console.WriteLine("  Aborting");
							YYABORT;
						}
						else
						{
							Console.Write($3.error+"\n> ");
						}
					}
               }
               catch ( ErrorException e)
               {
                   Console.Write(e.Message+"\n");
				   Console.WriteLine("  Aborting");
				   YYABORT;
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

exp		  : exp And rel
				{ $$ = Compiler.BooleanOp($1,$3,Tokens.And); }
		  | exp Or rel
				{ $$ = Compiler.BooleanOp($1,$3,Tokens.Or); }
		  | rel
				{ $$ = $1; }
		  ;

rel		  : art Equal art
				{ $$ = Compiler.RelationalOp($1,$3,Tokens.Equal); }
		  | art Diff art
				{ $$ = Compiler.RelationalOp($1,$3,Tokens.Diff); }
		  | art Gt art
				{ $$ = Compiler.RelationalOp($1,$3,Tokens.Gt); }
		  | art Lt art
				{ $$ = Compiler.RelationalOp($1,$3,Tokens.Lt); }
		  | art Gte art
				{ $$ = Compiler.RelationalOp($1,$3,Tokens.Gte); }
		  | art Lte art
				{ $$ = Compiler.RelationalOp($1,$3,Tokens.Lte); }
		  | art
				{ $$ = $1; }
		  ;

art       : art Plus term
               { $$ = Compiler.AritmeticalOp($1,$3,Tokens.Plus); }
          | art Minus term
               { $$ = Compiler.AritmeticalOp($1,$3,Tokens.Minus); }
          | term
               { $$ = $1; }
          ;

term      : term Multiplies tmpfactor
               { $$ = Compiler.AritmeticalOp($1,$3,Tokens.Multiplies); }
          | term Divides tmpfactor
               { $$ = Compiler.AritmeticalOp($1,$3,Tokens.Divides); }
          | tmpfactor
               { $$ = $1; }
          ;

tmpfactor : factor
			   { $$ = $1; }
		  | Minus factor
			   { $$ = Compiler.UnaryMinusOp($2); }
		  | Negation tmpfactor
			   { $$ = Compiler.NegationOp($2); }
          ;

factor    : OpenPar exp ClosePar
               { $$ = $2; }
		  | Ident OpenPar art ClosePar
			   { $$ = Compiler.Function($1.val, $3); }
          | IntNumber
               { $$.val = $1.val.TrimStart('0') != "" ? $1.val.TrimStart('0') : "0" ; $$.type = 'i'; }
		  | RealNumber
               { $$.val = $1.val.TrimStart('0'); $$.type = 'r'; }
		  | Boolean
               { $$.val = $1.val; $$.type = 'b'; }
          | Ident
			   { $$ = Compiler.GetVariable($1.val); }
          ;

%%

public Parser(Scanner scanner) : base(scanner) { }