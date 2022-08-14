using System;


namespace R5T.S0043
{
	public class RemoteRepositoryOperator : IRemoteRepositoryOperator
	{
		#region Infrastructure

	    public static RemoteRepositoryOperator Instance { get; } = new();

	    private RemoteRepositoryOperator()
	    {
        }

	    #endregion
	}
}