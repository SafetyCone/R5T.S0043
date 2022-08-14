using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface ISolutionFolderName : IDraftValuesMarker
	{
		public string Dependencies => "_Dependencies";
	}
}