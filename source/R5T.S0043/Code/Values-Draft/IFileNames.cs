using System;

using R5T.T0131;


namespace R5T.S0043
{
	[DraftValuesMarker]
	public partial interface IFileNames : IDraftValuesMarker
	{
		public string Documentation => "Documentation.cs";
		public string Instances => "Instances.cs";
		public string Program => "Program.cs";
		public string ProjectPlanTextFile => "Project Plan.txt";
		public string ProjectPlanMarkdownFile => "Project Plan.md";
	}
}