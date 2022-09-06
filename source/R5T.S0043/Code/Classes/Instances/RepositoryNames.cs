using System;


namespace R5T.S0043
{
	public class RepositoryNames : IRepositoryNames
	{
		#region Infrastructure

	    public static RepositoryNames Instance { get; } = new();

	    private RepositoryNames()
	    {
        }

	    #endregion
	}
}