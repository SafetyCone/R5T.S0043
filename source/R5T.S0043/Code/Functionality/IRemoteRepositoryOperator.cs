using System;
using System.Threading.Tasks;

using R5T.T0132;
using R5T.T0146;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IRemoteRepositoryOperator : IFunctionalityMarker,
		F0042.IRemoteRepositoryOperator
	{
		public Task<Result> VerifyRepositoryDoesNotExist_Result(
			string repositoryOwnerName,
			string repositoryName)
		{
			return Instances.GitHubOperator.VerifyRepositoryDoesNotExist_Result(
				repositoryOwnerName,
				repositoryName);
		}
	}
}