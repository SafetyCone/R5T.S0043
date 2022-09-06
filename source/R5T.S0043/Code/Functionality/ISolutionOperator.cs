using System;

using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ISolutionOperator : IFunctionalityMarker
	{
		public string Create_Solution(
			string sourceDirectoryPath,
			string solutionName,
			ILogger logger)
        {
			var solutionDirectoryPath = Instances.SolutionPathsOperator.GetSolutionDirectoryPath_FromRepositorySourceDirectoryPath(sourceDirectoryPath);

			var solutionFileName = Instances.FileNameOperator.GetSolutionFileName_FromSolutionName(solutionName);
			var solutionFilePath = Instances.PathOperator.GetFilePath(
				solutionDirectoryPath,
				solutionFileName);

			logger.LogInformation($"Creating solution file...{Environment.NewLine}\t{solutionFilePath}");

			Instances.SolutionFileGenerator.CreateNew(solutionFilePath);

			logger.LogInformation($"Created script solution file.{Environment.NewLine}\t{solutionFilePath}");

			return solutionFilePath;
		}

		public string CreateLibraryProjectInExistingSolution(
			string solutionFilePath,
			string projectName,
			string projectDescription)
        {
			// Check that solution file exists.
			var solutionFileExists = Instances.FileSystemOperator.FileExists(solutionFilePath);
            if(!solutionFileExists)
            {
				throw new Exception($"Solution file does not exist.{Environment.NewLine}\t{solutionFilePath}");
            }

			// Get project file path.
			var solutionDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(solutionFilePath);

			var projectDirectoryName = Instances.DirectoryNameOperator.GetProjectDirectoryName_FromProjectName(projectName);
			var projectDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				solutionDirectoryPath,
				projectDirectoryName);

			var projectFileName = Instances.FileNameOperator.GetProjectFileName_FromProjectName(projectName);
			var projectFilePath = Instances.PathOperator.GetFilePath(
				projectDirectoryPath,
				projectFileName);

			// Check that project file does not already exist.
			var projectFileExists = Instances.FileSystemOperator.FileExists(projectFilePath);
			if(projectFileExists)
            {
				throw new Exception($"Project file already exists.{Environment.NewLine}\t{projectFilePath}");
            }

			var namespaceName = Instances.ProjectNamespacesOperator.GetDefaultNamespaceName_FromProjectName(projectName);

			// Ensure the project directory exists.
			Instances.FileSystemOperator.CreateDirectory_OkIfAlreadyExists(projectDirectoryPath);

			// Create the project file.
			Instances.ProjectFileGenerator.CreateNewLibrary(projectFilePath);

			Instances.ProjectOperator.SetupProject_Library(
				projectFilePath,
				projectDescription,
				projectName,
				namespaceName);

			return projectFilePath;
        }
	}
}