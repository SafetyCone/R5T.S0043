using System;


namespace R5T.S0043.Library
{
	public class DirectoryNameOperator : IDirectoryNameOperator
	{
		#region Infrastructure

	    public static IDirectoryNameOperator Instance { get; } = new DirectoryNameOperator();

	    private DirectoryNameOperator()
	    {
        }

	    #endregion
	}
}