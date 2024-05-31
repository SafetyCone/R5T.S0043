using System;


namespace R5T.S0043
{
	public class LicenseIdentifiers : ILicenseIdentifiers
	{
		#region Infrastructure

	    public static ILicenseIdentifiers Instance { get; } = new LicenseIdentifiers();

	    private LicenseIdentifiers()
	    {
        }

	    #endregion
	}
}