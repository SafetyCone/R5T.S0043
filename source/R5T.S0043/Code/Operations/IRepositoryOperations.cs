using System;
using System.Extensions;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0132;
using R5T.T0146;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface IRepositoryOperations : IFunctionalityMarker,
		F0058.IRepositoryOperations
	{
		public async Task CreateNew_ConsoleRepository()
		{
			/// Inputs.
			var owner =
                Instances.GitHubOwners.SafetyCone
                //Instances.GitHubOwners.DavidCoats
                ;
			var name = Instances.RepositoryNameOperator.GetRepositoryName(owner, "C0003");
			var description = "TreeView WinForms of repository, solution, project, and code operations.";
			var isPrivate = false;


			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryTestOperations>>();

			logger.LogInformation($"Creating console repository '{name}'...");

			/// Library.
			var libraryDescriptors = F0043.LibraryOperator.Instance.GetDescriptors(
				name,
				description,
				isPrivate,
				logger);

			/// Repository.
			var repositoryDescriptors = F0046.RepositoryOperator.Instance.GetDescriptors(
				libraryDescriptors.Name,
				libraryDescriptors.Description,
				owner);			

			logger.LogInformation($"Repository name: '{repositoryDescriptors.OwnedName}'.");			

			var repositorySpecification = Instances.RepositoryOperator.Get_RepositorySpecification(
				owner,
				repositoryDescriptors.Name,
				repositoryDescriptors.Description,
				isPrivate);

			/// Safety check: stop if repository already exists.
			await Instances.RepositoryOperator.SafetyCheck_VerifyRepositoryDoesNotAlreadyExist(
				repositoryDescriptors.Name,
				owner,
				logger);

			// As of now, we can assume the repository does not exist.

			/// Create - Repository.
			var repositoryLocations = await Instances.RepositoryOperator.As<F0060.IRepositoryOperator, F0042.IRepositoryOperator>().CreateNew_NonIdempotent(
				repositorySpecification,
				logger);

			// Setup repository.
			var repositorySourceDirectoryPath = Instances.RepositoryOperator.As<F0060.IRepositoryOperator, F0042.IRepositoryOperator>().SetupRepository(
				repositoryLocations.LocalDirectoryPath,
				logger);

			/// Create - Solution.
			var unadjustedSolutionName = Instances.SolutionNameOperator.GetUnadjustedSolutionName_FromUnadjustedLibraryName(libraryDescriptors.UnadjustedName);
			var solutionName = Instances.SolutionNameOperator.AdjustSolutionName_ForPrivacy(unadjustedSolutionName, isPrivate);

			var solutionFilePath = Instances.SolutionOperator.Create_Solution_SourceDirectoryPath(
				repositorySourceDirectoryPath,
				solutionName,
				logger);

			/// Create - project.
			var projectName = Instances.ProjectNameOperator.GetProjectName_FromUnadjustedLibraryName(libraryDescriptors.UnadjustedName);

			var projectDescription = Instances.ProjectOperator.Get_ProjectDescription_FromLibraryDescription(libraryDescriptors.Description);

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

			// Perform initial commit.
			Instances.RepositoryOperator.PerformInitialCommit(
				repositoryLocations.LocalDirectoryPath,
				logger);
		}

		/// <summary>
		/// Create a new remote repository, clones it locally, adds the minimal files and folders for a repository, then commits the changes.
		/// </summary>
		public async Task CreateNew_MinimalRepository_NonIdempotent()
		{
			/// Inputs.
			var name =
                "R5T.W0001"
                //Instances.RepositoryNames.TestRepository
                ;
			var description =
				"An example web application using the web application builder configurer functionality."
				//Instances.RepositoryDescriptions.ForTestRepository
				;
			var isPrivate = false;

			var owner =
				Instances.GitHubOwners.SafetyCone
				;


			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryOperations>>();

			/// Library.
			var libraryDescriptors = F0043.LibraryOperator.Instance.GetDescriptors(
				name,
				description,
				isPrivate,
				logger);

			/// Repository.
			var repositoryDescriptors = Instances.RepositoryOperator.GetDescriptors(
				libraryDescriptors.Name,
				libraryDescriptors.Description,
				owner);

			var result = await this.CreateNew_MinimalRepository_NonIdempotent(
				owner,
				repositoryDescriptors.Name,
				repositoryDescriptors.Description,
				isPrivate,
				logger);

			// Output result to file, then open in Notepad++.
			Instances.Operations.WriteResultAndOpenInNotepadPlusPlus(
				result,
				Instances.FilePaths.ResultOutputJsonFilePath,
				logger);
		}

		/// <summary>
		/// Create a new, empty repository and clones it locally.
		/// </summary>
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

			var owner =
				Instances.GitHubOwners.SafetyCone
				;


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

			var repositoryLocations = await Instances.RepositoryOperator.As<F0060.IRepositoryOperator, F0042.IRepositoryOperator>().CreateNew_NonIdempotent(
				repositorySpecification,
				logger);

			// Perform initial commit.
			Instances.RepositoryOperator.PerformInitialCommit(
				repositoryLocations.LocalDirectoryPath,
				logger);
		}

		/// <summary>
		/// Library includes two solutions: the library solution and a construction solution. Two projects are created as well: a library project, and a constuction console project.
		/// </summary>
		public async Task CreateNew_LibraryRepository()
		{
			/// Inputs.
			var owner = Instances.GitHubOwners.SafetyCone;
			var name =
                Instances.RepositoryNameOperator.GetRepositoryName(owner, "F0063")
                //Instances.RepositoryNames.TestRepository
                ;
			var description = "Solution operations functionality.";
			var isPrivate = false;


			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryTestOperations>>();

			await this.CreateNew_LibraryRepository(
				owner,
				name,
				description,
				isPrivate,
				logger);
		}

		/// <summary>
		/// Library only means that no construction project is created, just the library project.
		/// </summary>
		public async Task CreateNew_LibraryOnlyRepository()
		{
			/// Inputs.
			var owner = Instances.GitHubOwners.SafetyCone;
			var name = Instances.RepositoryNameOperator.GetRepositoryName(owner, "NG0008");
			var description = "Newtonsoft.Json NuGet package selector library.";
			var isPrivate = false;

			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryTestOperations>>();

			logger.LogInformation($"Creating library repository '{name}'...");

			/// Library.
			var unadjustedLibraryName = Instances.LibraryNameOperator.GetUnadjustedLibraryName(name);

			var libraryName = Instances.LibraryNameOperator.AdjustLibraryName_ForPrivacy(
				unadjustedLibraryName,
				isPrivate,
				logger);

			var libraryDescription = Instances.LibraryDescriptionOperator.GetLibraryDescription(description);

			/// Repository.
			var repositoryName = Instances.RepositoryNameOperator.GetRepositoryName_FromLibraryName(libraryName);
			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(
				owner,
				repositoryName);

			logger.LogInformation($"Repository name: '{repositoryName}'.");

			// Repository description is just the library description.
			var repositoryDescription = Instances.RepositoryDescriptionOperator.GetRepositoryDescription_FromLibraryDescription(libraryDescription);

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
			var repositoryLocations = await Instances.RepositoryOperator.As<F0060.IRepositoryOperator, F0042.IRepositoryOperator>().CreateNew_NonIdempotent(
				repositorySpecification,
				logger);

			// Setup repository.
			var repositorySourceDirectoryPath = Instances.RepositoryOperator.As<F0060.IRepositoryOperator, F0042.IRepositoryOperator>().SetupRepository(
				repositoryLocations.LocalDirectoryPath,
				logger);

			/// Create - Solution.
			// Now create the solution and project.
			var unadjustedSolutionName = Instances.SolutionNameOperator.GetUnadjustedSolutionName_FromUnadjustedLibraryName(unadjustedLibraryName);
			var solutionName = Instances.SolutionNameOperator.AdjustSolutionName_ForPrivacy(unadjustedSolutionName, isPrivate);

			var solutionFilePath = Instances.SolutionOperator.Create_Solution_SourceDirectoryPath(
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

			// Set package properties.
			Instances.ProjectOperations.AddPackageProperties(projectFilePath);

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

			// Perform initial commit.
			Instances.RepositoryOperator.PerformInitialCommit(
				repositoryLocations.LocalDirectoryPath,
				logger);
		}

		public async Task Delete_Idempotent()
		{
			/// Inputs.
			var name =
                "D8S.S0001"
                //Instances.RepositoryNames.TestRepository
                ;
			var owner =
				Instances.GitHubOwners.DavidCoats
				//Instances.GitHubOwners.SafetyCone
				;
			var isPrivate = true;

			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryOperations>>();

			var deleteRepositoryResult = await this.Delete_Idempotent(
				name,
				owner,
				isPrivate,
				logger);

			// Output result to file, then open in Notepad++.
			Instances.Operations.WriteResultAndOpenInNotepadPlusPlus(
				deleteRepositoryResult,
				Instances.FilePaths.ResultOutputJsonFilePath,
				logger);
		}
	}
}