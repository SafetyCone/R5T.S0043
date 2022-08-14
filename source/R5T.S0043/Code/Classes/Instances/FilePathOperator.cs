using System;


namespace R5T.S0043
{
	public class FilePathOperator : IFilePathOperator
	{
		#region Infrastructure

	    public static FilePathOperator Instance { get; } = new();

	    private FilePathOperator()
	    {
        }

	    #endregion
	}
}