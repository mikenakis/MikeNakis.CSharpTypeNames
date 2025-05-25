namespace MikeNakis.CSharpTypeNames.Test;

using MikeNakis.CSharpTypeNames.Extensions;
using MikeNakis.Kit.Extensions;
using CodeDom = Sys.CodeDom;
using CSharp = Microsoft.CSharp;
using VSTesting = Microsoft.VisualStudio.TestTools.UnitTesting;

[VSTesting.TestClass]
public sealed class T104_CSharpTypeNamesTests
{
	[VSTesting.TestMethod]
	public void Test()
	{
		var cSharpCompiler = new CSharp.CSharpCodeProvider();

		test( typeof( sbyte ) );
		test( typeof( byte ) );
		test( typeof( short ) );
		test( typeof( ushort ) );
		test( typeof( int ) );
		test( typeof( uint ) );
		test( typeof( long ) );
		test( typeof( ulong ) );
		test( typeof( char ) );
		test( typeof( float ) );
		test( typeof( double ) );
		test( typeof( bool ) );
		test( typeof( decimal ) );

		test( typeof( object ) );
		test( typeof( string ) );
		test( typeof( Sys.Int128 ) );
		test( typeof( Sys.Guid ) );

		test( typeof( int[] ) );
		test( typeof( int[,] ) );
		test( typeof( int[,,] ) );
		test( typeof( int[][] ) );

		test( typeof( int? ) );
		test( typeof( int?[] ) );
		test( typeof( int?[,] ) );
		test( typeof( int?[,,] ) );
		test( typeof( int?[][] ) );

		test( typeof( List<> ) );
		test( typeof( List<int> ) );
		test( typeof( List<int[,]> ) );
		test( typeof( List<int[][]> ) );
		test( typeof( List<int?> ) );
		test( typeof( List<int?[,]> ) );
		test( typeof( List<int?[][]> ) );
		test( typeof( List<int>[] ) );
		test( typeof( List<int>[,] ) );
		test( typeof( List<int>[][] ) );
		test( typeof( List<List<int>> ) );
		test( typeof( List<List<int[,]>> ) );
		test( typeof( List<List<int[][]>> ) );
		test( typeof( List<List<int?>> ) );
		test( typeof( List<List<int?[,]>> ) );
		test( typeof( List<List<int?[][]>> ) );
		test( typeof( Dictionary<,> ) );
		test( typeof( Dictionary<int, bool> ) );
		test( typeof( Dictionary<List<int>, List<bool>> ) );
		test( typeof( List<>.Enumerator ) );
		test( typeof( List<int>.Enumerator ) );
		test( typeof( List<List<int>>.Enumerator ) );
		test( typeof( Dictionary<,>.Enumerator ) );
		test( typeof( Dictionary<int, bool>.Enumerator ) );
		test( typeof( Dictionary<List<int>, List<bool>>.Enumerator ) );

		test( typeof( C0<int>.C1A ) );
		test( typeof( C0<int>.C1B<bool, byte> ) );
		test( typeof( C0<int>.C1B<bool, byte>.C2A ) );
		test( typeof( C0<int>.C1B<bool, byte>.C2B<char> ) );

		test( typeof( C0<>.C1A ) );
		test( typeof( C0<>.C1B<,> ) );
		test( typeof( C0<>.C1B<,>.C2A ) );
		test( typeof( C0<>.C1B<,>.C2B<> ) );

		test( typeof( C0<> ).GetField( nameof( C0<int>.F1 ) )!.FieldType );
		test( typeof( C0<>.C1A ).GetField( nameof( C0<int>.C1A.F1 ) )!.FieldType );
		test( typeof( C0<>.C1B<,> ).GetField( nameof( C0<int>.C1B<int, int>.F1 ) )!.FieldType );
		test( typeof( C0<>.C1B<,> ).GetField( nameof( C0<int>.C1B<int, int>.F2 ) )!.FieldType );
		test( typeof( C0<>.C1B<,> ).GetField( nameof( C0<int>.C1B<int, int>.F3 ) )!.FieldType );
		test( typeof( C0<>.C1B<,>.C2A ).GetField( nameof( C0<int>.C1B<int, int>.C2A.F1 ) )!.FieldType );
		test( typeof( C0<>.C1B<,>.C2A ).GetField( nameof( C0<int>.C1B<int, int>.C2A.F2 ) )!.FieldType );
		test( typeof( C0<>.C1B<,>.C2A ).GetField( nameof( C0<int>.C1B<int, int>.C2A.F3 ) )!.FieldType );
		test( typeof( C0<>.C1B<,>.C2B<> ).GetField( nameof( C0<int>.C1B<int, int>.C2B<int>.F1 ) )!.FieldType );
		test( typeof( C0<>.C1B<,>.C2B<> ).GetField( nameof( C0<int>.C1B<int, int>.C2B<int>.F2 ) )!.FieldType );
		test( typeof( C0<>.C1B<,>.C2B<> ).GetField( nameof( C0<int>.C1B<int, int>.C2B<int>.F3 ) )!.FieldType );
		test( typeof( C0<>.C1B<,>.C2B<> ).GetField( nameof( C0<int>.C1B<int, int>.C2B<int>.F4 ) )!.FieldType );

		test( typeof( C0<bool> ).GetField( nameof( C0<int>.F1 ) )!.FieldType );
		test( typeof( C0<bool>.C1A ).GetField( nameof( C0<int>.C1A.F1 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char> ).GetField( nameof( C0<int>.C1B<int, int>.F1 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char> ).GetField( nameof( C0<int>.C1B<int, int>.F2 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char> ).GetField( nameof( C0<int>.C1B<int, int>.F3 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char>.C2A ).GetField( nameof( C0<int>.C1B<int, int>.C2A.F1 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char>.C2A ).GetField( nameof( C0<int>.C1B<int, int>.C2A.F2 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char>.C2A ).GetField( nameof( C0<int>.C1B<int, int>.C2A.F3 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char>.C2B<long> ).GetField( nameof( C0<int>.C1B<int, int>.C2B<int>.F1 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char>.C2B<long> ).GetField( nameof( C0<int>.C1B<int, int>.C2B<int>.F2 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char>.C2B<long> ).GetField( nameof( C0<int>.C1B<int, int>.C2B<int>.F3 ) )!.FieldType );
		test( typeof( C0<bool>.C1B<byte, char>.C2B<long> ).GetField( nameof( C0<int>.C1B<int, int>.C2B<int>.F4 ) )!.FieldType );

		void test( Sys.Type type )
		{
			string generatedTypeName = type.GetCSharpName();
			string expectedTypeName = getTypeNameFromCSharpCompiler( type, cSharpCompiler );
			if( generatedTypeName != expectedTypeName )
			{
				Sys.Console.WriteLine( $"CSharpCompiler gives: {expectedTypeName}" );
				Sys.Console.WriteLine( $"The Runtime gives:    {getTypeNameFromRuntime( type )}" );
				SysDiag.Debug.Assert( false );
			}
		}
	}

	static string getTypeNameFromCSharpCompiler( Sys.Type type, CSharp.CSharpCodeProvider cSharpCompiler )
	{
		var typeRef = new CodeDom.CodeTypeReference( type );
		string typeName = cSharpCompiler.GetTypeOutput( typeRef );
		//PEARL: Microsoft.CSharp.CSharpCodeProvider has a bug where it includes superfluous spaces in the generated type names. We remove them here.
		return typeName.Replace( " ", "", Sys.StringComparison.Ordinal );
	}

	static string getTypeNameFromRuntime( Sys.Type type )
	{
		string fullName = type.FullName!;
		if( fullName == null )
			return type.Name;
		fullName = RegEx.Regex.Replace( fullName, @", .+?, Version=.+?, Culture=.+?, PublicKeyToken=[0123456789abcdef]+", "..." );
		return fullName.SafeSubstring( 0, 200, true );
	}
}
