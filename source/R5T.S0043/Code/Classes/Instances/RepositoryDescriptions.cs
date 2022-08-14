using System;


namespace R5T.S0043
{
	public class RepositoryDescriptions : IRepositoryDescriptions
	{
		#region Infrastructure

	    public static RepositoryDescriptions Instance { get; } = new();

	    private RepositoryDescriptions()
	    {
        }

	    #endregion
	}
}