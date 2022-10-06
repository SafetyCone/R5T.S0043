using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface ICommitMessages : IDraftValuesMarker,
		F0042.ICommitMessages
	{
		public string ChangeProjectToNet6 => "Changed project to .NET 6.";
		public string ChangeProjectToNetStandard2_0 => "Changed project to .NET Standard 2.0.";
		public string ChangeProjectToNetStandard2_1 => "Changed project to .NET Standard 2.1.";
		public string UpgradeSolutionFileToVS2022 => "Upgrade solution file to VS 2022";
	}
}