using System;


namespace R5T.S0043.Library
{
	public class RepositoryNameOperator : IRepositoryNameOperator
	{
		#region Infrastructure

	    public static IRepositoryNameOperator Instance { get; } = new RepositoryNameOperator();

	    private RepositoryNameOperator()
	    {
        }

	    #endregion
	}
}