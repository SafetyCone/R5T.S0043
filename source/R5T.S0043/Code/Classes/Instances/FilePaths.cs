using System;


namespace R5T.S0043
{
	public class FilePaths : IFilePaths
	{
		#region Infrastructure

	    public static FilePaths Instance { get; } = new();

	    private FilePaths()
	    {
        }

	    #endregion
	}
}