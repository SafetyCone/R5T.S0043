using System;

using Microsoft.Extensions.Logging;

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

		public void AddNew_LibraryProjectToSolution()
		{
			/// Inputs.
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\davidcoats\D8S.W0002.Private\source\D8S.W0002.Private.sln";
			var projectName = "D8S.W0002.R000";
			var projectDescription = "Razor class library for the D8S.W0002 web application (personal technical blog).";

			/// Run.
			// Create the project file.
			var projectFilePath = Instances.SolutionOperator.CreateLibraryProjectInExistingSolution(
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

		public void CreateNewLibraryOnlySolution(
			string repositoryDirectoryPath,
			ILogger logger)
        {

        }
	}
}