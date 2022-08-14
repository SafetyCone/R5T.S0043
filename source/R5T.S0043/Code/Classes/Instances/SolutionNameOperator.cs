using System;


namespace R5T.S0043
{
	public class SolutionNameOperator : ISolutionNameOperator
	{
		#region Infrastructure

	    public static SolutionNameOperator Instance { get; } = new();

	    private SolutionNameOperator()
	    {
        }

	    #endregion
	}
}