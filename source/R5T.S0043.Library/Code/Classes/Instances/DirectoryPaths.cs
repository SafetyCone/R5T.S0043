using System;


namespace R5T.S0043.Library
{
	public class DirectoryPaths : IDirectoryPaths
	{
		#region Infrastructure

	    public static IDirectoryPaths Instance { get; } = new DirectoryPaths();

	    private DirectoryPaths()
	    {
        }

	    #endregion
	}
}