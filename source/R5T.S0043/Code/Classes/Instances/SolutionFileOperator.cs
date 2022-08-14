using System;


namespace R5T.S0043
{
	public class SolutionFileOperator : ISolutionFileOperator
	{
		#region Infrastructure

	    public static SolutionFileOperator Instance { get; } = new();

	    private SolutionFileOperator()
	    {
        }

	    #endregion
	}
}