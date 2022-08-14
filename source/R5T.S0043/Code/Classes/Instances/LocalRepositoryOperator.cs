using System;


namespace R5T.S0043
{
	public class LocalRepositoryOperator : ILocalRepositoryOperator
	{
		#region Infrastructure

	    public static LocalRepositoryOperator Instance { get; } = new();

	    private LocalRepositoryOperator()
	    {
        }

	    #endregion
	}
}