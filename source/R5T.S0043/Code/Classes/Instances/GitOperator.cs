using System;


namespace R5T.S0043
{
	public class GitOperator : IGitOperator
	{
		#region Infrastructure

	    public static GitOperator Instance { get; } = new();

	    private GitOperator()
	    {
        }

	    #endregion
	}
}