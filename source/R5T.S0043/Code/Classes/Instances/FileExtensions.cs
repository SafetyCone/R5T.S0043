using System;


namespace R5T.S0043
{
	public class FileExtensions : IFileExtensions
	{
		#region Infrastructure

	    public static FileExtensions Instance { get; } = new();

	    private FileExtensions()
	    {
        }

	    #endregion
	}
}