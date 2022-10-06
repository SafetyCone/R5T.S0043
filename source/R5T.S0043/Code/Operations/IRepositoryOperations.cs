using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0132;
using R5T.T0146;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface IRepositoryOperations : IFunctionalityMarker
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

			var solutionFilePath = Instances.SolutionOperator.Create_Solution_SourceDirectoryPath(
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

			// Perform initial commit.
			Instances.RepositoryOperator.PerformInitialCommit(
				repositoryDirectoryPath,
				logger);
		}

		/// <summary>
		/// Create a new remote repository, clones it locally, adds the minimal files and folders for a repository, then commits the changes.
		/// </summary>
		public async Task CreateNew_MinimalRepository_NonIdempotent()
		{
			/// Inputs.
			var name =
                //"R5T.W1001"
                Instances.RepositoryNames.TestRepository
                ;
			var description =
                //"A private first repo for creating a web application."
                Instances.RepositoryDescriptions.ForTestRepository
                ;
			var isPrivate = true;

			var owner =
				Instances.GitHubOwners.SafetyCone
				;


			/// Run.
			var result = T0146.Instances.ResultOperator.Result("Create New Minimal Repository"); 

			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryOperations>>();

			logger.LogInformation("Creating new empty repository...");

			// Perform safety checks.
			logger.LogInformation("Performing safety checks...");

			logger.LogInformation($"Checking if remote GitHub repository '{name}' already exists...");

			var verifyRepositoryAvailabilityResult = await Instances.RepositoryOperator.Verify_RepositoryDoesNotExist_Result(
				owner,
				name);

			logger.LogInformation($"Checked if remote GitHub repository '{name}' already exists.");

			IReason verifyRepositoryAvailabilityReason = verifyRepositoryAvailabilityResult.IsSuccess()
				? T0146.Instances.ReasonOperator.Success("Passed safety checks.")
				: T0146.Instances.ReasonOperator.Failure("Failed safety checks.", verifyRepositoryAvailabilityResult.Failures)
				;

			result
				.WithReason(verifyRepositoryAvailabilityReason)
				.WithChild(verifyRepositoryAvailabilityResult)
				;

			if(verifyRepositoryAvailabilityResult.IsSuccess())
            {
				logger.LogInformation($"Remote GitHub repository '{name}' does not already exist.");

				logger.LogInformation("Performed safety checks.");

				// Create new.
				var repositorySpecification = Instances.RepositoryOperator.Get_RepositorySpecification(
					owner,
					name,
					description,
					isPrivate);

				var createNewResult = await Instances.RepositoryOperator.CreateNew_NonIdempotent_Result(
					repositorySpecification,
					logger);

				IReason createNewReason = createNewResult.IsSuccess()
					? T0146.Instances.ReasonOperator.Success("Created new repository.")
					: T0146.Instances.ReasonOperator.Failure("Failed to create new repository.", createNewResult.Failures)
					;

				result
					.WithReason(createNewReason)
					.WithChild(createNewResult)
					;

				var localRepositoryDirectoryPath = createNewResult.Value;

				// Setup repository (gitignore file, source directory).
				var setupRepositoryResult = Instances.RepositoryOperator.SetupRepository_Result(
					localRepositoryDirectoryPath,
					logger);

				IReason setupRepositoryReason = setupRepositoryResult.IsSuccess()
					? Instances.ReasonOperator.Success("Successfully setup repository.")
					: Instances.ReasonOperator.Failure("Failed to setup repository.", setupRepositoryResult.Failures)
					;

				result.WithReason(setupRepositoryReason).WithChild(setupRepositoryResult);

				// Perform initial commit.
				var performInitialCommitResult = Instances.RepositoryOperator.PerformInitialCommit(
					localRepositoryDirectoryPath,
					logger);

				IReason performInitialCommitReason = performInitialCommitResult.IsSuccess()
					? Instances.ReasonOperator.Success("Initial commit succeeded.")
					: Instances.ReasonOperator.Failure("Initial commit failed.", performInitialCommitResult.Failures)
					;

				result.WithReason(performInitialCommitReason).WithChild(performInitialCommitResult);
			}
            else
            {
				logger.LogError($"Remote GitHub repository '{name}' already exists.");

				logger.LogError("Failed safety checks.");

				result.WithFailure("Failed safety checks, no repository created.");
			}

			// Output result to file, then open in Notepad++.
			Instances.Operations.WriteResultAndOpenInNotepadPlusPlus(
				result,
				Instances.FilePaths.ResultOutputFilePath,
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

			var repositoryDirectoryPath = await Instances.RepositoryOperator.CreateNew_NonIdempotent(
				repositorySpecification,
				logger);

			// Perform initial commit.
			Instances.RepositoryOperator.PerformInitialCommit(
				repositoryDirectoryPath,
				logger);
		}

		/// <summary>
		/// Library includes two solutions: the library solution and a construction solution. Two projects are created as well: a library project, and a constuction console project.
		/// </summary>
		public async Task CreateNew_LibraryRepository()
		{
			/// Inputs.
			var owner = Instances.GitHubOwners.SafetyCone;
			var name = Instances.RepositoryNameOperator.GetRepositoryName(owner, "F0042");
			var description = "Repository functionality (RepositoryOperator, RemoteRepositoryOperator, LocalRepositoryOperator).";
			var isPrivate = false;

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

			// Construction solution.
			var unadjustedConstructionSolutionName = Instances.SolutionNameOperator.GetConstructionSolutionName(unadjustedSolutionName);
			var constructionSolutionName = Instances.SolutionNameOperator.AdjustSolutionName_ForPrivacy(unadjustedConstructionSolutionName, isPrivate);

			var constructionSolutionFilePath = Instances.SolutionOperator.Create_Solution_SourceDirectoryPath(
				repositorySourceDirectoryPath,
				constructionSolutionName,
				logger);

			// Construction project.
			// Project name is just the unadjusted repository name. No adjustments needed.
			var constructionProjectName = Instances.ProjectNameOperator.GetConstructionProjectName(projectName);

			// Script project description is just the library description.
			var constructionProjectDescription = $"Construction console project for the {projectName} library.";

			// Namespace name is just the program name.
			var constructionProjectNamespaceName = Instances.ProjectNamespacesOperator.GetDefaultNamespaceName_FromProjectName(constructionProjectName);

			var constructionProjectFilePath = Instances.ProjectOperator.Create_New(
				solutionFilePath,
				constructionProjectName,
				F0020.ProjectType.Library,
				logger);

			// Set package properties.
			Instances.ProjectOperations.AddPackageProperties(constructionProjectFilePath);

			// Setup project.
			Instances.ProjectOperator.SetupProject_Library(
				constructionProjectFilePath,
				constructionProjectDescription,
				constructionProjectName,
				constructionProjectNamespaceName);

			// Add project reference to library project to the construction project.
			Instances.ProjectFileOperator.AddProjectReference_Synchronous(
				constructionProjectFilePath,
				projectFilePath);

			// Add projects to construction solution.
			// Add construction project first so it will be the default startup project.
			Instances.SolutionFileOperator.AddProject(
				constructionSolutionFilePath,
				constructionProjectFilePath);

			Instances.SolutionFileOperator.AddProject(
				constructionSolutionFilePath,
				projectFilePath);


			// Perform initial commit.
			Instances.RepositoryOperator.PerformInitialCommit(
				repositoryDirectoryPath,
				logger);
		}

		/// <summary>
		/// Library only means that no construction project is created, just the library project.
		/// </summary>
		public async Task CreateNew_LibraryOnlyRepository()
		{
			/// Inputs.
			var owner = Instances.GitHubOwners.SafetyCone;
			var name = Instances.RepositoryNameOperator.GetRepositoryName(owner, "NG0005");
			var description = "Octokit NuGet package selector library.";
			var isPrivate = false;

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
				repositoryDirectoryPath,
				logger);
		}

		public async Task Delete_Idempotent()
		{
			/// Inputs.
			var name =
                //"R5T.L0022"
                Instances.RepositoryNames.TestRepository
                ;

			var owner =
				Instances.GitHubOwners.SafetyCone
				;

			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryOperations>>();

			var ownedRepositoryName = Instances.RepositoryNameOperator.GetOwnedRepositoryName(owner, name);

			logger.LogInformation($"Deleting repository {ownedRepositoryName}...");

			var deleteRepositoryResult = await Instances.RepositoryOperator.Delete_Idempotent_Result(
				name,
				owner,
				logger);

			// No need to create a wrapping result.

			logger.LogInformation($"Deleted repository {ownedRepositoryName}.");

			// Output result to file, then open in Notepad++.
			Instances.Operations.WriteResultAndOpenInNotepadPlusPlus(
				deleteRepositoryResult,
				Instances.FilePaths.ResultOutputFilePath,
				logger);
		}
	}
}