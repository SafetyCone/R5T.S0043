using System;


namespace R5T.S0043
{
	public class GitHubLicenseIdentifiers : IGitHubLicenseIdentifiers
	{
		#region Infrastructure

	    public static GitHubLicenseIdentifiers Instance { get; } = new();

	    private GitHubLicenseIdentifiers()
	    {
        }

	    #endregion
	}
}