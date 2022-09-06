using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface IFileExtensions : IDraftValuesMarker
	{
		public string CSharpProjectFile => "csproj";
		public string SolutionFile => "sln";
	}
}