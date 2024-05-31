using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IFileNameOperator : IFunctionalityMarker
	{
		public string GetProjectFileNameStem_FromProjectName(string projectName)
        {
			// Project file name stem is just the project name.
			var projectFileNameStem = projectName;
			return projectFileNameStem;
        }

		public string GetProjectFileName_FromProjectName(string projectName)
        {
			var projectFileNameStem = this.GetProjectFileNameStem_FromProjectName(projectName);
			var projectFileExtension = Instances.FileExtensions.CSharp_ProjectFile;

			var output = Instances.FileExtensionOperator.Get_FileName(projectFileNameStem, projectFileExtension);
			return output;
        }

		public string GetSolutionFileNameStem_FromSolutionName(string solutionName)
        {
			var solutionFileNameStem = solutionName;
			return solutionFileNameStem;
        }

		public string GetSolutionFileName_FromSolutionName(string solutionName)
        {
			var solutionFileNameStem = this.GetSolutionFileNameStem_FromSolutionName(solutionName);
			var solutionFileExtension = Instances.FileExtensions.Solution_File;

			var output = Instances.FileExtensionOperator.Get_FileName(solutionFileNameStem, solutionFileExtension);
			return output;
		}
	}
}