using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ISolutionTestOperations : IFunctionalityMarker
	{
		public void TryAddProject()
        {
			/// Inputs.
			var originalSolutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0043\source\R5T.S0043.sln";
			var projectName = "R5T.S0043.Library";
			var projectDescription = "A library for the R5T.S0043 project.";

			/// Run.
			// First create a copy of the solution file (in case solution functionality does not work).
			var modifiedSolutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.S0043\source\R5T.S0043-Copy.sln";

			Instances.FileSystemOperator.CopyFile(
				originalSolutionFilePath,
				modifiedSolutionFilePath);

			var projectFilePath = Instances.SolutionOperator.CreateLibraryProjectInExistingSolution(
				modifiedSolutionFilePath,
				projectName,
				projectDescription);

			Instances.SolutionFileOperator.AddProject(
				modifiedSolutionFilePath,
				projectFilePath);
        }
	}
}