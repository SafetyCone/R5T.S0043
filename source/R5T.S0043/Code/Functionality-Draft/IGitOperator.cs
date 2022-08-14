using System;
using System.Threading.Tasks;

using R5T.T0132;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface IGitOperator : IDraftFunctionalityMarker,
		F0019.IGitOperator
	{
		public async Task<string> Clone_NonIdempotent(string repositoryName)
        {
			var cloneUrl = await Instances.GitHubOperator.GetCloneUrl(
				Instances.GitHubOwners.SafetyCone,
				repositoryName);

			var repositoryDirectoryName = repositoryName;

			var localRepositoryDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				Instances.DirectoryPaths.SafetyConeRepositoriesDirectory,
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