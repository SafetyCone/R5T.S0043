using System;
using System.IO;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ITextFileGenerator : IFunctionalityMarker
	{
		public void CreateProjectPlanTextFile(
			string filePath,
			string projectName,
			string projectDescription)
        {
			var text =
$@"
{projectName} - {projectDescription}
";

			this.WriteText(
				filePath,
				text);
        }

		public void CreateProjectPlanMarkdownFile(
			string filePath,
			string projectName,
			string projectDescription)
		{
			var text =
$@"
# {projectName}
{projectDescription}
";

			this.WriteText(
				filePath,
				text);
		}

		public void WriteText(
			string filePath,
			string text,
			bool trim = true)
		{
			var outputText = trim
				? text.Trim()
				: text
				;

			File.WriteAllText(
				filePath,
				outputText);
		}
	}
}