using System;


namespace R5T.S0043
{
	public class RepositoryOperations : IRepositoryOperator
	{
		#region Infrastructure

	    public static RepositoryOperations Instance { get; } = new();

	    private RepositoryOperations()
	    {
        }

	    #endregion
	}
}