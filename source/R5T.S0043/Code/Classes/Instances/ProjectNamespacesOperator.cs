using System;


namespace R5T.S0043
{
	public class ProjectNamespacesOperator : IProjectNamespacesOperator
	{
		#region Infrastructure

	    public static ProjectNamespacesOperator Instance { get; } = new();

	    private ProjectNamespacesOperator()
	    {
        }

	    #endregion
	}
}