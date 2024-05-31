using System;


namespace R5T.S0043
{
	public class ProjectFileGenerator : IProjectFileGenerator
	{
		#region Infrastructure

	    public static ProjectFileGenerator Instance { get; } = new();

	    private ProjectFileGenerator()
	    {
        }

	    #endregion
	}
}