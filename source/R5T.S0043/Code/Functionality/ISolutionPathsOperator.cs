using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ISolutionPathsOperator : IFunctionalityMarker
	{
		public string GetSolutionDirectoryPath_FromRepositorySourceDirectoryPath(string repositorySourceDirectoryPath)
        {
			// The solution directory path is just the repository source directory path.
			var solutionDirectoryPath = repositorySourceDirectoryPath;
			return solutionDirectoryPath;
        }
	}
}