using System;


namespace R5T.S0043
{
	public class ProjectFileOperator : IProjectFileOperator
	{
		#region Infrastructure

	    public static ProjectFileOperator Instance { get; } = new();

	    private ProjectFileOperator()
	    {
        }

	    #endregion
	}
}