using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.F0041;
using R5T.T0132;
using R5T.T0146;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IRepositoryOperator : IFunctionalityMarker,
		F0042.IRepositoryOperator
	{
		/// <summary>
		/// Creates a repository and returns the local repository directory path.
		/// </summary>
		/// <returns>A result containing the local repository directory path.</returns>
		public async Task<Result<string>> CreateNew_NonIdempotent_Result(
			GitHubRepositorySpecification repositorySpecification,
			ILogger logger)
		{
			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(
				repositorySpecification.Organization,
				repositorySpecification.Name);

			var repositoryName = repositorySpecification.Name;

			logger.LogInformation($"Creating new remote GitHub repository '{ownedRepositoryName}'...");

			var createRepositoryResult = await Instances.GitHubOperator.CreateRepository_NonIdempotent_Result(repositorySpecification);

			IReason createRepositoryReason = createRepositoryResult.IsSuccess()
				? T0146.Instances.ReasonOperator.Success("Created remote repository.")
				: T0146.Instances.ReasonOperator.Failure("Failed to create remote repository.", createRepositoryResult.Failures)
				;

			logger.LogInformation($"Created new remote GitHub repository '{ownedRepositoryName}'.");

			// Clone local.
			logger.LogInformation($"Cloning to local directory repository...");

			var cloneLocalResult = await Instances.GitOperator.Clone_NonIdempotent_Result(
				repositoryName,
				repositorySpecification.Organization);

			IReason cloneLocalReason = createRepositoryResult.IsSuccess()
				? T0146.Instances.ReasonOperator.Success("Cloned remote repository to local directory.")
				: T0146.Instances.ReasonOperator.Failure("Failed to clone remote repository to local directory.", cloneLocalResult.Failures)
				;

			logger.LogInformation($"Cloned to local directory repository.");

			logger.LogInformation($"New empty repository created.");

			var result = T0146.Instances.ResultOperator.Result<string>(cloneLocalResult.Value)
				.WithTitle("Create new repository")
				.WithReasons(createRepositoryReason, cloneLocalReason)
				.WithChildren(createRepositoryResult, cloneLocalResult)
				;

			return result;
		}

		public Result<string> Create_GitIgnoreFile_Idempotent_Result(
			string repositoryDirectoryPath,
			ILogger logger)
		{
			var result = T0146.Instances.ResultOperator.Result<string>()
				.WithTitle("Create GitIgnore File")
				;

			var gitIgnoreFilePath = Instances.RepositoryPathsOperator.GetGitIgnoreFilePath(repositoryDirectoryPath);

			result.WithValue(gitIgnoreFilePath);

			logger.LogInformation("Checking if gitignore file exists...");

			var gitIgnoreFileExistsResult = Instances.FileSystemOperator.FileExists_Result(gitIgnoreFilePath);

			var gitIgnoreFileExistsReason = gitIgnoreFileExistsResult.Value
				? T0146.Instances.ReasonOperator.Success($"GitIgnore file exists: {gitIgnoreFilePath}")
				: T0146.Instances.ReasonOperator.Success($"GitIgnore file does not exist: {gitIgnoreFilePath}")
				;

			result.WithReason(gitIgnoreFileExistsReason).WithChild(gitIgnoreFileExistsResult);

			var gitIgnoreFileExists = gitIgnoreFileExistsResult.Value;
			if (gitIgnoreFileExists)
			{
				logger.LogInformation($"Gitignore file exists:{Environment.NewLine}\t{gitIgnoreFilePath}");

				result.WithReason(
					T0146.Instances.ReasonOperator.Success("GitIgnore file already exists, no need to create it."));
			}
			else
			{
				logger.LogInformation($"Gitignore file does not exist. Copying template file...{Environment.NewLine}\tSource: {Instances.TemplateFilePaths.GitIgnoreTemplateFile}{Environment.NewLine}\tDestination: {gitIgnoreFilePath}");

				var copyGitIgnoreTemplateFileResult = this.CopyFile_Result(
					Instances.TemplateFilePaths.GitIgnoreTemplateFile,
					gitIgnoreFilePath);

				IReason copyGitIgnoreTemplateFileReason = copyGitIgnoreTemplateFileResult.IsSuccess()
					? Instances.ReasonOperator.Success("Copied gitignore file template.")
					: Instances.ReasonOperator.Failure("Failed to copy gitignore file template.", copyGitIgnoreTemplateFileResult.Failures)
					;

				result.WithReason(copyGitIgnoreTemplateFileReason).WithChild(copyGitIgnoreTemplateFileResult);

				logger.LogInformation($"Copied gitignore file:{Environment.NewLine}\t{gitIgnoreFilePath}");
			}

			return result;
		}

		public Result CopyFile_Result(
			string sourceFilePath,
			string destinationFilePath)
        {
			var result = Instances.ResultOperator.Result()
				.WithTitle("Copy File")
				.WithMetadata("Source File Path", sourceFilePath)
				.WithMetadata("Destination File Path", destinationFilePath)
				;

			try
			{
				Instances.FileSystemOperator.CopyFile(
						sourceFilePath,
						destinationFilePath);

				result.WithReason(Instances.ReasonOperator.Success($"Successfully copied file."));
			}
			catch (Exception exception)
            {
				result.WithReason(Instances.ReasonOperator.Failure($"Failed to copied file.", exception));
			}

			return result;
		}

		public Result<string> Create_SourceDirectory_Idempotent_Result(
			string repositoryDirectoryPath,
			ILogger logger)
		{
			var result = Instances.ResultOperator.Result<string>()
				.WithTitle("Create Source Directory")
				.WithMetadata("Repository Directory path", repositoryDirectoryPath);
			;

			var repositorySourceDirectoryPath = Instances.RepositoryPathsOperator.GetSourceDirectoryPath(repositoryDirectoryPath);

			result.WithValue(repositorySourceDirectoryPath);

			logger.LogInformation("Checking if repository source directory exists.");

			var repositorySourceDirectoryExistsResult = this.DirectoryExists_Result(repositorySourceDirectoryPath);
			var repositorySourceDirectoryExists = repositorySourceDirectoryExistsResult.Value;

			var successMessage = repositorySourceDirectoryExists
				? "Source directory already exists."
				: "Source directory does not already exist."
				;

			result
				.WithReason(Instances.ReasonOperator.Success(successMessage))
				.WithChild(repositorySourceDirectoryExistsResult)
				;

			if (repositorySourceDirectoryExists)
			{
				logger.LogInformation($"Repository source directory exists:{Environment.NewLine}\t{repositorySourceDirectoryPath}");

				result.WithReason(Instances.ReasonOperator.Success("Source directory already exists, no need to create it."));
			}
			else
			{
				logger.LogInformation($"Repository source directory does not exist. Creating directory...{Environment.NewLine}\t{repositorySourceDirectoryPath}");

				var createDirectoryResult = this.CreateDirectory_Result(repositorySourceDirectoryPath);

				result.WithChild(createDirectoryResult);

				if(createDirectoryResult.IsSuccess())
                {
					logger.LogInformation($"Created repository source directory:{Environment.NewLine}\t{repositorySourceDirectoryPath}");

					result.WithSuccess("Created source directory.");
                }
				else
                {
					logger.LogError($"Failed to create repository source directory:{Environment.NewLine}\t{repositorySourceDirectoryPath}");

					result.WithFailure("Failed to create source directory.", createDirectoryResult.Failures);
				}
			}

			return result;
		}

		public Result CreateDirectory_Result(string directoryPath)
        {
			var result = Instances.ResultOperator.Result()
				.WithTitle("Create Directory")
				.WithMetadata("Directory path", directoryPath)
				;

			try
			{
				F0000.Instances.FileSystemOperator.CreateDirectory(directoryPath);

				result.WithSuccess("Created directory.");
			}
			catch (Exception exception)
            {
				result.WithFailure("Unable to create directory.", exception);
            }

			return result;
        }

		public Result<bool> DirectoryExists_Result(string directoryPath)
		{
			var directoryExists = F0000.Instances.FileSystemOperator.DirectoryExists(directoryPath);

			// Always success, whether or not the directory exists is a different story.
			var successMessage = directoryExists
				? $"Directory exists."
				: $"Directory does not exist, or there was an error (perhaps pemissions?) accessing it."
				;

			var result = Instances.ResultOperator.Result<bool>()
				.WithTitle("Check Directory Exists")
				.WithValue(directoryExists)
				.WithSuccess(successMessage)
				;

			return result;
		}

		public async Task<Result> Delete_Idempotent_Result(
			string repositoryName,
			string repositoryOwnerName,
			ILogger logger)
		{
			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(repositoryOwnerName, repositoryName);

			var repositoryDirectoryPath = Instances.DirectoryPathOperator.GetRepositoryDirectory(repositoryOwnerName, repositoryName);

			logger.LogInformation($"Deleting repository '{repositoryName}'...");

			// Delete local.
			logger.LogInformation("Deleting local directory repository...");

			var deleteLocalDirectoryResult = this.DeleteDirectory_OkIfNotExists_Result(repositoryDirectoryPath);

			var message = deleteLocalDirectoryResult.Value
				? $"Deleted local repository directory: {repositoryDirectoryPath}"
				: $"Local repository directory already did not exist. No need to delete: {repositoryDirectoryPath}"
				;

			var deleteLocalDirectoryReason = T0146.Instances.ReasonOperator.Success(message);

			logger.LogInformation("Deleted local directory repository.");

			// Delete remote.
			logger.LogInformation("Deleting remote GitHub repository...");

			var deleteRepositoryResult = await Instances.GitHubOperator.DeleteRepository_Idempotent_Result(
				Instances.GitHubOwners.SafetyCone,
				repositoryName);

			var successMessage = deleteLocalDirectoryResult.Value
				? $"Deleted remote repository: {ownedRepositoryName}"
				: $"Remote repository already did not exist. No need to delete: {ownedRepositoryName}"
				;

			IReason deleteRepositoryReason = deleteRepositoryResult.IsSuccess()
				? T0146.Instances.ReasonOperator.Success(successMessage)
				: T0146.Instances.ReasonOperator.Failure($"Unable to delete remote repository: {ownedRepositoryName}")
				;

			logger.LogInformation("Deleted remote GitHub repository.");

			logger.LogInformation($"Deleted repository '{repositoryName}'.");

			var result = T0146.Instances.ResultOperator.Result()
				.WithTitle("Delete Repository")
				.WithReasons(deleteLocalDirectoryReason, deleteRepositoryReason)
				.WithChildren(deleteLocalDirectoryResult, deleteRepositoryResult)
				;

			return result;
		}

		public Result<bool> DeleteDirectory_OkIfNotExists_Result(string directoryPath)
		{
			var directoryExists = Instances.FileSystemOperator.DirectoryExists(directoryPath);

			Instances.FileSystemOperator.DeleteDirectory_OkIfNotExists(directoryPath);

			var message = directoryExists
				? $"Deleted directory: {directoryPath}"
				: $"Directory already did not exist; no need to delete: {directoryPath}"
				;

			var result = T0146.Instances.ResultOperator.Result<bool>()
				.WithTitle("Delete Directory")
				.WithValue(directoryExists)
				.WithSuccess(message)
				;

			return result;
		}

		public Result PerformInitialCommit(
			string repositoryLocalDirectoryPath,
			ILogger logger)
		{
			var result = Instances.ResultOperator.Result<bool>()
				.WithTitle("Perform Initial Commit")
				.WithMetadata("Repository directory path", repositoryLocalDirectoryPath)
				;

			var pushAllChangesResult = Instances.GitHubOperator.PushAllChanges(
				repositoryLocalDirectoryPath,
				Instances.CommitMessages.InitialCommit,
				logger);

			IReason pushAllChangesReason = pushAllChangesResult.IsSuccess()
				? Instances.ReasonOperator.Success("Push all changes succeeded.")
				: Instances.ReasonOperator.Failure($"Push all changes failed.", pushAllChangesResult.Failures)
				;

			result.WithChild(pushAllChangesResult).WithReason(pushAllChangesReason);

			return result;
		}

		public Result<(string gitIgnoreFilePath, string sourceDirectoryPath)> SetupRepository_Result(
			string repositoryDirectoryPath,
			ILogger logger)
		{
			var result = Instances.ResultOperator.Result<(string gitIgnoreFilePath, string sourceDirectoryPath)>()
				.WithTitle("Setup Repository")
				.WithMetadata("Repository directory path", repositoryDirectoryPath)
				;

			// Gitignore file.
			var createGitIgnoreFileResult = Instances.RepositoryOperator.Create_GitIgnoreFile_Idempotent_Result(
				repositoryDirectoryPath,
				logger);

			IReason createGitIgnoreFileReason = createGitIgnoreFileResult.IsSuccess()
				? Instances.ReasonOperator.Success("Created GitIgnore file.")
				: Instances.ReasonOperator.Failure("Failed to create GitIgnore file.", createGitIgnoreFileResult.Failures)
				;

			result.WithReason(createGitIgnoreFileReason).WithChild(createGitIgnoreFileResult);

			// Create repository source directory.
			var createSourceDirectoryResult = Instances.RepositoryOperator.Create_SourceDirectory_Idempotent_Result(
				repositoryDirectoryPath,
				logger);

			IReason createSourceDirectoryReason = createGitIgnoreFileResult.IsSuccess()
				? Instances.ReasonOperator.Success("Created source directory.")
				: Instances.ReasonOperator.Failure("Failed to create source directory.", createSourceDirectoryResult.Failures)
				;

			result.WithReason(createSourceDirectoryReason).WithChild(createSourceDirectoryResult);

			result.WithValue((createGitIgnoreFileResult.Value, createSourceDirectoryResult.Value));

			return result;
		}

		public async Task<Result> Verify_RepositoryDoesNotExist_Result(
			string repositoryOwnerName,
			string repositoryName)
		{
			var verifyRemoteResult = await Instances.RemoteRepositoryOperator.VerifyRepositoryDoesNotExist_Result(
				repositoryOwnerName,
				repositoryName);

			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(repositoryOwnerName, repositoryName);

			IReason verifyRemoteReason = verifyRemoteResult.IsSuccess()
				? T0146.Instances.ReasonOperator.Success($"Remote repository does not exist: {ownedRepositoryName}.")
				: T0146.Instances.ReasonOperator.Failure($"Remote repository already exists: {ownedRepositoryName}.", verifyRemoteResult.Failures)
				;				

			var verifyLocalResult = Instances.LocalRepositoryOperator.VerifyRepositoryDoesNotExist_Result(
				repositoryOwnerName,
				repositoryName);

			IReason verifyLocalReason = verifyRemoteResult.IsSuccess()
				? T0146.Instances.ReasonOperator.Success($"Local repository does not exist: {verifyLocalResult.Value}.")
				: T0146.Instances.ReasonOperator.Failure($"Local repository already exists: {verifyLocalResult.Value}.", verifyLocalResult.Failures)
				;

			var result = T0146.Instances.ResultOperator.Result("Verify Repository does not Exist")
				.WithReasons(verifyRemoteReason, verifyLocalReason)
				;
				// Add reasons.
				
			result.Children.AddRange(verifyRemoteResult, verifyLocalResult);

			return result;
		}
	}
}