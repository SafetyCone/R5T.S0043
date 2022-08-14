using System;


namespace R5T.S0043
{
	public class TemplateFilePaths : ITemplateFilePaths
	{
		#region Infrastructure

	    public static TemplateFilePaths Instance { get; } = new();

	    private TemplateFilePaths()
	    {
        }

	    #endregion
	}
}