using System;


namespace R5T.S0043
{
	public class DirectoryNames : IDirectoryNames
	{
		#region Infrastructure

	    public static DirectoryNames Instance { get; } = new();

	    private DirectoryNames()
	    {
        }

	    #endregion
	}
}