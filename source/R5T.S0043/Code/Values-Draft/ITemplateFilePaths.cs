using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface ITemplateFilePaths : IDraftValuesMarker
	{
		private static readonly Lazy<string> zGitIgnoreTemplateFile = new(() => Instances.PathOperator.GetFilePath(
			Instances.DirectoryPaths.FilesDirectoryPath,
			"gitignore-template.txt"));

		public string GitIgnoreTemplateFile => ITemplateFilePaths.zGitIgnoreTemplateFile.Value;
	}
}