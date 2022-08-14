using System;


namespace R5T.S0043
{
	public class TextFileGenerator : ITextFileGenerator
	{
		#region Infrastructure

	    public static TextFileGenerator Instance { get; } = new();

	    private TextFileGenerator()
	    {
        }

	    #endregion
	}
}