using System;

using R5T.T0131;


namespace R5T.S0043
{
    [Obsolete("See R5T.Z0036.ICommitMessages.")]
    [DraftValuesMarker]
	public partial interface ICommitMessages : IDraftValuesMarker,
		F0042.ICommitMessages
	{
        [Obsolete("See R5T.Z0036.ICommitMessages.ChangeProjectToNet6.")]
        public string ChangeProjectToNet6 => "Changed project to .NET 6.";
        [Obsolete("See R5T.Z0036.ICommitMessages.ChangeProjectToNetStandard2_0.")]
        public string ChangeProjectToNetStandard2_0 => "Changed project to .NET Standard 2.0.";
        [Obsolete("See R5T.Z0036.ICommitMessages.ChangeProjectToNetStandard2_1.")]
        public string ChangeProjectToNetStandard2_1 => "Changed project to .NET Standard 2.1.";
        [Obsolete("See R5T.Z0036.ICommitMessages.UpgradeSolutionFileToVS2022.")]
        public string UpgradeSolutionFileToVS2022 => "Upgrade solution file to VS 2022";
	}
}