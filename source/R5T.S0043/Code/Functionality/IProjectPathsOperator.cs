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

			var codeDirectoryPath = projectDirectoryPath + "Code\\";
			return codeDirectoryPath;
        }

		public string GetInstancesFilePath(string projectFilePath)
		{
			var codeDirectoryPath = this.GetCodeDirectoryPath(projectFilePath);

			var instancesFilePath = codeDirectoryPath + "Instances.cs";
			return instancesFilePath;
		}

		public string GetProgramFilePath(string projectFilePath)
		{
			var codeDirectoryPath = this.GetCodeDirectoryPath(projectFilePath);

			var instancesFilePath = codeDirectoryPath + "Program.cs";
			return instancesFilePath;
		}

		public string GetProjectDirectoryPath(string projectFilePath)
        {
			var projectDirectoryPath = String.Join('\\', projectFilePath.Split('\\').SkipLast(1)) + '\\';
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

			var projectName = String.Join('.', projectFileName.Split('.').SkipLast(1));
			return projectName;
		}
	}
}