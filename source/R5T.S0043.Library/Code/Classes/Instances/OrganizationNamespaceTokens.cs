using System;


namespace R5T.S0043.Library
{
	public class OrganizationNamespaceTokens : IOrganizationNamespaceTokens
	{
		#region Infrastructure

	    public static IOrganizationNamespaceTokens Instance { get; } = new OrganizationNamespaceTokens();

	    private OrganizationNamespaceTokens()
	    {
        }

	    #endregion
	}
}