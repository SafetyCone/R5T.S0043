using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface IRepositoryOperations : IFunctionalityMarker
	{
		public async Task CreateNew_ConsoleRepository()
		{
			/// Inputs.
			var name = "R5T.F0027";
			var description = "Dotnet command line operator.";
			var isPrivate = false;

			var owner = Instances.GitHubOwners.SafetyCone;


			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryTestOperations>>();

			logger.LogInformation($"Creating console repository '{name}'...");

			/// Library.
			// Unadjusted library name is just the name.
			var unadjustedLibraryName = Instances.LibraryOperator.GetUnadjustedLibraryName(name);

			var libraryName = Instances.LibraryOperator.AdjustLibraryName_ForPrivacy(
				unadjustedLibraryName,
				isPrivate,
				logger);

			var libraryDescription = Instances.LibraryOperator.GetLibraryDescription(description);

			/// Repository.
			var repositoryName = Instances.RepositoryNameOperator.GetRepositoryName_FromLibraryName(libraryName);
			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(
				owner,
				repositoryName);

			logger.LogInformation($"Repository name: '{ownedRepositoryName}'.");

			var repositoryDescription = Instances.RepositoryOperator.Get_RepositoryDescription_FromLibraryDescription(libraryDescription);

			var repositorySpecification = Instances.RepositoryOperator.Get_RepositorySpecification(
				owner,
				repositoryName,
				repositoryDescription,
				isPrivate);

			/// Safety check: stop if repository already exists.
			await Instances.RepositoryOperator.SafetyCheck_VerifyRepositoryDoesNotAlreadyExist(
				repositoryName,
				owner,
				logger);

			// As of now, we can assume the repository does not exist.

			/// Create - Repository.
			var repositoryDirectoryPath = await Instances.RepositoryOperator.CreateNew_NonIdempotent(
				repositorySpecification,
				logger);

			// Setup repository.
			var repositorySourceDirectoryPath = Instances.RepositoryOperator.SetupRepository(
				repositoryDirectoryPath,
				logger);

			/// Create - Solution.
			var unadjustedSolutionName = Instances.SolutionNameOperator.GetUnadjustedSolutionName_FromUnadjustedLibraryName(unadjustedLibraryName);
			var solutionName = Instances.SolutionNameOperator.AdjustSolutionName_ForPrivacy(unadjustedSolutionName, isPrivate);

			var solutionFilePath = Instances.SolutionOperator.Create_Solution(
				repositorySourceDirectoryPath,
				solutionName,
				logger);

			/// Create - project.
			var projectName = Instances.ProjectNameOperator.GetProjectName_FromUnadjustedLibraryName(unadjustedLibraryName);

			var projectDescription = Instances.ProjectOperator.Get_ProjectDescription_FromLibraryDescription(libraryDescription);

			var projectNamespaceName = Instances.ProjectNamespacesOperator.GetDefaultNamespaceName_FromProjectName(projectName);

			var projectFilePath = Instances.ProjectOperator.Create_New(
				solutionFilePath,
				projectName,
				F0020.ProjectType.Console,
				logger);

			// Setup project.
			Instances.ProjectOperator.SetupProject_Console(
				projectFilePath,
				projectDescription,
				projectName,
				projectNamespaceName);

			// Add project to solution.
			Instances.SolutionFileOperator.AddProject(
				solutionFilePath,
				projectFilePath);
		}

		// Create a new, empty repository and clones it locally.
		public async Task CreateNew_EmptyRepository_NonIdempotent()
        {
			/// Inputs.
			var name =
                "R5T.W1001"
                //Instances.RepositoryNames.TestRepository
                ;
			var description =
                "A private first repo for creating a web application."
                //Instances.RepositoryDescriptions.ForTestRepository
                ;
			var isPrivate = true;

			var owner = Instances.GitHubOwners.SafetyCone;


			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryOperations>>();

			logger.LogInformation("Creating new empty repository...");

			// Perform safety checks.
			logger.LogInformation("Performing safety checks...");

			logger.LogInformation($"Checking if remote GitHub repository '{name}' already exists...");

			await Instances.RepositoryOperator.Verify_RepositoryDoesNotExist(
				owner,
				name);

			logger.LogInformation($"Checked if remote GitHub repository '{name}' already exists.");

			logger.LogInformation("Performed safety checks.");

			// Create new.
			var repositorySpecification = Instances.RepositoryOperator.Get_RepositorySpecification(
				owner,
				name,
				description,
				isPrivate);

			await Instances.RepositoryOperator.CreateNew_NonIdempotent(
				repositorySpecification,
				logger);
		}

		/// <summary>
		/// Library only means that no construction project is created, just the library project.
		/// </summary>
		/// <returns></returns>
		public async Task CreateNew_LibraryOnlyRepository()
		{
			/// Inputs.
			var name = "R5T.F0030";
			var description = "SSH.NET related functionality.";
			var isPrivate = false;

			var owner = Instances.GitHubOwners.SafetyCone;


			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryTestOperations>>();

			logger.LogInformation($"Creating library repository '{name}'...");

			/// Library.
			var unadjustedLibraryName = Instances.LibraryOperator.GetUnadjustedLibraryName(name);

			var libraryName = Instances.LibraryOperator.AdjustLibraryName_ForPrivacy(
				unadjustedLibraryName,
				isPrivate,
				logger);

			var libraryDescription = Instances.LibraryOperator.GetLibraryDescription(description);

			/// Repository.
			var repositoryName = Instances.RepositoryNameOperator.GetRepositoryName_FromLibraryName(libraryName);
			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(
				owner,
				repositoryName);

			logger.LogInformation($"Repository name: '{repositoryName}'.");

			// Repository description is just the library description.
			var repositoryDescription = Instances.RepositoryOperator.Get_RepositoryDescription_FromLibraryDescription(libraryDescription);

			var repositorySpecification = Instances.RepositoryOperator.Get_RepositorySpecification(
				owner,
				repositoryName,
				repositoryDescription,
				isPrivate);

			/// Safety check: stop if repository already exists.
			await Instances.RepositoryOperator.SafetyCheck_VerifyRepositoryDoesNotAlreadyExist(
				repositoryName,
				owner,
				logger);

			/// Create - Repository.
			// As of now, we can assume the repository does not exist.
			var repositoryDirectoryPath = await Instances.RepositoryOperator.CreateNew_NonIdempotent(
				repositorySpecification,
				logger);

			// Setup repository.
			var repositorySourceDirectoryPath = Instances.RepositoryOperator.SetupRepository(
				repositoryDirectoryPath,
				logger);

			/// Create - Solution.
			// Now create the solution and project.
			var unadjustedSolutionName = Instances.SolutionNameOperator.GetUnadjustedSolutionName_FromUnadjustedLibraryName(unadjustedLibraryName);
			var solutionName = Instances.SolutionNameOperator.AdjustSolutionName_ForPrivacy(unadjustedSolutionName, isPrivate);

			var solutionFilePath = Instances.SolutionOperator.Create_Solution(
				repositorySourceDirectoryPath,
				solutionName,
				logger);

			// Create - project.
			// Project name is just the unadjusted repository name. No adjustments needed.
			var projectName = unadjustedLibraryName;

			// Script project description is just the library description.
			var projectDescription = libraryDescription;

			// Namespace name is just the program name.
			var projectNamespaceName = Instances.ProjectNamespacesOperator.GetDefaultNamespaceName_FromProjectName(projectName);

			var projectFilePath = Instances.ProjectOperator.Create_New(
				solutionFilePath,
				projectName,
				F0020.ProjectType.Library,
				logger);

			// Setup project.
			Instances.ProjectOperator.SetupProject_Library(
				projectFilePath,
				projectDescription,
				projectName,
				projectNamespaceName);

			// Add project to solution.
			Instances.SolutionFileOperator.AddProject(
				solutionFilePath,
				projectFilePath);
		}

		public async Task Delete_Idempotent()
		{
			/// Inputs.
			var name =
                "R5T.L0022"
                //Instances.RepositoryNames.TestRepository
                ;

			var owner = Instances.GitHubOwners.SafetyCone;

			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryOperations>>();

			await Instances.RepositoryOperator.Delete_Idempotent(
				name,
				owner,
				logger);
		}
	}
}