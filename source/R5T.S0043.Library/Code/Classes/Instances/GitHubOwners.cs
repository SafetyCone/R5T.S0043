using System;


namespace R5T.S0043.Library
{
	public class GitHubOwners : IGitHubOwners
	{
		#region Infrastructure

	    public static IGitHubOwners Instance { get; } = new GitHubOwners();

	    private GitHubOwners()
	    {
        }

	    #endregion
	}
}