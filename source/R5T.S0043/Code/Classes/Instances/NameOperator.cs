using System;


namespace R5T.S0043
{
	public class NameOperator : INameOperator
	{
		#region Infrastructure

	    public static NameOperator Instance { get; } = new();

	    private NameOperator()
	    {
        }

	    #endregion
	}
}