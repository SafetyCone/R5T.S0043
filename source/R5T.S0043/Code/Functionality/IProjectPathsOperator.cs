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

		public string GetProjectDirectoryPath(string projectFilePath)
        {
			var projectDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(projectFilePath);
			return projectDirectoryPath;
		}

		public string GetProjectFileName(string projectFilePath)
        {
			var projectFileName = projectFilePath.Split('\\').Last();
			return projectFileName;
		}

		public string GetProjectName(string projectFilePath)
        {
			var projectFileName = this.GetProjectFileName(projectFilePath);

			var projectName = Instances.FileNameOperator_Base.GetFileNameStem(projectFileName);
			return projectName;
		}

		public string GetProjectPlanFilePath(string projectFilePath)
        {
			var projectDirectoryPath = this.GetProjectDirectoryPath(projectFilePath);

			var projectPlanFilePath = Instances.PathOperator.GetFilePath(
				projectDirectoryPath,
				Instances.FileNames.ProjectPlan);

			return projectPlanFilePath;
        }
	}
}