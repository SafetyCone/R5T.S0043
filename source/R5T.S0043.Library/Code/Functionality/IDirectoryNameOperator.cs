using System;

using R5T.T0132;


namespace R5T.S0043.Library
{
	[FunctionalityMarker]
	public partial interface IDirectoryNameOperator : IFunctionalityMarker
	{
		public string GetProjectDirectoryName_FromProjectName(string projectName)
        {
			// Project directory name is just project name.
			var projectDirectoryName = projectName;
			return projectDirectoryName;
        }

		public string GetRepositoryOwnerDirectoryName(string repositoryOwnerName)
        {
			// The directory name is just the name.
			var output = repositoryOwnerName;
			return output;
        }

		public string GetRepositoryDirectoryName(string repositoryName)
        {
			// The directory name is just the name.
			var output = repositoryName;
			return output;
        }
	}
}