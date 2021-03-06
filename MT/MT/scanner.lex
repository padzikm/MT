%using QUT.Gppg;
%namespace GardensPoint

Type		"int"|"real"|bool
IntNumber   [0-9]+
RealNumber  [0-9]+\.[0-9]+
Boolean		"true"|"false"
Ident       "Pi"|"E"|[a-z][a-z0-9]*

%%

"print"             { return (int)Tokens.Print; }
"exit"              { return (int)Tokens.Exit; }
{IntNumber}			{ yylval.val=yytext; return (int)Tokens.IntNumber; }
{RealNumber}		{ yylval.val=yytext; return (int)Tokens.RealNumber; }
{Boolean}			{ yylval.val=yytext; return (int)Tokens.Boolean; }
{Type}				{ yylval.type=yytext[0]; return (int)Tokens.Type; }
{Ident}				{ yylval.val=yytext; return (int)Tokens.Ident; }
"="					{ return (int)Tokens.Assign; }
"&&"				{ return (int)Tokens.And; }
"||"				{ return (int)Tokens.Or; }
"=="				{ return (int)Tokens.Equal; }
"!="				{ return (int)Tokens.Diff; }
">"					{ return (int)Tokens.Gt; }
"<"					{ return (int)Tokens.Lt; }
">="				{ return (int)Tokens.Gte; }
"<="				{ return (int)Tokens.Lte; }
"+"					{ return (int)Tokens.Plus; }
"-"					{ return (int)Tokens.Minus; }
"*"					{ return (int)Tokens.Multiplies; }
"/"					{ return (int)Tokens.Divides; }
"!"					{ return (int)Tokens.Negation; }
"("					{ return (int)Tokens.OpenPar; }
")"					{ return (int)Tokens.ClosePar; }
"\r"				{ return (int)Tokens.Endl; }
<<EOF>>				{ return (int)Tokens.Eof; }
" "					{ }
"\t"				{ }
.					{ return (int)Tokens.Error; }