using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ISolutionNameOperator : IFunctionalityMarker
	{
		public string AdjustSolutionName_ForPrivacy(string unadjustedSolutionName, bool isPrivate)
        {
			var output = Instances.NameOperator.AdjustNameForPrivacy(unadjustedSolutionName, isPrivate);
			return output;
        }

		public string GetUnadjustedSolutionName_FromUnadjustedLibraryName(string unadjustedLibraryName)
        {
			// The unadjusted solution name is just the unadjusted library name.
			var unadjustedSolutionName = unadjustedLibraryName;
			return unadjustedSolutionName;
        }
	}
}