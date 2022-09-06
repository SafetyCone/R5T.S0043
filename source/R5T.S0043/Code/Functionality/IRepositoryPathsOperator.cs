using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IRepositoryPathsOperator : IFunctionalityMarker
	{
		public string GetGitIgnoreFilePath(string repositoryDirectoryPath)
        {
			var gitIgnoreFilePath = Instances.PathOperator.GetFilePath(
				repositoryDirectoryPath,
				Instances.FileNames.GitIgnore);

			return gitIgnoreFilePath;
		}
	}
}