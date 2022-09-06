using System;


namespace R5T.S0043
{
	public class ProjectOperator : IProjectOperator
	{
		#region Infrastructure

	    public static ProjectOperator Instance { get; } = new();

	    private ProjectOperator()
	    {
        }

	    #endregion
	}
}