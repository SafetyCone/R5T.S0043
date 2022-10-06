using System;

using R5T.T0132;
using R5T.T0146;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface ILocalRepositoryOperator : IFunctionalityMarker,
		F0042.ILocalRepositoryOperator
	{
		/// <summary>
		/// Verifies that the local repository directory does not exist.
		/// </summary>
		/// <returns>The local repository directory path.</returns>
		public Result<string> VerifyRepositoryDoesNotExist_Result(
			string repositoryOwnerName,
			string repositoryName)
		{
			var repositoryDirectoryPath = Instances.DirectoryPathOperator.GetRepositoryDirectory(
				repositoryOwnerName,
				repositoryName);

			var verifyResult = this.VerifyRepositoryDoesNotExist_Result(repositoryDirectoryPath);

			var output = T0146.Instances.ResultOperator.Result(verifyResult, repositoryDirectoryPath);
			return output;
		}

		public Result VerifyRepositoryDoesNotExist_Result(string repositoryDirectoryPath)
		{
			var directoryExists = Instances.FileSystemOperator.DirectoryExists(repositoryDirectoryPath);

			var result = T0146.Instances.ResultOperator.Result("Verify local repository does not exist");

			if(directoryExists)
            {
				result.WithFailure($"Local repository directory already exists: {repositoryDirectoryPath}");
			}
            else
            {
				result.WithSuccess($"Local repository directory does not exist: {repositoryDirectoryPath}");
			}

			return result;
		}
	}
}