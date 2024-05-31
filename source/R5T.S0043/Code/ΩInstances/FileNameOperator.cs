using System;


namespace R5T.S0043
{
	public class FileNameOperator : IFileNameOperator
	{
		#region Infrastructure

	    public static FileNameOperator Instance { get; } = new();

	    private FileNameOperator()
	    {
        }

	    #endregion
	}
}