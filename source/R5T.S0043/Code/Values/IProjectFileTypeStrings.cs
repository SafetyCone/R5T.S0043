using System;

using R5T.T0131;


namespace R5T.S0043
{
	[ValuesMarker]
	public partial interface IProjectFileTypeStrings : IValuesMarker
	{
		public string Console => F0020.Instances.OutputTypeStrings.Exe;
		public string Library => F0020.Instances.OutputTypeStrings.Library;
	}
}