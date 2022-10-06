using System;


namespace R5T.S0043.Library
{
	public class GitHubOwnerOperator : IGitHubOwnerOperator
	{
		#region Infrastructure

	    public static IGitHubOwnerOperator Instance { get; } = new GitHubOwnerOperator();

	    private GitHubOwnerOperator()
	    {
        }

	    #endregion
	}
}