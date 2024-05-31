using System;


namespace R5T.S0043
{
	public class ServicesOperator : IServicesOperator
	{
		#region Infrastructure

	    public static ServicesOperator Instance { get; } = new();

	    private ServicesOperator()
	    {
        }

	    #endregion
	}
}