using System;


namespace R5T.S0043
{
	public class DirectoryPathOperator : IDirectoryPathOperator
	{
		#region Infrastructure

	    public static DirectoryPathOperator Instance { get; } = new();

	    private DirectoryPathOperator()
	    {
        }

	    #endregion
	}
}