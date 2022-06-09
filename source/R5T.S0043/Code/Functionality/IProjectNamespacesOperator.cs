using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IProjectNamespacesOperator : IFunctionalityMarker
	{
		public string GetDefaultNamespaceName(string projectFilePath)
        {
			var projectName = Instances.ProjectPathsOperator.GetProjectName(projectFilePath);

			// The default namespace name is just the project name.
			var defaultNamespaceName = projectName;
			return defaultNamespaceName;
        }
	}
}