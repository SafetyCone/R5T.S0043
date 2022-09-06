using System;


namespace R5T.S0043
{
	public class GitHubOperator : IGitHubOperator
	{
		#region Infrastructure

	    public static GitHubOperator Instance { get; } = new();

	    private GitHubOperator()
	    {
        }

	    #endregion
	}
}