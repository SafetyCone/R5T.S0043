using System;


namespace R5T.S0043
{
	public class ProjectNameOperator : IProjectNameOperator
	{
		#region Infrastructure

	    public static ProjectNameOperator Instance { get; } = new();

	    private ProjectNameOperator()
	    {
        }

	    #endregion
	}
}