using System;


namespace R5T.S0043
{
	public class SolutionOperator : ISolutionOperator
	{
		#region Infrastructure

	    public static SolutionOperator Instance { get; } = new();

	    private SolutionOperator()
	    {
        }

	    #endregion
	}
}