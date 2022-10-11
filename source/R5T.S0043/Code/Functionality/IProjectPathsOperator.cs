using System;
using System.Linq;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IProjectPathsOperator : IFunctionalityMarker
	{
		public string GetCodeDirectoryPath(string projectFilePath)
        {
			var projectDirectoryPath = this.GetProjectDirectoryPath(projectFilePath);

			var codeDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				projectDirectoryPath,
				Instances.DirectoryNames.Code);

			return codeDirectoryPath;
        }

		public string GetDocumentationFilePath(string projectFilePath)
        {
			var codeDirectoryPath = this.GetCodeDirectoryPath(projectFilePath);

			var documentationFilePath = Instances.PathOperator.GetFilePath(
				codeDirectoryPath,
				Instances.FileNames.Documentation);

			return documentationFilePath;
		}

		public string GetInstancesFilePath(string projectFilePath)
		{
			var codeDirectoryPath = this.GetCodeDirectoryPath(projectFilePath);

			var instancesFilePath = Instances.PathOperator.GetFilePath(
				codeDirectoryPath,
				Instances.FileNames.Instances);

			return instancesFilePath;
		}

		public string GetProgramFilePath(string projectFilePath)
		{
			var codeDirectoryPath = this.GetCodeDirectoryPath(projectFilePath);

			var instancesFilePath = Instances.PathOperator.GetFilePath(codeDirectoryPath, Instances.FileNames.Program);
			return instancesFilePath;
		}

		public string GetProjectFilePath(
			string projectDirectoryPath,
			string projectName)
        {
			var projectFileName = Instances.FileNameOperator.GetProjectFileName_FromProjectName(projectName);

			var projectFilePath = F0002.Instances.PathOperator.GetFilePath(
				projectDirectoryPath,
				projectFileName);

			return projectFilePath;
        }

		public string GetProjectDirectoryPath(string projectFilePath)
        {
			var projectDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(projectFilePath);
			return projectDirectoryPath;
		}

		public string GetProjectDirectoryPath_FromSolutionDirectoryPath(
			string solutionDirectoryPath,
			string projectName)
        {
			var projectDirectoryName = Instances.ProjectDirectoryNameOperator.GetProjectDirectoryName_FromProjectName(projectName);

			var projectDirectoryPath = Instances.PathOperator.GetDirectoryPath(
				solutionDirectoryPath,
				projectDirectoryName);

			return projectDirectoryPath;
        }

		public string GetProjectPlanTextFilePath(string projectFilePath)
        {
			var projectDirectoryPath = this.GetProjectDirectoryPath(projectFilePath);

			var projectPlanFilePath = Instances.PathOperator.GetFilePath(
				projectDirectoryPath,
				Instances.FileNames.ProjectPlanTextFile);

			return projectPlanFilePath;
        }

		public string GetProjectPlanMarkdownFilePath(string projectFilePath)
		{
			var projectDirectoryPath = this.GetProjectDirectoryPath(projectFilePath);

			var projectPlanFilePath = Instances.PathOperator.GetFilePath(
				projectDirectoryPath,
				Instances.FileNames.ProjectPlanMarkdownFile);

			return projectPlanFilePath;
		}
	}
}