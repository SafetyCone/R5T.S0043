using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface IStrings : IDraftValuesMarker
	{
		public string MitLicenseFirstLine => "MIT License";
		public string NoLicenseFoundExpression => "XXX No License File Found XXX";
	}
}