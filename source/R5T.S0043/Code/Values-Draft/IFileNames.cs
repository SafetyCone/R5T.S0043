using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface IFileNames : IDraftValuesMarker
	{
		public string Documentation => "Documentation.cs";
		public string GitIgnore => ".gitignore";
		public string Instances => "Instances.cs";
		public string Program => "Program.cs";
		public string ProjectPlan => "Project Plan.txt";
	}
}