namespace MikeNakis.CSharpTypeNames.Extensions;

public static class Extensions
{
	/// <summary>Generates the human-readable name of the <see cref="Sys.Type"/> using C# notation.</summary>
	/// <param name="type">The <see cref="Sys.Type"/> whose name is to be generated.</param>
	/// <param name="options">Specifies how the name will be generated. Default is <see cref="Options.None"/>.</param>
	///<remarks>See also <seealso cref="CSharpTypeNameGenerator.GetCSharpTypeName(System.Type, Options)" /></remarks>
	public static string GetCSharpName( this Sys.Type type, Options options = Options.None )
	{
		return CSharpTypeNameGenerator.GetCSharpTypeName( type, options );
	}
}
