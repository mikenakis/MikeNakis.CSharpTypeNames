namespace MikeNakis.CSharpTypeNames.Extensions;

public static class Extensions
{
	///<summary>Computes the name of the type in human-readable C# notation.</summary>
	///<remarks>See also <seealso cref="CSharpTypeNameGenerator.GetCSharpTypeName(System.Type, bool)" /></remarks>
	public static string GetCSharpName( this Sys.Type type, bool useAliases = true )
	{
		return CSharpTypeNameGenerator.GetCSharpTypeName( type, useAliases );
	}
}
