namespace MikeNakis.CSharpTypeNames;

[Sys.Flags]
public enum Options
{
	/// <summary>Specifies no options.</summary>
	None = 0,

	/// <summary>Specifies that name aliases should be emitted for built-in types. For example, <b><c>int</c></b> instead of <b><c>System.Int32</c></b>.</summary>
	UseBuiltInTypeNameAliases = 1 << 0,

	/// <summary>Specifies that shorthand notation for nullable value types should be used. For example, <b><c>System.DateTime?</c></b> instead of <b><c>System.Nullable&lt;System.DateTime&gt;</c></b>.</summary>
	UseNullableShorthand = 1 << 1,

	/// <summary>Specifies that generic parameter names should be emitted rather than left blank. For example, <b><c>System.Collections.Generic.Dictionary&lt;TKey,TValue&gt;</c></b> instead of <b><c>System.Collections.Generic.Dictionary&lt;,&gt;</c></b>.</summary>
	UseGenericParameters = 1 << 2,

	/// <summary>Specifies that 'nint' and 'nuint' should be emitted instead of `System.IntPtr` and `System.UIntPtr` respectively. Only valid if <see cref="UseBuiltInTypeNameAliases" /> is also specified.</summary>
	UseNIntAndNUIntAliases = 1 << 3,
}

public static class CSharpTypeNameGenerator
{
	/// <summary>Generates the human-readable name of a <see cref="Sys.Type"/> using C# notation.</summary>
	/// <param name="type">The <see cref="Sys.Type"/> whose name is to be generated.</param>
	/// <param name="options">Specifies how the name will be generated. Default is <see cref="Options.None"/>.</param>
	/// <remarks>
	/// PEARL: DotNet internally represents the names of types in a cryptic way which greatly differs from the way they are
	/// specified in C# source code:<para/>
	/// <list type="bullet">
	/// <item>Generic types are suffixed with a back-quote character, followed by the number of generic parameters.</item>
	/// <item>Constructed generic types are further suffixed with a list of assembly-qualified type names, one for each generic parameter.</item>
	/// <item>Nested class names are delimited with '+' instead of '.'.</item>
	/// </list>
	/// PEARL-ON-PEARL: Dotnet does not provide any means of converting such cryptic type names to C# notation.<para/>
	/// This method fixes all this insanity.<para/>
	/// </remarks>
	public static string GetCSharpTypeName( Sys.Type type, Options options = Options.None )
	{
		if( type.IsGenericParameter ) //if a generic parameter is directly passed, always yield its name.
			return type.Name;
		SysText.StringBuilder stringBuilder = new();
		recurse( type );
		return stringBuilder.ToString();

		void recurse( Sys.Type type )
		{
			if( type.IsGenericParameter )
			{
				stringBuilder.Append( options.HasFlag( Options.UseGenericParameters ) ? type.Name : "" );
				return;
			}

			if( type.IsArray )
			{
				recurse( type.GetElementType()! );
				stringBuilder.Append( '[' );
				stringBuilder.Append( ',', type.GetArrayRank() - 1 );
				stringBuilder.Append( ']' );
				return;
			}

			if( options.HasFlag( Options.UseBuiltInTypeNameAliases ) )
			{
				string? alias = getBuiltInTypeNameAlias( type, options.HasFlag( Options.UseNIntAndNUIntAliases ) );
				if( alias != null )
				{
					stringBuilder.Append( alias );
					return;
				}
			}

			if( options.HasFlag( Options.UseNullableShorthand ) )
			{
				var underlyingType = Sys.Nullable.GetUnderlyingType( type );
				if( underlyingType != null )
				{
					recurse( underlyingType );
					stringBuilder.Append( '?' );
					return;
				}
			}

			Sys.Type[] allGenericArguments = type.GetGenericArguments();
			recurseNested( type, allGenericArguments );
			return;

			void recurseNested( Sys.Type type, Sys.Type[] allGenericArguments )
			{
				if( type.IsNested )
				{
					recurseNested( type.DeclaringType!, allGenericArguments );
					stringBuilder.Append( '.' );
				}
				else
				{
					string? namespaceName = type.Namespace;
					if( namespaceName != null )
					{
						stringBuilder.Append( namespaceName );
						stringBuilder.Append( '.' );
					}
				}

				if( type.IsGenericType )
				{
					string typeName = type.Name;
					int indexOfTick = typeName.LastIndexOf( '`' );
					SysDiag.Debug.Assert( indexOfTick == typeName.IndexOf( '`' ) );
					if( indexOfTick == -1 )
					{
						stringBuilder.Append( typeName );
						return;
					}
					stringBuilder.Append( typeName.Substring( 0, indexOfTick ) );
					stringBuilder.Append( '<' );
					Sys.Type[] arguments = type.GetGenericArguments();
					int start = type.IsNested ? type.DeclaringType!.GetGenericArguments().Length : 0;
					SysDiag.Debug.Assert( arguments.Length - start == int.Parse( typeName.Substring( indexOfTick + 1 ), SysGlob.CultureInfo.InvariantCulture ) );
					for( int i = start; i < arguments.Length; i++ )
					{
						Sys.Type argument = arguments[i];
						if( argument.IsGenericParameter )
						{
							int position = argument.GenericParameterPosition;
							argument = allGenericArguments[position];
						}
						recurse( argument );
						if( i + 1 < arguments.Length )
							stringBuilder.Append( ',' );
					}
					stringBuilder.Append( '>' );
					return;
				}

				stringBuilder.Append( type.Name );
			}
		}
	}

	static string? getBuiltInTypeNameAlias( Sys.Type type, bool useNIntAndNUIntAliases )
	{
		if( ((Sys.Func<bool>)(() => true)).Invoke() )
		{
			if( type == typeof( object ) )
				return "object";
			if( type == typeof( void ) )
				return "void";
			if( useNIntAndNUIntAliases )
			{
				if( type == typeof( nint ) )
					return "nint";
				if( type == typeof( nuint ) )
					return "nuint";
			}
			return Sys.Type.GetTypeCode( type ) switch
			{
				Sys.TypeCode.SByte => "sbyte",
				Sys.TypeCode.Byte => "byte",
				Sys.TypeCode.Int16 => "short",
				Sys.TypeCode.UInt16 => "ushort",
				Sys.TypeCode.Int32 => "int",
				Sys.TypeCode.UInt32 => "uint",
				Sys.TypeCode.Int64 => "long",
				Sys.TypeCode.UInt64 => "ulong",
				Sys.TypeCode.Char => "char",
				Sys.TypeCode.Single => "float",
				Sys.TypeCode.Double => "double",
				Sys.TypeCode.Boolean => "bool",
				Sys.TypeCode.Decimal => "decimal",
				Sys.TypeCode.Object => null, //typecode is 'object' for everything not in the list.
				Sys.TypeCode.String => "string",
				_ => null,
			};
		}
		else
		{
			if( type == typeof( sbyte ) )
				return "sbyte";
			if( type == typeof( byte ) )
				return "byte";
			if( type == typeof( short ) )
				return "short";
			if( type == typeof( ushort ) )
				return "ushort";
			if( type == typeof( int ) )
				return "int";
			if( type == typeof( uint ) )
				return "uint";
			if( type == typeof( long ) )
				return "long";
			if( type == typeof( ulong ) )
				return "ulong";
			if( type == typeof( char ) )
				return "char";
			if( type == typeof( float ) )
				return "float";
			if( type == typeof( double ) )
				return "double";
			if( type == typeof( bool ) )
				return "bool";
			if( type == typeof( decimal ) )
				return "decimal";
			if( type == typeof( object ) )
				return "object";
			if( type == typeof( string ) )
				return "string";
			if( type == typeof( void ) )
				return "void";
			if( type == typeof( nint ) )
				return "nint";
			if( type == typeof( nuint ) )
				return "nuint";
			return null;
		}
	}
}
