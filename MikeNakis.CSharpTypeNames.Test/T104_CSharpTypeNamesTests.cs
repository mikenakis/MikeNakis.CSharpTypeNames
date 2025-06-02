namespace MikeNakis.CSharpTypeNames.Test;

using MikeNakis.CSharpTypeNames.Extensions;
using CodeDom = Sys.CodeDom;
using CSharp = Microsoft.CSharp;
using VSTesting = Microsoft.VisualStudio.TestTools.UnitTesting;

[VSTesting.TestClass]
public sealed class T104_CSharpTypeNamesTests
{
	public class T { };

	[VSTesting.TestMethod]
	public void T201_Works_As_Expected_On_Various_Types()
	{
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
		test( typeof( void ) );
		test( typeof( nint ) );
		test( typeof( nuint ) );

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

		test( typeof( List<int> ) );
		test( typeof( List<int[]> ) );
		test( typeof( List<int?> ) );
		test( typeof( List<int?[]> ) );
		test( typeof( List<int>[] ) );
		test( typeof( List<int?>[] ) );
		test( typeof( List<List<int>> ) );

		test( typeof( List<> ) );
		test( typeof( List<>.Enumerator ) );
		test( typeof( Dictionary<,> ) );
		test( typeof( Dictionary<,>.Enumerator ) );
		test( typeof( Dictionary<,>.KeyCollection ) );

		test( typeof( List<int>.Enumerator ) );
		test( typeof( List<List<int>>.Enumerator ) );
		test( typeof( Dictionary<int, bool> ) );
		test( typeof( Dictionary<List<int>, List<bool>> ) );
		test( typeof( Dictionary<int, bool>.Enumerator ) );
		test( typeof( Dictionary<List<int>, List<bool>>.Enumerator ) );
		test( typeof( Dictionary<int, int>.KeyCollection ) );

		test( typeof( C0<int, bool>.C1A ) );
		test( typeof( C0<int, bool>.C1B<byte> ) );
		test( typeof( C0<int, bool>.C1B<byte>.C2A ) );
		test( typeof( C0<int, bool>.C1B<byte>.C2A.C3A<char> ) );
		test( typeof( C0<int, bool>.C1B<byte>.C2B<char> ) );

		test( typeof( C0<,>.C1A ) );
		test( typeof( C0<,>.C1B<> ) );
		test( typeof( C0<,>.C1B<>.C2A ) );
		test( typeof( C0<,>.C1B<>.C2A.C3A<> ) );
		test( typeof( C0<,>.C1B<>.C2B<> ) );

		test( typeof( C0<,> ).GetField( C0<T, T>.F1Name )!.FieldType );
		test( typeof( C0<,> ).GetField( C0<T, T>.F2Name )!.FieldType );
		test( typeof( C0<,> ).GetField( C0<T, T>.F2cName )!.FieldType );
		test( typeof( C0<,>.C1A ).GetField( C0<T, T>.C1A.F1Name )!.FieldType );
		test( typeof( C0<,>.C1A ).GetField( C0<T, T>.C1A.F2Name )!.FieldType );
		test( typeof( C0<,>.C1A ).GetField( C0<T, T>.C1A.F2cName )!.FieldType );
		test( typeof( C0<,>.C1B<> ).GetField( C0<T, T>.C1B<T>.F1Name )!.FieldType );
		test( typeof( C0<,>.C1B<> ).GetField( C0<T, T>.C1B<T>.F2Name )!.FieldType );
		test( typeof( C0<,>.C1B<> ).GetField( C0<T, T>.C1B<T>.F3Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2A ).GetField( C0<T, T>.C1B<T>.C2A.F1Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2A ).GetField( C0<T, T>.C1B<T>.C2A.F2Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2A ).GetField( C0<T, T>.C1B<T>.C2A.F3Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2A.C3A<> ).GetField( C0<T, T>.C1B<T>.C2A.C3A<T>.F1Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2A.C3A<> ).GetField( C0<T, T>.C1B<T>.C2A.C3A<T>.F2Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2A.C3A<> ).GetField( C0<T, T>.C1B<T>.C2A.C3A<T>.F3Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2A.C3A<> ).GetField( C0<T, T>.C1B<T>.C2A.C3A<T>.F4Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2B<> ).GetField( C0<T, T>.C1B<T>.C2B<int>.F1Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2B<> ).GetField( C0<T, T>.C1B<T>.C2B<int>.F2Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2B<> ).GetField( C0<T, T>.C1B<T>.C2B<int>.F3Name )!.FieldType );
		test( typeof( C0<,>.C1B<>.C2B<> ).GetField( C0<T, T>.C1B<T>.C2B<int>.F4Name )!.FieldType );

		test( typeof( C0<int, bool> ).GetField( C0<T, T>.F1Name )!.FieldType );
		test( typeof( C0<int, bool> ).GetField( C0<T, T>.F2Name )!.FieldType );
		test( typeof( C0<int, bool> ).GetField( C0<T, T>.F2cName )!.FieldType );
		test( typeof( C0<int, bool>.C1A ).GetField( C0<T, T>.C1A.F1Name )!.FieldType );
		test( typeof( C0<int, bool>.C1A ).GetField( C0<T, T>.C1A.F2Name )!.FieldType );
		test( typeof( C0<int, bool>.C1A ).GetField( C0<T, T>.C1A.F2cName )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte> ).GetField( C0<T, T>.C1B<T>.F1Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte> ).GetField( C0<T, T>.C1B<T>.F2Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte> ).GetField( C0<T, T>.C1B<T>.F3Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2A ).GetField( C0<T, T>.C1B<T>.C2A.F1Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2A ).GetField( C0<T, T>.C1B<T>.C2A.F2Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2A ).GetField( C0<T, T>.C1B<T>.C2A.F3Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2A.C3A<char> ).GetField( C0<T, T>.C1B<T>.C2A.C3A<T>.F1Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2A.C3A<char> ).GetField( C0<T, T>.C1B<T>.C2A.C3A<T>.F2Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2A.C3A<char> ).GetField( C0<T, T>.C1B<T>.C2A.C3A<T>.F3Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2A.C3A<char> ).GetField( C0<T, T>.C1B<T>.C2A.C3A<T>.F4Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2B<char> ).GetField( C0<T, T>.C1B<T>.C2B<T>.F1Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2B<char> ).GetField( C0<T, T>.C1B<T>.C2B<T>.F2Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2B<char> ).GetField( C0<T, T>.C1B<T>.C2B<T>.F3Name )!.FieldType );
		test( typeof( C0<int, bool>.C1B<byte>.C2B<char> ).GetField( C0<T, T>.C1B<T>.C2B<T>.F4Name )!.FieldType );
	}

	static void test( Sys.Type type )
	{
		string generatedTypeName = type.GetCSharpName( Options.NoNullableShorthandNotation | Options.NoKeywordsForNativeSizedIntegers | Options.NoTupleShorthandNotation );
		Sys.Console.WriteLine( $"    {generatedTypeName}" );
		string expectedTypeName = getTypeNameFromCSharpCompiler( type );
		if( generatedTypeName != expectedTypeName )
		{
			Sys.Console.WriteLine( $"        CSharpCompiler: {expectedTypeName}" );
			Sys.Console.WriteLine( $"        Runtime:        {getTypeNameFromRuntime( type )}" );
			SysDiag.Debug.Assert( false );
		}
	}

	static string getTypeNameFromCSharpCompiler( Sys.Type type )
	{
		var cSharpCompiler = new CSharp.CSharpCodeProvider();
		var typeRef = new CodeDom.CodeTypeReference( type );
		string typeName = cSharpCompiler.GetTypeOutput( typeRef );
		//PEARL: Microsoft.CSharp.CSharpCodeProvider.GetTypeOutput() has a bug causing it to insert superfluous spaces
		//    in the generated type names, so we have to remove them here.
		return typeName.Replace( " ", "", Sys.StringComparison.Ordinal );
	}

	static string getTypeNameFromRuntime( Sys.Type type )
	{
		string fullName = type.FullName!;
		if( fullName == null )
			return type.Name;
		//The type names generated by the runtime often contain information about assemblies. This information makes
		//the type names insanely verbose, but it is entirely useless to us, so we remove it here.
		return RegEx.Regex.Replace( fullName, @", .+?, Version=.+?, Culture=.+?, PublicKeyToken=[0123456789abcdef]+", "(...)" );
	}
}
