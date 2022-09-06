using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IProjectNamespacesOperator : IFunctionalityMarker
	{
		public string GetDefaultNamespaceName_FromProjectName(string projectName)
        {
			// The default namespace name is just the project name.
			var defaultNamespaceName = projectName;
			return defaultNamespaceName;
		}

		public string GetDefaultNamespaceName(string projectFilePath)
        {
			var projectName = Instances.ProjectPathsOperator.GetProjectName(projectFilePath);

			var output = this.GetDefaultNamespaceName_FromProjectName(projectName);
			return output;
        }
	}
}