using System;

using Microsoft.Extensions.Logging;

using R5T.F0024.F001;
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

			Instances.FileSystemOperator.Copy_File(
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
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.O0007\source\R5T.O0007.Q000.sln";
			var projectName = "R5T.O0007.Q000";
			var projectDescription = "Demonstrations for project description operator and operations functionality.";

			/// Run.
			// Create the project file.
			var projectFilePath = Instances.SolutionOperator.CreateConsoleProjectInExistingSolution(
				solutionFilePath,
				projectName,
				projectDescription);

			// Add to the solution.
			// First create a backup copy of the solution file (in case solution functionality does not work).
			var backupSolutionFilePath = Instances.FilePathOperator.GetBackupCopyFilePath(solutionFilePath);

			Instances.FileSystemOperator.Copy_File(
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
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0099\source\R5T.F0099.sln";
			var projectName = "R5T.F0099.T000";
			var projectDescription = "Types library for the XML documentation operator functionality library.";

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

			Instances.FileSystemOperator.Copy_File(
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
				var solutionFile = F0024.Instances.SolutionFileSerializer.Deserialize_Synchronous(solutionFilePath);

				var visualStudioVersionDescription = solutionFile.VersionInformation.VersionDescription;

				var visualStudioVersionIs2019 = visualStudioVersionDescription == "# Visual Studio Version 16";
				if (!visualStudioVersionIs2019)
				{
					throw new Exception($"Unknown Visual Studio version, refusing to upgrade:\n{visualStudioVersionDescription}");
				}

				// Save a copy.
				var vs2019SolutionFilePath = F0002.Instances.PathOperator.Append_ToFileNameStem(
					solutionFilePath,
					"_VS2019");

				F0000.Instances.FileSystemOperator.Copy_File(
					solutionFilePath,
					vs2019SolutionFilePath);

				// Upgrade the file.
				solutionFile.VersionInformation.VersionDescription = F0024.Instances.VersionInformationOperator.GetVisualStudioVersionDescription(
					F0024.Instances.VisualStudioVersionStrings.Version_17);

				solutionFile.VersionInformation.Version = F0024.Instances.VersionInformationOperator.GetVisualStudioVersionLine(
					F0024.Instances.VisualStudioVersions.VisualStudio_2022);

				// Save the file, overwriting.
				F0024.Instances.SolutionFileSerializer.Serialize_Synchronous(
					solutionFilePath,
					solutionFile);

				// Push change to remote.
				var solutionIsInRepository = Instances.GitOperator.Has_Repository(
					solutionFilePath,
					out var repositoryDirectoryPath);

				if(pushChangesToRemote && solutionIsInRepository)
                {
					Instances.GitHubOperator_Base.PushAllChanges(
						repositoryDirectoryPath,
						Instances.CommitMessages.UpgradeSolutionFileToVS2022.Value,
						logger);
				}
			}
		}
	}
}