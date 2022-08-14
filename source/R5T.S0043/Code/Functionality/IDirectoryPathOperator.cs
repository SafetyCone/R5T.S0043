using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IDirectoryPathOperator : IFunctionalityMarker
	{
		public string GetRepositoryDirectory(string repositoryOwnerName, string repositoryName)
        {
			var repositoryOwnerDirectoryName = Instances.DirectoryNameOperator.GetRepositoryOwnerDirectoryName(repositoryOwnerName);
			var repositoryDirectoryName = Instances.DirectoryNameOperator.GetRepositoryDirectoryName(repositoryName);

			var gitHubRepositoriesDirectoryPath = Instances.DirectoryPaths.GitHubRepositoriesDirectory;

			var output = Instances.PathOperator.GetDirectoryPath(
				gitHubRepositoriesDirectoryPath,
				repositoryOwnerDirectoryName,
				repositoryDirectoryName);

			return output;
        }
	}
}