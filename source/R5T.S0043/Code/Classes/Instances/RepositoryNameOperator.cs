using System;


namespace R5T.S0043
{
	public class RepositoryNameOperator : IRepositoryNameOperator
	{
		#region Infrastructure

	    public static RepositoryNameOperator Instance { get; } = new();

	    private RepositoryNameOperator()
	    {
        }

	    #endregion
	}
}