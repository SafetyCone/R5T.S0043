using System;


namespace R5T.S0043
{
	public class GitHubOwners : IGitHubOwners
	{
		#region Infrastructure

	    public static GitHubOwners Instance { get; } = new();

	    private GitHubOwners()
	    {
        }

	    #endregion
	}
}