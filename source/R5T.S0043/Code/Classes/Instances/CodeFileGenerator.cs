using System;


namespace R5T.S0043
{
	public class CodeFileGenerator : ICodeFileGenerator
	{
		#region Infrastructure

	    public static CodeFileGenerator Instance { get; } = new();

	    private CodeFileGenerator()
	    {
        }

	    #endregion
	}
}