using System;
using System.Threading.Tasks;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IRemoteRepositoryOperator : IFunctionalityMarker
	{
		public async Task<bool> RepositoryExists(string repositoryOwnerName, string repositoryName)
        {
			var output = await Instances.GitHubOperator.RepositoryExists(
				repositoryOwnerName,
				repositoryName);

			return output;
        }

		public Task VerifyRepositoryDoesNotExist(string repositoryOwnerName, string repositoryName)
        {
			return Instances.GitHubOperator.VerifyRepositoryDoesNotExist(repositoryOwnerName, repositoryName);
        }
	}
}