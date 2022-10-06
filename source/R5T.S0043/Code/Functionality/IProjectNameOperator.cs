using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IProjectNameOperator : IFunctionalityMarker
	{
		public string GetConstructionProjectName(string projectName)
        {
			var constructionProjectName = $"{projectName}.Construction";
			return constructionProjectName;
        }

		public string GetProjectName_FromUnadjustedLibraryName(string unadjustedLibraryName)
        {
			// Project name is just the unadjusted repository name. No adjustments needed.
			var projectName = unadjustedLibraryName;
			return projectName;
		}
	}
}