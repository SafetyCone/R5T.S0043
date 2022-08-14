using System;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface INameOperator : IFunctionalityMarker
	{
		public string AdjustNameForPrivacy(string name, bool isPrivate)
		{
			var output = isPrivate
				? name + ".Private"
				: name
				;

			return output;
		}
	}
}