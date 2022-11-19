using System;

using Microsoft.Extensions.Logging;

using R5T.F0024;
using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ISolutionOperations : IFunctionalityMarker
	{
		public void Add_DependencyProjectReferenceToSolution()
        {
			/// Inputs.
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.E0049.Private\source\R5T.E0049.Private.sln";
			var dependencyProjectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.L0022\source\R5T.L0022\R5T.L0022.csproj";


			/// Run.
			// First create a backup copy of the solution file (in case solution functionality does not work).
			var backupSolutionFilePath = Instances.FilePathOperator.GetBackupCopyFilePath(solutionFilePath);

			Instances.FileSystemOperator.CopyFile(
				solutionFilePath,
				backupSolutionFilePath);

			// Now add project to solution.
			Instances.SolutionFileOperator.Add_DependencyProject(
				solutionFilePath,
				dependencyProjectFilePath);
		}

		public void AddNew_ConsoleProjectToSolution()
		{
			/// Inputs.
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0027\source\R5T.F0027.Construction.sln";
			var projectName = "R5T.F0027.Construction";
			var projectDescription = "Construction project for the dotnet executable functionality library.";

			/// Run.
			// Create the project file.
			var projectFilePath = Instances.SolutionOperator.CreateConsoleProjectInExistingSolution(
				solutionFilePath,
				projectName,
				projectDescription);

			// Add to the solution.
			// First create a backup copy of the solution file (in case solution functionality does not work).
			var backupSolutionFilePath = Instances.FilePathOperator.GetBackupCopyFilePath(solutionFilePath);

			Instances.FileSystemOperator.CopyFile(
				solutionFilePath,
				backupSolutionFilePath);

			// Add project to solution.
			Instances.SolutionFileOperator.AddProject(
				solutionFilePath,
				projectFilePath);
		}

		public void AddNew_LibraryProjectToSolution()
		{
			/// Inputs.
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0038\source\R5T.F0038.Construction.sln";
			var projectName = "R5T.F0038.F001";
			var projectDescription = "NuGet SDK related functionality.";

			/// Run.
			// Create the project file.
			var projectFilePath = Instances.SolutionOperator.CreateLibraryProjectInExistingSolution(
				solutionFilePath,
				projectName,
				projectDescription);

			// Set package properties.
			Instances.ProjectOperations.AddPackageProperties(projectFilePath);

			// Add to the solution.
			// First create a backup copy of the solution file (in case solution functionality does not work).
			var backupSolutionFilePath = Instances.FilePathOperator.GetBackupCopyFilePath(solutionFilePath);

			Instances.FileSystemOperator.CopyFile(
				solutionFilePath,
				backupSolutionFilePath);

			// Add project to solution.
			Instances.SolutionFileOperator.AddProject(
				solutionFilePath,
				projectFilePath);
		}

		public void CreateNewSolutionFile()
        {
			/// Inputs.
			var solutionName = "TestSolution";
			var repositoryDirectoryPath = @"C:\Code\DEV\Git\GitHub\SafetyCone\Test123";
			var visualStudioVersion = VisualStudioVersion.Version_2022;

			/// Run.
			using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<ISolutionOperations>>();

			var repositorySourceDirectoryPath = Instances.RepositoryPathsOperator.GetSourceDirectoryPath(repositoryDirectoryPath);

			var solutionDirectoryPath = Instances.SolutionPathsOperator.GetSolutionDirectoryPath_FromRepositorySourceDirectoryPath(repositorySourceDirectoryPath);

			var createNewSolutionFileResult = Instances.SolutionOperator.CreateNew_SolutionFile(
				visualStudioVersion,
				solutionDirectoryPath,
				solutionName,
				logger);

			Instances.Operations.WriteResultAndOpenInNotepadPlusPlus(
				createNewSolutionFileResult,
				Instances.FilePaths.ResultOutputJsonFilePath,
				logger);
        }

		public void UpgradeSolutionFiles()
		{
			/// Inputs.
			var pushChangesToRemote = false;
			var solutionFilePaths = new[]
			{
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0027\source\R5T.F0027.Construction.sln",
			};


			/// Run.
			using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<ISolutionOperations>>(); 

			foreach (var solutionFilePath in solutionFilePaths)
			{
				var solutionFile = F0024.Instances.SolutionFileSerializer.Deserialize(solutionFilePath);

				var visualStudioVersionDescription = solutionFile.VersionInformation.VersionDescription;

				var visualStudioVersionIs2019 = visualStudioVersionDescription == "# Visual Studio Version 16";
				if (!visualStudioVersionIs2019)
				{
					throw new Exception($"Unknown Visual Studio version, refusing to upgrade:\n{visualStudioVersionDescription}");
				}

				// Save a copy.
				var vs2019SolutionFilePath = F0002.Instances.PathOperator.AppendToFileNameStem(
					solutionFilePath,
					"_VS2019");

				F0000.Instances.FileSystemOperator.CopyFile(
					solutionFilePath,
					vs2019SolutionFilePath);

				// Upgrade the file.
				solutionFile.VersionInformation.VersionDescription = F0024.Instances.VersionInformationOperator.GetVisualStudioVersionDescription(
					F0024.Instances.VisualStudioVersionStrings.Version_17);

				solutionFile.VersionInformation.Version = F0024.Instances.VersionInformationOperator.GetVisualStudioVersionLine(
					F0024.Instances.VisualStudioVersions.VisualStudio_2022);

				// Save the file, overwriting.
				F0024.Instances.SolutionFileSerializer.Serialize(
					solutionFilePath,
					solutionFile);

				// Push change to remote.
				var solutionIsInRepository = Instances.GitOperator.HasRepository(solutionFilePath);
				if(pushChangesToRemote && solutionIsInRepository)
                {
					var repositoryDirectoryPath = solutionIsInRepository.Result;

					Instances.GitHubOperator_Base.PushAllChanges(
						repositoryDirectoryPath,
						Instances.CommitMessages.UpgradeSolutionFileToVS2022,
						logger);
				}
			}
		}
	}
}