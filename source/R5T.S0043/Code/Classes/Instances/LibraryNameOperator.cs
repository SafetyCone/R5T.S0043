using System;


namespace R5T.S0043
{
	public class LibraryNameOperator : ILibraryNameOperator
	{
		#region Infrastructure

	    public static LibraryNameOperator Instance { get; } = new();

	    private LibraryNameOperator()
	    {
        }

	    #endregion
	}
}