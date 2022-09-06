using System;
using System.Threading.Tasks;

using R5T.T0132;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface IGitOperator : IDraftFunctionalityMarker,
		F0019.IGitOperator
	{
		public Task<string> Clone_NonIdempotent(string repositoryName)
        {
			return this.Clone_NonIdempotent(
				repositoryName,
				Instances.GitHubOwners.SafetyCone);
        }

		public async Task<string> Clone_NonIdempotent(
			string repositoryName,
			string repositoryOwnerName)
		{
			var cloneUrl = await Instances.GitHubOperator.GetCloneUrl(
				repositoryOwnerName,
				repositoryName);

			var ownerDirectoryName = Instances.DirectoryNameOperator.GetRepositoryOwnerDirectoryName(repositoryOwnerName);

			var localOwnerRepositoryDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				Instances.DirectoryPaths.GitHubRepositoriesDirectory,
				ownerDirectoryName);

			var repositoryDirectoryName = Instances.DirectoryNameOperator.GetRepositoryDirectoryName(repositoryName);

			var localRepositoryDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				localOwnerRepositoryDirectoryPath,
				repositoryDirectoryName);

			var authentication = await Instances.GitHubOperator.GetGitHubAuthentication();

			var _ = this.Clone_NonIdempotent(
				cloneUrl,
				localRepositoryDirectoryPath,
				authentication);

			return localRepositoryDirectoryPath;
        }
	}
}