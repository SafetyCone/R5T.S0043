using System;


namespace R5T.S0043.Library
{
	public class GitHubOwnerNames : IGitHubOwnerNames
	{
		#region Infrastructure

	    public static IGitHubOwnerNames Instance { get; } = new GitHubOwnerNames();

	    private GitHubOwnerNames()
	    {
        }

	    #endregion
	}
}