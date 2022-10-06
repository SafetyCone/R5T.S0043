using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ISolutionPathsOperator : IFunctionalityMarker
	{
		public string GetSolutionFilePath(
			string solutionDirectoryPath,
			string solutionName)
        {
			var solutionFileName = Instances.FileNameOperator.GetSolutionFileName_FromSolutionName(solutionName);

			var solutionFilePath = Instances.PathOperator.GetFilePath(
				solutionDirectoryPath,
				solutionFileName);

			return solutionFilePath;
		}

		public string GetSolutionDirectoryPath_FromRepositorySourceDirectoryPath(string repositorySourceDirectoryPath)
        {
			// The solution directory path is just the repository source directory path.
			var solutionDirectoryPath = repositorySourceDirectoryPath;
			return solutionDirectoryPath;
        }

		public string GetSolutionDirectoryPath_FromSolutionFilePath(string solutionFilePath)
		{
			var solutionDirectoryPath = F0002.Instances.PathOperator.GetParentDirectoryPath_ForFile(solutionFilePath);
			return solutionDirectoryPath;
		}
	}
}