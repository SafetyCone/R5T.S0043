using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface IDirectoryPaths : IDraftValuesMarker
	{
		private static readonly Lazy<string> zFilesDirectoryPath = new(() => Instances.PathOperator.GetDirectoryPath(
			Instances.ExecutablePathOperator.GetExecutableDirectoryPath(),
			"Files", "R5T.S0043"));

		public string FilesDirectoryPath => IDirectoryPaths.zFilesDirectoryPath.Value;
		public string GitHubRepositoriesDirectory => @"C:\Code\DEV\Git\GitHub";
		public string SafetyConeRepositoriesDirectory => @"C:\Code\DEV\Git\GitHub\SafetyCone";
	}
}