using System;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IRepositoryTestOperations : IFunctionalityMarker
	{
		/// <summary>
		/// Library only means that no construction project is created, just the library project.
		/// </summary>
		/// <returns></returns>
		public async Task TryCreate_NewLibraryOnlyRepository()
		{
			/// Inputs.
			var name = "R5T.L0022";
			var description = "A test script.";
			var isPrivate = false;

			var owner = Instances.GitHubOwners.SafetyCone;


			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryTestOperations>>();

			logger.LogInformation($"Creating library repository '{name}'...");

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

			logger.LogInformation($"Repository name: '{repositoryName}'.");

			// Repository description is just the library description.
			var repositoryDescription = Instances.RepositoryOperator.Get_RepositoryDescription_FromLibraryDescription(libraryDescription);

			var repositorySpecification = Instances.RepositoryOperator.Get_RepositorySpecification(
				owner,
				repositoryName,
				repositoryDescription,
				isPrivate);

			// Safety check: check if the repository exists.
			logger.LogInformation($"Checking if remote GitHub repository '{repositoryName}' already exists...");

			var remoteRepositoryAlreadyExists = await Instances.RemoteRepositoryOperator.RepositoryExists(
					owner,
					repositoryName);

			logger.LogInformation($"Checking if local directory repository '{repositoryName}' already exists...");

			var localRepositoryAlreadyExists = Instances.LocalRepositoryOperator.RepositoryExists(
				owner,
				repositoryName);

			// Error if remote or local repositories exist.
			if (remoteRepositoryAlreadyExists || localRepositoryAlreadyExists)
			{
				throw new Exception($"Repository already exists.{Environment.NewLine}\tRemote '{ownedRepositoryName}' exists: {remoteRepositoryAlreadyExists}{Environment.NewLine}\tLocal '{repositoryName}' exists: {localRepositoryAlreadyExists}");
			}

			/// Create.
			// As of now, we can assume the repository does not exist.
			var repositoryDirectoryPath = await Instances.RepositoryOperator.CreateNew_NonIdempotent(
				repositorySpecification,
				logger);

			// Setup repository.
			var repositorySourceDirectoryPath = Instances.RepositoryOperator.SetupRepository(
				repositoryDirectoryPath,
				logger);

			// Now create the solution and project.
			var unadjustedSolutionName = Instances.SolutionNameOperator.GetUnadjustedSolutionName_FromUnadjustedLibraryName(unadjustedLibraryName);
			var solutionName = Instances.SolutionNameOperator.AdjustSolutionName_ForPrivacy(unadjustedSolutionName, isPrivate);

			var solutionFilePath = Instances.SolutionOperator.Create_Solution_SourceDirectoryPath(
				repositorySourceDirectoryPath,
				solutionName,
				logger);

			// Create - project.
			// Script console executable project name is just the unadjusted repository name. No adjustments needed.
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

		public async Task TryCreateScriptRepository()
        {
			/// Inputs.
			var scriptName = "R5T.STest";
			var description = "A test script.";
			var isPrivate = true;

			var owner = Instances.GitHubOwners.SafetyCone;


			/// Run.
			await using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<IRepositoryTestOperations>>();

			logger.LogInformation($"Creating script repository '{scriptName}'...");

			/// Library.
			// Unadjusted library name is just the script name.
			var unadjustedLibraryName = scriptName;

			logger.LogInformation($"Unadjusted library name: '{unadjustedLibraryName}'");

			logger.LogInformation($"Adjusting library name for privacy (is private: {isPrivate})...");

			var libraryName = Instances.LibraryOperator.AdjustLibraryName_ForPrivacy(
				unadjustedLibraryName,
				isPrivate);

			logger.LogInformation($"Adjusted library name for privacy, library name: '{libraryName}'.");

			// Library description is just the description.
			var libraryDescription = description;

			/// Repository.
			var repositoryName = Instances.RepositoryNameOperator.GetRepositoryName_FromLibraryName(libraryName);

			logger.LogInformation($"Repository name: '{repositoryName}'.");

			// Repository description is just the library description.
			var repositoryDescription = libraryDescription;

			var repositorySpecification = Instances.RepositoryOperator.Get_RepositorySpecification(
				owner,
				repositoryName,
				repositoryDescription,
				isPrivate);

			// Skip creation of local repository if it already exists.
			logger.LogInformation($"Checking if repository '{repositoryName}' already exists...");

			var localRepositoryAlreadyExists = Instances.LocalRepositoryOperator.RepositoryExists(
				owner,
				repositoryName);
			if(localRepositoryAlreadyExists)
            {
				logger.LogInformation("Local repository already exists. Skipping local repository cloning.");
            }
            else
            {
				logger.LogInformation("Local repository does not exist.");

				logger.LogInformation("Checking if remote repository exists...");

				// Skip creation of remote repository if it already exists.
				var remoteRepositoryAlreadyExists = await Instances.RemoteRepositoryOperator.RepositoryExists(
					owner,
					repositoryName);
				if(remoteRepositoryAlreadyExists)
                {
					logger.LogInformation("Remote repository already exists. Skipping remote repository creation.");
                }
                else
                {
					logger.LogInformation("Remote repository does not exist.");

					logger.LogInformation("Creating remote repository...");

					await Instances.GitHubOperator.CreateRepository_NonIdempotent(repositorySpecification);

					logger.LogInformation("Created remote repository.");
				}

				// At this point, remote repository exists. Clone locally.
				logger.LogInformation("Cloning repository locally...");

				await Instances.GitOperator.Clone_NonIdempotent(repositoryName);

				logger.LogInformation("Cloned repository locally.");
			}

			var repositoryDirectoryPath = Instances.DirectoryPathOperator.GetRepositoryDirectory(
				owner,
				repositoryName);

			/// Perform repository setup.
			// Gitignore file.
			var gitIgnoreFilePath = Instances.RepositoryOperator.Create_GitIgnoreFile_Idempotent(
				repositoryDirectoryPath,
				logger);

			// Create repository source directory.
			var repositorySourceDirectoryPath = Instances.RepositoryOperator.Create_SourceDirectory_Idempotent(
				repositoryDirectoryPath,
				logger);

			/// Solutions.
			// Solution directory is just the repository source directory.
			var solutionDirectoryPath = Instances.SolutionPathsOperator.GetSolutionDirectoryPath_FromRepositorySourceDirectoryPath(repositorySourceDirectoryPath);

			// Script console executable solution. Unadjusted name is just the unadjusted library name.
			var unadjustedScriptSolutionName = Instances.SolutionNameOperator.GetUnadjustedSolutionName_FromUnadjustedLibraryName(unadjustedLibraryName);

			// Adjust solution name for privacy. This is done so that a user within Visual Studio can see whether the repository is private with just a glance at the solution name.
			var scriptSolutionName = Instances.SolutionNameOperator.AdjustSolutionName_ForPrivacy(unadjustedScriptSolutionName, isPrivate);

			var scriptSolutionFileName = Instances.FileNameOperator.GetSolutionFileName_FromSolutionName(scriptSolutionName);
			var scriptSolutionFilePath = Instances.PathOperator.GetFilePath(
				solutionDirectoryPath,
				scriptSolutionFileName);

			// Create script solution.
			logger.LogInformation("Checking if script solution file exists...");

			var scriptSolutionFileExists = Instances.FileSystemOperator.FileExists(scriptSolutionFilePath);
			if (scriptSolutionFileExists)
			{
				logger.LogInformation($"Script solution file exists.{Environment.NewLine}\t{scriptSolutionFilePath}");
			}
			else
			{
				logger.LogInformation($"Script solution file does not exist. Creating script solution file...{Environment.NewLine}\t{scriptSolutionFilePath}");

				Instances.SolutionFileGenerator.CreateNew(scriptSolutionFilePath);

				logger.LogInformation($"Created script solution file.{Environment.NewLine}\t{scriptSolutionFilePath}");
			}

			/// Projects
			// Script console executable project name is just the unadjusted repository name. No adjustments needed.
			var scriptProjectName = unadjustedLibraryName;

			// Script project description is just the library description.
			var scriptProjectDescription = libraryDescription;

			// Namespace name is just the program name.
			var scriptProjectNamespaceName = Instances.ProjectNamespacesOperator.GetDefaultNamespaceName_FromProjectName(scriptProjectName);

			// Project directory name is just the project name.
			var scriptProjectDirectoryName = Instances.DirectoryNameOperator.GetProjectDirectoryName_FromProjectName(scriptProjectName);
			var scriptProjectDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				solutionDirectoryPath,
				scriptProjectDirectoryName);

			var scriptProjectFileName = Instances.FileNameOperator.GetProjectFileName_FromProjectName(scriptProjectName);
			var scriptProjectFilePath = Instances.PathOperator.GetFilePath(
				scriptProjectDirectoryPath,
				scriptProjectFileName);

			logger.LogInformation("Checking if script executable console project file exists...");

			var scriptProjectFileExists = Instances.FileSystemOperator.FileExists(scriptProjectFilePath);
			if (scriptProjectFileExists)
			{
				logger.LogInformation($"Script executable console project file exists.{Environment.NewLine}\t{scriptProjectFilePath}");
			}
			else
			{
				logger.LogInformation($"Script executable console project file does not exist. Creating script project file...{Environment.NewLine}\t{scriptProjectFilePath}");

				Instances.ProjectFileGenerator.CreateNewConsole(scriptProjectFilePath);

				logger.LogInformation($"Created script executable console project file.{Environment.NewLine}\t{scriptProjectFilePath}");
			}

			// Add project to solution.
			logger.LogInformation($"Checking if solution references project...{Environment.NewLine}\tSolution:{scriptSolutionFilePath}{Environment.NewLine}\tProject:{scriptProjectFilePath}");

			var scriptSolutionReferencesScriptProject = Instances.SolutionFileOperator.HasProject(
				scriptSolutionFilePath,
				scriptProjectFilePath);
			if(scriptSolutionReferencesScriptProject)
            {
				logger.LogInformation("Solution references project.");
            }
			else
            {
				logger.LogInformation("Solution does not reference project.");

				logger.LogInformation("Add project reference to solution...");

				Instances.SolutionFileOperator.AddProject(
					scriptSolutionFilePath,
					scriptProjectFilePath);

				logger.LogInformation("Added project reference to solution.");
			}

			// Setup project.
			// Create project plan file.
			var projectPlanFilePath = Instances.PathOperator.GetFilePath(scriptProjectDirectoryPath, Instances.FileNames.ProjectPlanTextFile);

			var projectPlanFileExists = Instances.FileSystemOperator.FileExists(projectPlanFilePath);
			if(projectPlanFileExists)
            {
				logger.LogInformation("Project plan file already exists.");
			}
            else
            {
				logger.LogInformation("Creating project plan file...");

				Instances.TextFileGenerator.CreateProjectPlanTextFile(
					projectPlanFilePath,
					scriptProjectName,
					scriptProjectDescription);

				logger.LogInformation("Created project plan file.");
			}

			// Create code directory.
			var codeDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				scriptProjectDirectoryPath,
				Instances.DirectoryNames.Code);

			Instances.FileSystemOperator.CreateDirectory_OkIfAlreadyExists(codeDirectoryPath);

			// Create program file.
			var programFilePath = Instances.PathOperator.GetFilePath(
				codeDirectoryPath,
				Instances.FileNames.Program);

			var programFileExists = Instances.FileSystemOperator.FileExists(programFilePath);
			if(programFileExists)
            {
				logger.LogInformation("Program file exists.");
            }
            else
            {
				logger.LogInformation("Creating program file...");

				Instances.CodeFileGenerator.CreateProgramFile(
					programFilePath,
					scriptProjectNamespaceName);

				logger.LogInformation("Created program file.");
			}

			// Create the documentation file.
			var documentationFilePath = Instances.ProjectPathsOperator.GetDocumentationFilePath(scriptProjectFilePath);

			var documentationFileExists = Instances.FileSystemOperator.FileExists(documentationFilePath);
			if(documentationFileExists)
            {
				logger.LogInformation("Documentation file exists.");
			}
            else
            {
				logger.LogInformation("Creating documentation file...");

				Instances.CodeFileGenerator.CreateDocumentationFile(
					documentationFilePath,
					scriptProjectNamespaceName,
					scriptProjectDescription);

				logger.LogInformation("Created documentation file.");
			}
		}
	}
}