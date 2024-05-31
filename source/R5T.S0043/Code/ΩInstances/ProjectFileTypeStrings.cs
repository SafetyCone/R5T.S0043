using System;


namespace R5T.S0043
{
	public class ProjectFileTypeStrings : IProjectFileTypeStrings
	{
		#region Infrastructure

	    public static ProjectFileTypeStrings Instance { get; } = new();

	    private ProjectFileTypeStrings()
	    {
        }

	    #endregion
	}
}