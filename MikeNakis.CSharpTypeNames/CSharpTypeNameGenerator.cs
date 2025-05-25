namespace MikeNakis.CSharpTypeNames;

static class CSharpTypeNameGenerator
{
	///<summary>Computes the name of a given System.Type in human-readable C# notation.</summary>
	///<remarks>
	/// PEARL: DotNet internally represents the names of types in a cryptic way which greatly differs from the way they are
	/// specified in C# source code:<para/>
	/// <list type="bullet">
	/// <item>Generic types are suffixed with a back-quote character, followed by the number of generic parameters.</item>
	/// <item>Constructed generic types are further suffixed with a list of assembly-qualified type names, one for each generic parameter.</item>
	/// <item>Nested class names are delimited with '+' instead of '.'.</item>
	/// </list>
	/// PEARL-ON-PEARL: Dotnet does not provide any means of converting such cryptic type names to C# notation.<para/>
	/// This method fixes all this insanity.<para/>
	///</remarks>
	public static string GetCSharpTypeName( Sys.Type type, bool useAliases = true )
	{
		if( type.IsGenericParameter )
			return type.Name;
		SysText.StringBuilder stringBuilder = new();
		recurse( type );
		return stringBuilder.ToString();

		void recurse( Sys.Type type )
		{
			if( type.IsGenericParameter )
			{
				stringBuilder.Append( "" ); //TODO: append actual parameter names if requested
				return;
			}

			if( type.IsArray )
			{
				Sys.Type elementType = type.GetElementType()!;
				recurse( elementType );
				appendArraySuffix( stringBuilder, type.GetArrayRank() );
				return;
			}

			if( useAliases && appendTypeNameAlias( stringBuilder, type ) )
				return;

			Sys.Type[] allGenericArguments = type.GetGenericArguments();
			recurse2( type, allGenericArguments );
			return;

			void recurse2( Sys.Type type, Sys.Type[] allGenericArguments )
			{
				if( type.IsNested )
				{
					Sys.Type declaringType = type.DeclaringType!;
					recurse2( declaringType, allGenericArguments );
					stringBuilder.Append( '.' );
				}
				else
					appendNamespaceAndDot( stringBuilder, type );

				if( type.IsGenericType )
				{
					string typeName = type.Name;
					int indexOfTick = typeName.LastIndexOf( '`' );
					SysDiag.Debug.Assert( indexOfTick == typeName.IndexOf( '`' ) );
					if( indexOfTick == -1 )
						stringBuilder.Append( typeName );
					else
					{
						stringBuilder.Append( typeName.Substring( 0, indexOfTick ) );
						stringBuilder.Append( '<' );
						Sys.Type[] arguments = type.GetGenericArguments();
						int start = numberOfArgumentsToSkip( type );
						bool first = true;
						for( int i = start; i < arguments.Length; i++ )
						{
							if( first )
								first = false;
							else
								stringBuilder.Append( ',' );
							Sys.Type argument = arguments[i];
							if( argument.IsGenericParameter )
							{
								int position = argument.GenericParameterPosition;
								argument = allGenericArguments[position];
							}
							recurse( argument );
						}
						stringBuilder.Append( '>' );
					}
					return;
				}

				stringBuilder.Append( type.Name.Replace( '+', '.' ) );
				return;
			}

			static bool appendTypeNameAlias( SysText.StringBuilder stringBuilder, Sys.Type type )
			{
				string? alias = getTypeNameAlias( type );
				if( alias == null )
					return false;
				stringBuilder.Append( alias );
				return true;

				static string? getTypeNameAlias( Sys.Type type )
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
					return null;
				}
			}

			static void appendNamespaceAndDot( SysText.StringBuilder stringBuilder, Sys.Type type )
			{
				string? namespaceName = type.Namespace;
				if( namespaceName != null )
				{
					stringBuilder.Append( namespaceName );
					stringBuilder.Append( '.' );
				}
			}

			static void appendArraySuffix( SysText.StringBuilder stringBuilder, int rank )
			{
				stringBuilder.Append( '[' );
				SysDiag.Debug.Assert( rank >= 1 );
				for( int i = 0; i < rank - 1; i++ )
					stringBuilder.Append( ',' );
				stringBuilder.Append( ']' );
			}

			static int numberOfArgumentsToSkip( Sys.Type type )
			{
				if( !type.IsNested )
					return 0;
				return type.DeclaringType!.GetGenericArguments().Length;
			}
		}
	}
}
