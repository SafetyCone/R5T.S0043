using System;
using System.Threading.Tasks;

using R5T.T0046;
using R5T.T0132;
using R5T.T0144;
using R5T.T0146;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface IGitOperator : IDraftFunctionalityMarker,
		F0041.IGitOperator
	{
		/// <summary>
		/// Clones a remote repository to a local directory in the GitHub repositories directory.
		/// Returns the local repository directory path.
		/// </summary>
		/// <returns>A result containing the local repository directory path.</returns>
		public async Task<Result<string>> Clone_NonIdempotent_Result(
			string repositoryName,
			string repositoryOwnerName)
		{
			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(repositoryOwnerName, repositoryName);

			var cloneUrl = await Instances.GitHubOperator.GetCloneUrl(
				repositoryOwnerName,
				repositoryName);

			var ownerDirectoryName = Instances.RepositoryDirectoryNameOperator.GetRepositoryOwnerDirectoryName(repositoryOwnerName);

			var localOwnerRepositoryDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				Instances.DirectoryPaths.GitHubRepositoriesDirectory,
				ownerDirectoryName);

			var repositoryDirectoryName = Instances.RepositoryDirectoryNameOperator.GetRepositoryDirectoryName(repositoryName);

			var localRepositoryDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				localOwnerRepositoryDirectoryPath,
				repositoryDirectoryName);

			var authentication = await Instances.GitHubOperator.GetGitHubAuthentication();

			var result = T0146.Instances.ResultOperator.Result<string>()
				.WithTitle("Clone GitHub Repository Locally")
				;

			try
			{
				var _ = this.Clone_NonIdempotent(
					cloneUrl,
					localRepositoryDirectoryPath,
					authentication);

				result
					.WithValue(localRepositoryDirectoryPath)
					.WithSuccess($"Cloned GitHub repository '{ownedRepositoryName}' to local directory:\n{localRepositoryDirectoryPath}")
					;
			}
			catch (Exception exception)
            {
				result.WithFailure($"Failed to clone GitHub repository '{ownedRepositoryName}' to local directory:\n{localRepositoryDirectoryPath}", exception);
            }

			return result;
		}

		public Result Commit_Result(
			string localRepositoryDirectoryPath,
			string commitMessage,
			Author author)
        {
			var result = Instances.ResultOperator.Result<bool>()
				.WithTitle("Commit Changes")
				.WithMetadata("Repository directory path", localRepositoryDirectoryPath)
				.WithMetadata("Commit message", commitMessage)
				.WithMetadata("Author", author)
				;

			try
			{
				this.Commit(
					localRepositoryDirectoryPath,
					commitMessage,
					author);

				result.WithSuccess("Git success: committed changes.");
			}
			catch (Exception exception)
			{
				result.WithFailure("Git failure: commit changes failed.", exception);
			}

			return result;
		}

		public Result Commit_Result(
			string localRepositoryDirectoryPath,
			string commitMessage)
		{
			var author = this.GetAuthor();

			var output = this.Commit_Result(
				localRepositoryDirectoryPath,
				commitMessage,
				author);

			return output;
		}

		/// <inheritdoc cref="F0019.IGitOperator.Push(string, Authentication)"/>
		public Result Push_Result(
			string localRepositoryDirectoryPath,
			Authentication authentication)
        {
			var result = Instances.ResultOperator.Result<bool>()
				.WithTitle("Push Changes")
				.WithMetadata("Repository directory path", localRepositoryDirectoryPath)
				.WithMetadata("Username", authentication.Username)
				;

			try
			{
				this.Push(
					localRepositoryDirectoryPath,
					authentication);

				result.WithSuccess("Git success: pushed changed.");
			}
			catch (Exception exception)
			{
				result.WithFailure("Git failure: pushed changes failed.", exception);
			}

			return result;
		}

		public Result Push_Result(string localRepositoryDirectoryPath)
		{
			var authentication = Instances.GitHubOperator.GetGitHubAuthentication_Synchronous();

			var output = this.Push_Result(
				localRepositoryDirectoryPath,
				authentication);

			return output;
		}

		public Result<bool> HasUnpushedLocalChanges_Result(string repositoryDirectoryPath)
        {
			var result = Instances.ResultOperator.Result<bool>()
				.WithTitle("Check if Any Unpushed Changes")
				.WithMetadata("Repository directory path", repositoryDirectoryPath)
				;

            try
            {
				var hasAnyUnpushedLocalChanges = this.HasUnpushedLocalChanges(repositoryDirectoryPath);

				result
					.WithValue(hasAnyUnpushedLocalChanges)
					.WithSuccess("Git success: check of any unpushed changes succeeded.")
					;
            }
			catch (Exception exception)
            {
				result.WithFailure("Git failure: check of any unpushed changes failed.", exception);
            }

			return result;
        }

		public Result StageAllUnstagedPaths_Result(string repositoryDirectoryPath)
        {
			var result = Instances.ResultOperator.Result<bool>()
				.WithTitle("Stage All Unstaged Paths")
				.WithMetadata("Repository directory path", repositoryDirectoryPath)
				;

			try
			{
				this.StageAllUnstagedPaths(repositoryDirectoryPath);

				result.WithSuccess("Git success: staging all unstaged paths succeeded.");
			}
			catch (Exception exception)
			{
				result.WithFailure("Git failure: staging all unstaged paths failed.", exception);
			}

			return result;
		}
	}
}