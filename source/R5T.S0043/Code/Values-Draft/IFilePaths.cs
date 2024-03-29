using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface IFilePaths : IDraftValuesMarker,
		Z0015.IFilePaths
	{
		public string GitHubAuthorFile_Json => @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Data\Secrets\GitHub-Author-David.json";
		public string GitHubAuthenticationFile_Json => @"C:\Users\David\Dropbox\Organizations\Rivet\Shared\Data\Secrets\Authentication-GitHub-Aalborg.json";
	}
}