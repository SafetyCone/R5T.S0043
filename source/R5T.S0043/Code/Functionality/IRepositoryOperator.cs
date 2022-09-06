using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.D0082.T000;
using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IRepositoryOperator : IFunctionalityMarker
	{
		/// <summary>
		/// Creates a repository and returns the local repository directory path.
		/// </summary>
		/// <returns>The local repository directory path.</returns>
		public async Task<string> CreateNew_NonIdempotent(
			GitHubRepositorySpecification repositorySpecification,
			ILogger logger)
        {
			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(
				repositorySpecification.Organization,
				repositorySpecification.Name);

			var repositoryName = repositorySpecification.Name;

			logger.LogInformation($"Creating new remote GitHub repository '{ownedRepositoryName}'...");

			await Instances.GitHubOperator.CreateRepository_NonIdempotent(repositorySpecification);

			logger.LogInformation($"Created new remote GitHub repository '{ownedRepositoryName}'.");

			// Clone local.
			logger.LogInformation($"Cloning to local directory repository...");

			var localRepositoryDirectoryPath = await Instances.GitOperator.Clone_NonIdempotent(
				repositoryName,
				repositorySpecification.Organization);

			logger.LogInformation($"Cloned to local directory repository.");

			logger.LogInformation($"New empty repository created.");

			return localRepositoryDirectoryPath;
		}

		public string Create_GitIgnoreFile_Idempotent(
			string repositoryDirectoryPath,
			ILogger logger)
        {
			var gitIgnoreFilePath = Instances.RepositoryPathsOperator.GetGitIgnoreFilePath(repositoryDirectoryPath);

			logger.LogInformation("Checking if gitignore file exists...");

			var gitIgnoreFileExists = Instances.FileSystemOperator.FileExists(gitIgnoreFilePath);
			if (gitIgnoreFileExists)
			{
				logger.LogInformation($"Gitignore file exists:{Environment.NewLine}\t{gitIgnoreFilePath}");
			}
			else
			{
				logger.LogInformation($"Gitignore file does not exist. Copying template file...{Environment.NewLine}\tSource: {Instances.TemplateFilePaths.GitIgnoreTemplateFile}{Environment.NewLine}\tDestination: {gitIgnoreFilePath}");

				Instances.FileSystemOperator.CopyFile(
					Instances.TemplateFilePaths.GitIgnoreTemplateFile,
					gitIgnoreFilePath);

				logger.LogInformation($"Copied gitignore file:{Environment.NewLine}\t{gitIgnoreFilePath}");
			}

			return gitIgnoreFilePath;
		}

		public string Create_SourceDirectory_Idempotent(
			string repositoryDirectoryPath,
			ILogger logger)
        {
			var repositorySourceDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				repositoryDirectoryPath,
				Instances.DirectoryNames.RepositorySourceDirectoryName);

			logger.LogInformation("Checking if repository source directory exists.");

			var repositorySourceDirectoryExists = Instances.FileSystemOperator.DirectoryExists(repositorySourceDirectoryPath);
			if (repositorySourceDirectoryExists)
			{
				logger.LogInformation($"Repository source directory exists:{Environment.NewLine}\t{repositorySourceDirectoryPath}");
			}
			else
			{
				logger.LogInformation($"Repository source directory does not exist. Creating directory...{Environment.NewLine}\t{repositorySourceDirectoryPath}");

				Instances.FileSystemOperator.CreateDirectory(repositorySourceDirectoryPath);

				logger.LogInformation($"Created repository source directory:{Environment.NewLine}\t{repositorySourceDirectoryPath}");
			}

			return repositorySourceDirectoryPath;
		}

		public async Task Delete_Idempotent(
			string repositoryName,
			string repositoryOwnerName,
			ILogger logger)
        {
			var repositoryDirectoryPath = Instances.DirectoryPathOperator.GetRepositoryDirectory(repositoryOwnerName, repositoryName);

			logger.LogInformation($"Deleting repository '{repositoryName}'...");

			// Delete local.
			logger.LogInformation("Deleting local directory repository...");

			Instances.FileSystemOperator.DeleteDirectory_OkIfNotExists(repositoryDirectoryPath);

			logger.LogInformation("Deleted local directory repository.");

			// Delete remote.
			logger.LogInformation("Deleting remote GitHub repository...");

			await Instances.GitHubOperator.DeleteRepository(
				Instances.GitHubOwners.SafetyCone,
				repositoryName);

			logger.LogInformation("Deleted remote GitHub repository.");

			logger.LogInformation($"Deleted repository '{repositoryName}'.");
		}

		public string Get_RepositoryDescription_FromLibraryDescription(string libraryDescription)
        {
			// The repository description is just the library description.
			var output = libraryDescription;
			return output;
        }

		public GitHubRepositorySpecification Get_RepositorySpecification(
			string ownerName,
			string name,
			string description,
			bool isPrivate)
        {
			var output = new GitHubRepositorySpecification
			{
				Organization = ownerName,
				Name = name,
				Description = description,
				InitializeWithReadMe = true,
				License = GitHubRepositoryLicense.MIT,
			};

			// Set visibility.
			output.IsPrivate(isPrivate);

			return output;
		}

		public async Task SafetyCheck_VerifyRepositoryDoesNotAlreadyExist(
			string repositoryName,
			string repositoryOwnerName,
			ILogger logger)
        {
			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(
				repositoryOwnerName,
				repositoryName);

			logger.LogInformation($"Checking if remote GitHub repository '{ownedRepositoryName}' already exists...");

			var remoteRepositoryAlreadyExists = await Instances.RemoteRepositoryOperator.RepositoryExists(
				repositoryOwnerName,
				repositoryName);

			logger.LogInformation($"Checking if local directory repository '{ownedRepositoryName}' already exists...");

			var localRepositoryAlreadyExists = Instances.LocalRepositoryOperator.RepositoryExists(
				repositoryOwnerName,
				repositoryName);

			// Error if remote or local repositories exist.
			if (remoteRepositoryAlreadyExists || localRepositoryAlreadyExists)
			{
				throw new Exception($"Repository already exists.{Environment.NewLine}\tRemote '{ownedRepositoryName}' exists: {remoteRepositoryAlreadyExists}{Environment.NewLine}\tLocal '{repositoryName}' exists: {localRepositoryAlreadyExists}");
			}
		}

		public string SetupRepository(
			string repositoryDirectoryPath,
			ILogger logger)
        {
			// Gitignore file.
			var _ = Instances.RepositoryOperator.Create_GitIgnoreFile_Idempotent(
				repositoryDirectoryPath,
				logger);

			// Create repository source directory.
			var repositorySourceDirectoryPath = Instances.RepositoryOperator.Create_SourceDirectory_Idempotent(
				repositoryDirectoryPath,
				logger);

			return repositorySourceDirectoryPath;
		}

		public async Task Verify_RepositoryDoesNotExist(string repositoryOwnerName, string repositoryName)
        {
			await Instances.RemoteRepositoryOperator.VerifyRepositoryDoesNotExist(
				repositoryOwnerName,
				repositoryName);

			Instances.LocalRepositoryOperator.VerifyRepositoryDoesNotExist(
				repositoryOwnerName,
				repositoryName);
        }
	}
}