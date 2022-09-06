using System;


namespace R5T.S0043
{
	public class RepositoryGeneration : IRepositoryOperations
	{
		#region Infrastructure

	    public static RepositoryGeneration Instance { get; } = new();

	    private RepositoryGeneration()
	    {
        }

	    #endregion
	}
}