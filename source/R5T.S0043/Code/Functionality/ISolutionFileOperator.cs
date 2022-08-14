using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ISolutionFileOperator : IFunctionalityMarker,
		F0024.ISolutionFileOperator
	{
		public Guid Add_DependencyProject(
			string solutionFilePath,
			string dependencyProjectFilePath)
        {
			var dependencyProjectIdentity = this.AddProject_InSolutionFolder(
				solutionFilePath,
				dependencyProjectFilePath,
				Instances.SolutionFolderName.Dependencies);

			return dependencyProjectIdentity;
        }
	}
}