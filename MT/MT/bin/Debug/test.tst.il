.assembly extern mscorlib { }
.assembly calculator { }
.method static void main()
{
.entrypoint
.try
{

// initializing Pi and E
.locals init ( float64 Pi )
.locals init ( float64 E )
ldc.r8 3.14159265358979
stloc Pi
ldc.r8 2.71828182845905
stloc E

// initializing temporary variables
.locals init ( int32 _i )
.locals init ( float64 _r )

// linia   1 :  real a
.locals init ( float64 a )

// linia   2 :  real b
.locals init ( float64 b )

// linia   3 :  real c
.locals init ( float64 c )

// linia   4 :  real d
.locals init ( float64 d )

// linia   5 :  a=2
ldc.i4 2
conv.r8
stloc a

// linia   6 :  b=-3
ldc.i4 3
neg
conv.r8
stloc b

// linia   7 :  c=-2
ldc.i4 2
neg
conv.r8
stloc c

// linia   8 :  d=sqrt(b*b-4*a*c)
ldloc b
ldloc b
mul
ldc.i4 4
ldloc a
stloc _r
conv.r8
ldloc _r
mul
ldloc c
mul
sub
conv.r8
call float64 [mscorlib]System.Math::Sqrt(float64)
stloc d

// linia   9 :  print (-b+d)/(2*a)
ldloc b
neg
ldloc d
add
ldc.i4 2
ldloc a
stloc _r
conv.r8
ldloc _r
mul
div
stloc _r
ldstr "  Result: {0}{1}"
ldloc _r
box [mscorlib]System.Double
ldstr "r"
call void [mscorlib]System.Console::WriteLine(string,object,object)

// linia  10 :  print (-b-d)/(2*a)
ldloc b
neg
ldloc d
sub
ldc.i4 2
ldloc a
stloc _r
conv.r8
ldloc _r
mul
div
stloc _r
ldstr "  Result: {0}{1}"
ldloc _r
box [mscorlib]System.Double
ldstr "r"
call void [mscorlib]System.Console::WriteLine(string,object,object)

// linia  11 :  print Pi/4
ldloc Pi
ldc.i4 4
conv.r8
div
stloc _r
ldstr "  Result: {0}{1}"
ldloc _r
box [mscorlib]System.Double
ldstr "r"
call void [mscorlib]System.Console::WriteLine(string,object,object)

// linia  12 :  bool aa
.locals init ( int32 aa )

// linia  13 :  aa=false
ldc.i4 0
stloc aa

// linia  14 :  int i
.locals init ( int32 i )

// linia  15 :  i=5
ldc.i4 5
stloc i

// linia  16 :  real x
.locals init ( float64 x )

// linia  17 :  x=7.5
ldc.r8 7.5
stloc x

// linia  18 :  bool bb
.locals init ( int32 bb )

// linia  19 :  bb = i<=x
ldloc i
ldloc x
stloc _r
conv.r8
ldloc _r
clt
stloc bb

// linia  20 :  print aa
ldloc aa
stloc _i
ldstr "  Result: {0}"
ldloc _i
box [mscorlib]System.Boolean
call void [mscorlib]System.Console::WriteLine(string,object)

// linia  21 :  print bb
ldloc bb
stloc _i
ldstr "  Result: {0}"
ldloc _i
box [mscorlib]System.Boolean
call void [mscorlib]System.Console::WriteLine(string,object)

// linia  22 :  print aa==bb
ldloc aa
ldloc bb
ceq
stloc _i
ldstr "  Result: {0}"
ldloc _i
box [mscorlib]System.Boolean
call void [mscorlib]System.Console::WriteLine(string,object)

// linia  23 :  int y
.locals init ( int32 y )

// linia  24 :  y=0
ldc.i4 0
stloc y

// linia  25 :  bb= i>10*x && aa==(x/y>-2)
ldloc i
ldc.i4 10
ldloc x
stloc _r
conv.r8
ldloc _r
mul
stloc _r
conv.r8
ldloc _r
cgt
stloc bb

// linia  26 :  print !bb
ldloc bb
ldc.i4 0
ceq
stloc _i
ldstr "  Result: {0}"
ldloc _i
box [mscorlib]System.Boolean
call void [mscorlib]System.Console::WriteLine(string,object)

// linia  27 :  print 0090
ldc.i4 90
stloc _i
ldstr "  Result: {0}{1}"
ldloc _i
box [mscorlib]System.Int32
ldstr "i"
call void [mscorlib]System.Console::WriteLine(string,object,object)

leave EndMain
}
catch [mscorlib]System.Exception
{
callvirt instance string [mscorlib]System.Exception::get_Message()
call void [mscorlib]System.Console::WriteLine(string)
leave EndMain
}
EndMain: ret
}
