%namespace GardensPoint

%YYSTYPE SemanticValue

%token Print Exit Endl Eof Error
%token Ident IntNumber RealNumber Boolean Type
%token Assign And Or Equal Diff Gt Lt Gte Lte Plus Minus Multiplies Divides Negation OpenPar ClosePar

%%

start     : start line { ++lineno; }
          | line { ++lineno; }
          ;

line      : Print { Compiler.EmitCode("// linia {0,3} :  "+Compiler.sourceCode[lineno-1],lineno); } exp Endl
            {
				if($3.error == null)
				{
					Compiler.Print($3);
					Compiler.EmitCode("");
				}
				else
				{
					Compiler.errors[lineno] = $3.error;
				}
            }
		  | Type Ident Endl
			{
				try
				{
					Compiler.EmitCode("// linia {0,3} :  "+ Compiler.sourceCode[lineno-1],lineno);
					Compiler.Declare($1.type, $2.val);
					Compiler.EmitCode("");
				}
				catch(ErrorException e)
				{
					Compiler.errors[lineno] = e.Message;
				}
			}
          | Ident Assign { Compiler.EmitCode("// linia {0,3} :  "+ Compiler.sourceCode[lineno-1],lineno); } exp Endl
            {
               try
               {
					if($4.error == null)
					{
						Compiler.Mem($1.val, $4);
						Compiler.EmitCode("");
					}
					else
					{
						Compiler.errors[lineno] = $4.error;
					}
               }
               catch ( ErrorException e)
               {
					Compiler.errors[lineno] = e.Message;
               }
            }
          | Exit Endl
            {
               YYACCEPT;
            }
		  | Exit Eof
            {
               YYACCEPT;
            }
          | error Endl
            {
               Compiler.errors[lineno] = "  syntax error";
            }
		  | error Eof
            {
			   Compiler.errors[lineno] = "  syntax error";
               yyerrok();
               YYACCEPT;
            }
          | Eof
            {
               YYACCEPT;
            }
          ;

exp		  : exp { if($1.error == null && !bool.Parse($1.val)){ Compiler.genCode = false; }} And rel
				{
					if($1.error == null && !bool.Parse($1.val))
					{
						$$ = $1;
						Compiler.genCode = true;
					}
					else
					{
						$$ = Compiler.LogicalOp($1,$4,Tokens.And); 
					}
				}
		  | exp { if($1.error == null && bool.Parse($1.val)){ Compiler.genCode = false; }} Or rel
				{
					if($1.error == null && bool.Parse($1.val))
					{
						$$ = $1;
						Compiler.genCode = true;
					}
					else
					{
						$$ = Compiler.LogicalOp($1,$4,Tokens.Or); 
					}
				}
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
               { $$ = Compiler.Create($1.val, 'i'); }
		  | RealNumber
               { $$ = Compiler.Create($1.val, 'r'); }
		  | Boolean
               { $$ = Compiler.Create($1.val, 'b'); }
          | Ident
			   { $$ = Compiler.GetVariable($1.val); }
          ;

%%

int lineno = 1;

public Parser(Scanner scanner) : base(scanner) { }