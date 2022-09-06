using System;


namespace R5T.S0043
{
	public class RepositoryPathsOperator : IRepositoryPathsOperator
	{
		#region Infrastructure

	    public static RepositoryPathsOperator Instance { get; } = new();

	    private RepositoryPathsOperator()
	    {
        }

	    #endregion
	}
}