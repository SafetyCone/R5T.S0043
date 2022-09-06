using System;


namespace R5T.S0043
{
	public class DirectoryPaths : IDirectoryPaths
	{
		#region Infrastructure

	    public static DirectoryPaths Instance { get; } = new();

	    private DirectoryPaths()
	    {
        }

	    #endregion
	}
}