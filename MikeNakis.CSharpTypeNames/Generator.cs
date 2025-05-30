namespace MikeNakis.CSharpTypeNames;

[Sys.Flags]
public enum Options
{
	/// <summary>Specifies no options.</summary>
	None = 0,

	/// <summary>Specifies that language keywords for built-in types should be used.</summary>
	/// <remarks>For example, <b><c>int</c></b> will be generated instead of <b><c>System.Int32</c></b>.</remarks>
	UseLanguageKeywordsForBuiltInTypes = 1 << 0,

	/// <summary>Specifies that shorthand notation for nullable value types should be used.</summary>
	/// <remarks>For example, <b><c>System.DateTime?</c></b> will be generated instead of <b><c>System.Nullable&lt;System.DateTime&gt;</c></b>.</remarks>
	UseNullableShorthandNotation = 1 << 1,

	/// <summary>Specifies that generic parameter names should be used rather than left blank.</summary>
	/// <remarks>For example, <b><c>System.Collections.Generic.Dictionary&lt;TKey,TValue&gt;</c></b> will be generated instead of <b><c>System.Collections.Generic.Dictionary&lt;,&gt;</c></b>.</remarks>
	UseGenericParameterNames = 1 << 2,

	/// <summary>Specifies that language keywords for native integer built-in types should be used.</summary>
	/// <remarks>For example, <b><c>nint</c></b> will be generated instead of <b><c>System.IntPtr</c></b>.<para/>
	/// Only valid if <see cref="UseLanguageKeywordsForBuiltInTypes" /> is also specified.</remarks>
	UseLanguageKeywordsForNativeIntegers = 1 << 3,

	/// <summary>Specifies that namespaces should not be used.</summary>
	/// <remarks>For example, <b><c>DateTime</c></b> will be generated instead of <b><c>System.DateTime</c></b>.</remarks>
	OmitNamespaces = 1 << 4,

	/// <summary>Specifies that tuple notation should be used for value tuples.</summary>
	/// <remarks>For example, <b><c>(System.Int32,System.String)</c></b> will be generated instead of <b><c>System.ValueTuple&lt;System.Int32,System.String&gt;</c></b>.</remarks>
	UseTupleShorthandNotation = 1 << 5,
}

public static class Generator
{
	/// <summary>Generates the human-readable name of a <see cref="Sys.Type"/> using C# notation.</summary>
	/// <param name="type">The <see cref="Sys.Type"/> whose name is to be generated.</param>
	/// <param name="options">Specifies how the name will be generated.</param>
	/// <returns>The human-readable name of the given <see cref="Sys.Type"/> in C# notation.</returns>
	public static string GetCSharpTypeName( Sys.Type type, Options options )
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
				if( options.HasFlag( Options.UseGenericParameterNames ) )
					stringBuilder.Append( type.Name );
				return;
			}

			if( type.IsArray )
			{
				recurse( type.GetElementType()! );
				stringBuilder.Append( '[' );
				int rank = type.GetArrayRank();
				SysDiag.Debug.Assert( rank >= 1 );
				stringBuilder.Append( ',', rank - 1 );
				stringBuilder.Append( ']' );
				return;
			}

			if( options.HasFlag( Options.UseLanguageKeywordsForBuiltInTypes ) )
			{
				string? languageKeyword = getLanguageKeywordIfBuiltInType( type, options.HasFlag( Options.UseLanguageKeywordsForNativeIntegers ) );
				if( languageKeyword != null )
				{
					stringBuilder.Append( languageKeyword );
					return;
				}
			}

			if( options.HasFlag( Options.UseNullableShorthandNotation ) )
			{
				var underlyingType = Sys.Nullable.GetUnderlyingType( type );
				if( underlyingType != null )
				{
					recurse( underlyingType );
					stringBuilder.Append( '?' );
					return;
				}
			}

			if( options.HasFlag( Options.UseTupleShorthandNotation ) && isValueTuple( type ) )
			{
				stringBuilder.Append( '(' );
				var genericArguments = type.GetGenericArguments();
				for( int i = 0; i < genericArguments.Length; i++ )
				{
					recurse( genericArguments[i] );
					if( i + 1 < genericArguments.Length )
						stringBuilder.Append( ',' );
				}
				stringBuilder.Append( ')' );
				return;
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
					if( type.Namespace != null && !options.HasFlag( Options.OmitNamespaces ) )
					{
						stringBuilder.Append( type.Namespace );
						stringBuilder.Append( '.' );
					}
				}

				string typeName = type.Name;

				if( type.IsGenericType )
				{
					//PEARL: DotNet offers no means of obtaining the plain, unadulterated name of a generic type.
					//    The only way to get the name of a type seems to be the `Name` property, but this property
					//    returns a name that is polluted by a back-quote followed by a number, and there is nothing we
					//    can do about that.
					//    So, we have no option but to search for the back-quote and extract the part before it.
					int indexOfBackQuote = typeName.LastIndexOf( '`' );
					SysDiag.Debug.Assert( indexOfBackQuote == typeName.IndexOf( '`' ) );
					if( indexOfBackQuote != -1 )
					{
						stringBuilder.Append( typeName.Substring( 0, indexOfBackQuote ) );
						stringBuilder.Append( '<' );
						Sys.Type[] arguments = type.GetGenericArguments();
						int start = type.IsNested ? type.DeclaringType!.GetGenericArguments().Length : 0;
						SysDiag.Debug.Assert( arguments.Length - start == int.Parse( typeName.Substring( indexOfBackQuote + 1 ), SysGlob.CultureInfo.InvariantCulture ) );
						for( int i = start; i < arguments.Length; i++ )
						{
							Sys.Type argument = arguments[i];
							if( argument.IsGenericParameter )
							{
								int position = argument.GenericParameterPosition;
								//if( position >= allGenericArguments.Length )
								//	SysDiag.Debug.WriteLine( $"type: {typeName} IsGenericTypeDefinition: {type.IsGenericTypeDefinition}; argument: {argument.Name}; GenericParameterPosition: {argument.GenericParameterPosition}; allGenericArguments.Length: {allGenericArguments.Length}" );
								//else
								argument = allGenericArguments[position];
							}
							recurse( argument );
							if( i + 1 < arguments.Length )
								stringBuilder.Append( ',' );
						}
						stringBuilder.Append( '>' );
						return;
					}
				}

				stringBuilder.Append( typeName );
			}
		}
	}

	static bool isValueTuple( Sys.Type type )
	{
		// See https://stackoverflow.com/a/75852077/773113
		if( !type.IsValueType )
			return false;
		if( type.IsGenericTypeDefinition )
			return false;
		//In netcore all ValueTuple structs implement ITuple, so they can be easily detected as follows:
		//    return typeof( SysCompiler.ITuple ).IsAssignableFrom( type );
		//Unfortunately, in netstandard2.0, ITuple does not seem to exist, so we have to resort to string comparison.
		if( type.FullName == null )
			return false;
		return type.FullName.StartsWith( "System.ValueTuple`", Sys.StringComparison.Ordinal );
		//Note that the above would also match "System.ValueTuple`1[]", but it cannot happen here because arrays
		//     are not value types, and we have already checked to make sure that this is a value type.
	}

	static string? getLanguageKeywordIfBuiltInType( Sys.Type type, bool useLanguageKeywordsForNativeIntegers )
	{
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
			Sys.TypeCode.Object => otherType( type, useLanguageKeywordsForNativeIntegers ),
			Sys.TypeCode.String => "string",
			_ => null,
		};

		static string? otherType( Sys.Type type, bool useLanguageKeywordsForNativeIntegers )
		{
			if( type == typeof( object ) )
				return "object";
			if( type == typeof( void ) )
				return "void";
			if( useLanguageKeywordsForNativeIntegers )
			{
				if( type == typeof( nint ) )
					return "nint";
				if( type == typeof( nuint ) )
					return "nuint";
			}
			return null;
		}
	}
}
