using System;


namespace R5T.S0043
{
	public class FileNames : IFileNames
	{
		#region Infrastructure

	    public static FileNames Instance { get; } = new();

	    private FileNames()
	    {
        }

	    #endregion
	}
}