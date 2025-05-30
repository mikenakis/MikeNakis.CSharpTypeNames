namespace MikeNakis.CSharpTypeNames.Test;

using MikeNakis.CSharpTypeNames.Extensions;
using static System.Reflection.Metadata.PEReaderExtensions;
using CodeDom = Sys.CodeDom;
using CSharp = Microsoft.CSharp;
using SysReflectMetadata = SysReflect.Metadata;
using SysReflectPortableExecutable = SysReflect.PortableExecutable;
using VSTesting = Microsoft.VisualStudio.TestTools.UnitTesting;

[VSTesting.TestClass]
public sealed class T104_CSharpTypeNamesTests
{
	[VSTesting.TestMethod]
	public void Test()
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
	}

	static void test( Sys.Type type )
	{
		string generatedTypeName = type.GetCSharpName( Options.NoNullableShorthandNotation | Options.NoLanguageKeywordsForNativeIntegers | Options.NoTupleShorthandNotation );
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

	[VSTesting.TestMethod]
	public void All_DotNet_SDK_Types()
	{
		string dotNetSdkDirectoryPath = getDotNetSdkDirectoryPath();
		List<(SysReflect.Assembly, Sys.Type[])> assembliesAndTypes = new();
		foreach( string filePath in enumerateFiles( dotNetSdkDirectoryPath, "*.dll" ) )
		{
			SysReflect.Assembly? assembly = tryLoadAssemblyFrom( filePath );
			if( assembly == null )
				continue;
			Sys.Type[] types = assembly.GetTypes();
			if( types.Length == 0 )
				continue;
			assembliesAndTypes.Add( (assembly, types) );
		}
		int typeCount = assembliesAndTypes.Select( tuple => tuple.Item2.Length ).Sum();
		Sys.Console.WriteLine( $"Generating type names for all {typeCount} types in all {assembliesAndTypes.Count} assemblies of the DotNet SDK..." );
		Sys.Console.WriteLine( "" );
		foreach( (SysReflect.Assembly assembly, Sys.Type[] types) in assembliesAndTypes )
		{
			Sys.Console.WriteLine( $"Generating type names for {types.Length} types in assembly {assembly.GetName().Name}..." );
			Sys.Console.Out.Flush();
			foreach( var type in types )
				test( type );
		}
	}

	static IEnumerable<string> enumerateFiles( string directoryPath, string pattern )
	{
		foreach( string s in SysIo.Directory.GetFiles( directoryPath, pattern ) )
			yield return s;
	}

	static string getDotNetSdkDirectoryPath()
	{
		Sys.Type type = typeof( string );
		SysReflect.Assembly assembly = type.Assembly;
		string? result = SysIo.Path.GetDirectoryName( assembly.Location );
		SysDiag.Debug.Assert( result != null );
		return result;
	}

	static SysReflect.Assembly? tryLoadAssemblyFrom( string filePath )
	{
		// PEARL: trying to load "System.Private.CoreLib.dll" fails with a System.IO.FileNotFoundException, which
		//        is A DAMNED LIE, because the file definitely exists.
		//        The only workaround I can think of is to add a special-case for this assembly and instead of
		//        loading it return the already loaded one.
		if( SysIo.Path.GetFileName( filePath )! == "System.Private.CoreLib.dll" )
		{
			Sys.Type type = typeof( string );
			SysReflect.Assembly assembly = type.Assembly;
			SysDiag.Debug.Assert( assembly.Location == filePath );
			return assembly;
		}

		// PEARL: Even though dotnet assemblies and non-dotnet (native or otherwise) assemblies are fundamentally
		//        different beasts, they both have a .dll extension, so you have no way of knowing what is what.
		// PEARL: SysReflect.Assembly has no `TryLoadFrom()` method, so the only way to try to load an assembly is
		//        to invoke `LoadFrom()` and catch the exception, which is excruciatingly slow.
		// Therefore, we have to perform additional magical incantations to detect if the dll is an assembly, and
		// refrain from trying to load it if not.
		if( !isAssembly( filePath ) )
			return null;
		return SysReflect.Assembly.LoadFrom( filePath );

		// from https://stackoverflow.com/a/74839545/773113
		static bool isAssembly( string filePath )
		{
			using( SysIo.FileStream fs = new SysIo.FileStream( filePath, SysIo.FileMode.Open, SysIo.FileAccess.Read, SysIo.FileShare.ReadWrite ) )
			{
				using var peReader = new SysReflectPortableExecutable.PEReader( fs );
				if( !peReader.HasMetadata )
					return false;
				SysReflectMetadata.MetadataReader reader = peReader.GetMetadataReader();
				return reader.IsAssembly;
			}
		}
	}
}
