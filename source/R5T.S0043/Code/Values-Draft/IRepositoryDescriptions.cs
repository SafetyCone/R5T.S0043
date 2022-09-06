using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface IRepositoryDescriptions : IDraftValuesMarker
	{
		public string ForTestRepository => "A test repository.";
	}
}