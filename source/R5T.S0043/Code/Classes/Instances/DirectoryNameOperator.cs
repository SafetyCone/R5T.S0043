using System;


namespace R5T.S0043
{
	public class DirectoryNameOperator : IDirectoryNameOperator
	{
		#region Infrastructure

	    public static DirectoryNameOperator Instance { get; } = new();

	    private DirectoryNameOperator()
	    {
        }

	    #endregion
	}
}