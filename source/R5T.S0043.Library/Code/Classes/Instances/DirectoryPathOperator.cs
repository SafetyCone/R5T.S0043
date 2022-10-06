using System;


namespace R5T.S0043.Library
{
	public class DirectoryPathOperator : IDirectoryPathOperator
	{
		#region Infrastructure

	    public static IDirectoryPathOperator Instance { get; } = new DirectoryPathOperator();

	    private DirectoryPathOperator()
	    {
        }

	    #endregion
	}
}