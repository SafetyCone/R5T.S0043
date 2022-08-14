using System;
using System.IO;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ICodeFileGenerator : IFunctionalityMarker
	{
		public void CreateDocumentationFile(
			 string filePath,
			 string namespaceName,
			 string description)
        {
			var text =
$@"
using System;


namespace {namespaceName}
{{
	/// <summary>
	/// {description}
	/// </summary>
	public static class Documentation
	{{
	}}
}}
";

			Instances.TextFileGenerator.WriteText(
				filePath,
				text);
		}

		public void CreateInstancesFile(
			string filePath,
			string namespaceName)
		{
			var text =
$@"
using System;


namespace {namespaceName}
{{
    public static class Instances
    {{
		
    }}
}}
";

			Instances.TextFileGenerator.WriteText(
				filePath,
				text);
		}

		public void CreateProgramFile(
			string filePath,
			string namespaceName)
		{
			var text =
$@"
using System;


namespace {namespaceName}
{{
    class Program
    {{
        static void Main()
        {{
            Console.WriteLine(""Hello World!"");
        }}
    }}
}}
";
			Instances.TextFileGenerator.WriteText(
				filePath,
				text);
		}
	}
}