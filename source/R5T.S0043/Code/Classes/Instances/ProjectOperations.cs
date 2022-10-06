using System;


namespace R5T.S0043
{
	public class ProjectOperations : IProjectOperations
	{
		#region Infrastructure

	    public static ProjectOperations Instance { get; } = new();

	    private ProjectOperations()
	    {
        }

	    #endregion
	}
}