using System;


namespace R5T.S0043
{
	public class SolutionFolderName : ISolutionFolderName
	{
		#region Infrastructure

	    public static SolutionFolderName Instance { get; } = new();

	    private SolutionFolderName()
	    {
        }

	    #endregion
	}
}