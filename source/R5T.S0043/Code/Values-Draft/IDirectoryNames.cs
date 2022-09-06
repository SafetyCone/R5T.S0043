using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface IDirectoryNames : IDraftValuesMarker
	{
		public string Code => "Code";
		public string RepositorySourceDirectoryName => "source";
	}
}