using System;


namespace R5T.S0043
{
	public class RepositoryTestOperations : IRepositoryTestOperations
	{
		#region Infrastructure

	    public static RepositoryTestOperations Instance { get; } = new();

	    private RepositoryTestOperations()
	    {
        }

	    #endregion
	}
}