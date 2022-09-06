using System;


namespace R5T.S0043
{
	public class LibraryOperator : ILibraryOperator
	{
		#region Infrastructure

	    public static LibraryOperator Instance { get; } = new();

	    private LibraryOperator()
	    {
        }

	    #endregion
	}
}