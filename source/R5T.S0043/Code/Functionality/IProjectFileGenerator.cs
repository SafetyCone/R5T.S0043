using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IProjectFileGenerator : IFunctionalityMarker,
		F0020.IProjectFileGenerator
	{
		public void Create(
			string projectDirectoryPath,
			string projectName,
			string targetFrameworkMonikerString,
			string outputTypeString)
		{
			// Ensure the project directory exists.
			F0000.Instances.FileSystemOperator.Ensure_DirectoryExists(projectDirectoryPath);

			var projectFilePath = Instances.ProjectPathsOperator.GetProjectFilePath(
				projectDirectoryPath,
				projectName);

			F0020.Instances.ProjectFileOperator.CreateProjectFile_Synchronous(
				projectFilePath,
				F0020.Instances.ProjectXmlOperations.EmptyToStandard(
					targetFrameworkMonikerString,
					outputTypeString));
		}

		public void Create_UsingSolutionFilePath(
			string solutionFilePath,
			string projectName,
			string targetFrameworkMonikerString,
			string outputTypeString)
		{
			var solutionDirectoryPath = Instances.SolutionPathsOperator.GetSolutionDirectoryPath_FromSolutionFilePath(solutionFilePath);

			var projectDirectoryPath = Instances.ProjectPathsOperator.GetProjectDirectoryPath_FromSolutionDirectoryPath(
				solutionDirectoryPath,
				projectName);

			this.Create(
				projectDirectoryPath,
				projectName,
				targetFrameworkMonikerString,
				outputTypeString);
		}
	}
}