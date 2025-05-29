namespace MikeNakis.CSharpTypeNames.Extensions;

public static class Extensions
{
	/// <summary>Generates the human-readable name of the <see cref="Sys.Type"/> using C# notation.</summary>
	/// <param name="type">The <see cref="Sys.Type"/> whose name is to be generated.</param>
	///<remarks>See also <seealso cref="Generator.GetCSharpTypeName(System.Type, Options)" /></remarks>
	public static string GetCSharpName( this Sys.Type type )
	{
		//TODO: invert the meaning of the options, so that here we can specify Options.None
		return Generator.GetCSharpTypeName( type, //
				Options.UseLanguageKeywordsForBuiltInTypes | //
				Options.UseLanguageKeywordsForNativeIntegers | //
				Options.UseNullableShorthandNotation | //
				Options.UseTupleShorthandNotation );
	}
}
