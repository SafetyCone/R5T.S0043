using System;

using R5T.T0131;


namespace R5T.S0043
{
	/// <summary>
	/// SPDX (Software Package Data Exchange) license identifiers <see href="https://spdx.org/licenses/"/>.
	/// </summary>
	[DraftValuesMarker]
	public partial interface ILicenseIdentifiers : IDraftValuesMarker
	{
		public string MIT => "MIT";
	}
}