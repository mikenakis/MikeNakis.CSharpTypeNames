#pragma warning disable IDE0100 // Remove redundant equality
#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable CA1812 // Internal class never instantiated
#pragma warning disable CS0649 // Field never assigned to

namespace MikeNakis.CSharpTypeNames.Drill;

using System.Collections.Generic;
using System.Linq;
using MikeNakis.CSharpTypeNames.Extensions;
using MikeNakis.Kit;
using MikeNakis.Kit.Collections;
using MikeNakis.Kit.Extensions;
using MikeNakis.Kit.FileSystem;
using static System.MemoryExtensions;
using static System.Reflection.Metadata.PEReaderExtensions;
using CodeDom = System.CodeDom;
using CSharp = Microsoft.CSharp;
using RegEx = System.Text.RegularExpressions;
using Sys = System;
using SysDiag = System.Diagnostics;
using SysIo = System.IO;
using SysReflect = System.Reflection;
using SysReflectMetadata = System.Reflection.Metadata;
using SysReflectPortableExecutable = System.Reflection.PortableExecutable;

sealed class CSharpTypeNamesDrillMain
{
	public static void Main( string[] _ )
	{
		StartupProjectDirectory.Initialize();
		Console.ConsoleHelpers.Run( false, run );
	}

	record struct Designator( bool IsConstructed, bool ContainsParameters, bool IsDefinition )
	{
		public static Designator Of( Sys.Type type ) => new Designator( type.IsConstructedGenericType, type.ContainsGenericParameters, type.IsGenericTypeDefinition );

		public static readonly Designator OfNonGenericType = new Designator( false, false, false );
		public static readonly Designator OfGenericType = new Designator( true, false, false );
		public static readonly Designator OfGenericTypeDefinition = new Designator( false, true, true );
		public static readonly Designator OfGenericArgument = new Designator( false, true, false );
	}

	public sealed class Type1<T, U>
	{
		public const string FieldName = nameof( F );
		public ICollection<U>? F;
	}

	static int run()
	{
		// Studying Non-Generic Types */
		{
			Sys.Type type = typeof( string );
			Assert( type.IsGenericType == false );
			Assert( Designator.Of( type ) == Designator.OfNonGenericType );
			Assert( type.GenericTypeArguments.Length == 0 );
			Assert( type.GetGenericArguments().Length == 0 );
			Assert( TryCatch( () => type.GetGenericTypeDefinition() ) is Sys.InvalidOperationException );
		}

		// Studying Generic Types
		{
			Sys.Type type = typeof( IEnumerable<string> );
			Assert( type.IsGenericType == true );
			Assert( Designator.Of( type ) == Designator.OfGenericType );
			Assert( type.GenericTypeArguments.Length == 1 );
			Assert( type.GenericTypeArguments[0] == typeof( string ) );
			Assert( type.GenericTypeArguments.SequenceEqual( type.GetGenericArguments() ) );
			Assert( type.GetGenericTypeDefinition().ReferenceEquals( typeof( IEnumerable<> ) ) );
		}

		// Studying Generic Type Definitions
		{
			Sys.Type type = typeof( IEnumerable<> );
			Assert( type.IsGenericType == true );
			Assert( Designator.Of( type ) == Designator.OfGenericTypeDefinition );
			Assert( type.GenericTypeArguments.Length == 0 );
			Assert( type.GetGenericArguments().Length == 1 );
			Assert( type.GetGenericTypeDefinition().ReferenceEquals( type ) );

			Sys.Type argumentType = type.GetGenericArguments()[0];
			Assert( argumentType.IsGenericType == false );
			Assert( Designator.Of( argumentType ) == Designator.OfGenericArgument );
			Assert( argumentType.IsGenericParameter == true );
			Assert( argumentType.IsGenericTypeParameter == true );
			Assert( argumentType.IsGenericMethodParameter == false );
			Assert( argumentType.Name == "T" );
			Assert( argumentType.FullName == null );
			Assert( argumentType.GenericParameterPosition == 0 );
		}

		// Studying Nested Generic Type Definitions
		{
			Sys.Type type = typeof( Type1<,> ).GetField( Type1<int, int>.FieldName )!.FieldType;
			Assert( type.IsGenericType == true );
			Assert( Designator.Of( type ) == new Designator( true, true, false ) ); // !!!
			Assert( type.GenericTypeArguments.Length == 1 );
			Assert( type.GetGenericArguments().Length == 1 );
			Assert( type.GenericTypeArguments.SequenceEqual( type.GetGenericArguments() ) );

			Sys.Type genericTypeDefinition = type.GetGenericTypeDefinition();
			Assert( genericTypeDefinition.IsGenericType == true );
			Assert( Designator.Of( genericTypeDefinition ) == Designator.OfGenericTypeDefinition );
			Assert( genericTypeDefinition.GenericTypeArguments.Length == 0 );
			Assert( genericTypeDefinition.GetGenericArguments().Length == 1 );
			Assert( genericTypeDefinition.IsNested == false );

			Sys.Type argumentType = type.GetGenericArguments()[0];
			Assert( argumentType.IsGenericType == false );
			Assert( Designator.Of( argumentType ) == Designator.OfGenericArgument );
			Assert( argumentType.IsGenericParameter == true );
			Assert( argumentType.IsGenericTypeParameter == true );
			Assert( argumentType.IsGenericMethodParameter == false );
			Assert( argumentType.Name == "U" );
			Assert( argumentType.FullName == null );
			Assert( argumentType.GenericParameterPosition == 1 );
		}

		DirectoryPath dotNetSdkDirectoryPath = getDotNetSdkDirectoryPath();
		List<(SysReflect.Assembly, IReadOnlyList<Sys.Type>)> assembliesAndTypes = new();
		foreach( FilePath filePath in dotNetSdkDirectoryPath.EnumerateFiles( "*.dll" ) )
		{
			SysReflect.Assembly? assembly = tryLoadAssemblyFrom( filePath );
			if( assembly == null )
				continue;
			IReadOnlyList<Sys.Type> types = addNestedTypes( assembly.GetTypes() ).Collect();
			if( types.Count == 0 )
				continue;
			assembliesAndTypes.Add( (assembly, types) );
		}
		int typeCount = assembliesAndTypes.Select( tuple => tuple.Item2.Count ).Sum();
		Sys.Console.WriteLine( $"Generating type names for all {typeCount} types in {assembliesAndTypes.Count} assemblies of the DotNet SDK..." );
		Sys.Console.WriteLine( "" );

		long start = SysDiag.Stopwatch.GetTimestamp();

		bool consoleOutput = false;
		foreach( (SysReflect.Assembly assembly, IReadOnlyList<Sys.Type> types) in assembliesAndTypes )
		{
			if( consoleOutput )
				Sys.Console.WriteLine( $"Generating type names for {types.Count} types in assembly {assembly.GetName().Name}..." );
			foreach( Sys.Type type in types )
				test( type, consoleOutput );
		}

		Sys.Console.WriteLine( $"{SysDiag.Stopwatch.GetElapsedTime( start ).TotalMilliseconds} milliseconds" );

		return 0;
	}

	static void test( Sys.Type type, bool consoleOutput )
	{
		string generatedTypeName = type.GetCSharpName( Options.NoNullableShorthandNotation | Options.NoKeywordsForNativeSizedIntegers | Options.NoTupleShorthandNotation );
		string expectedTypeName = getTypeNameFromCSharpCompiler( type );
		bool failure = generatedTypeName != expectedTypeName;
		if( consoleOutput || failure )
			Sys.Console.WriteLine( $"    {generatedTypeName}" );
		if( failure )
		{
			Sys.Console.WriteLine( $"        CSharpCompiler: {expectedTypeName}" );
			Sys.Console.WriteLine( $"        Runtime:        {getTypeNameFromRuntime( type )}" );
			Assert( false );
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

	static IEnumerable<Sys.Type> addNestedTypes( IEnumerable<Sys.Type> types )
	{
		return types.Concat( types.SelectMany( type => addNestedTypes( type.GetNestedTypes() ) ) );
	}

	static DirectoryPath getDotNetSdkDirectoryPath()
	{
		Sys.Type type = typeof( string );
		SysReflect.Assembly assembly = type.Assembly;
		FilePath assemblyLocationFilePath = FilePath.FromAbsolutePath( assembly.Location );
		return assemblyLocationFilePath.Directory;
	}

	static SysReflect.Assembly? tryLoadAssemblyFrom( FilePath filePath )
	{
		// PEARL: trying to load "System.Private.CoreLib.dll" fails with a System.IO.FileNotFoundException, which
		//        is A DAMNED LIE, because the file definitely exists.
		//        The only workaround I can think of is to add a special-case for this assembly and instead of
		//        loading it return the already loaded one.
		if( filePath.GetFileNameAndExtension() == "System.Private.CoreLib.dll" )
		{
			Sys.Type type = typeof( string );
			SysReflect.Assembly assembly = type.Assembly;
			Assert( assembly.Location == filePath.Path );
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
		return SysReflect.Assembly.LoadFrom( filePath.Path );

		// from https://stackoverflow.com/a/74839545/773113
		static bool isAssembly( FilePath filePath )
		{
			using( SysIo.FileStream fileStream = new SysIo.FileStream( filePath.Path, SysIo.FileMode.Open, SysIo.FileAccess.Read, SysIo.FileShare.ReadWrite ) )
			{
				using var peReader = new SysReflectPortableExecutable.PEReader( fileStream );
				if( !peReader.HasMetadata )
					return false;
				SysReflectMetadata.MetadataReader reader = peReader.GetMetadataReader();
				return reader.IsAssembly;
			}
		}
	}
}
