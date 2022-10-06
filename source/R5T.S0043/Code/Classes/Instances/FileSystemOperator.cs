using System;


namespace R5T.S0043
{
	public class FileSystemOperator : IFileSystemOperator
	{
		#region Infrastructure

	    public static FileSystemOperator Instance { get; } = new();

	    private FileSystemOperator()
	    {
        }

	    #endregion
	}
}