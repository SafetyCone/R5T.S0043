using System;

using R5T.T0132;
using R5T.T0146;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface IFileSystemOperator : IDraftFunctionalityMarker,
		F0002.IFileSystemOperator
	{
		public Result<bool> FileExists_Result(string filePath)
		{
			var fileExists = F0000.Instances.FileSystemOperator.FileExists(filePath);

			var successMessage = fileExists
				? $"File exists: {filePath}"
				: $"File does not exist: {filePath}"
				;

			var result = T0146.Instances.ResultOperator.SuccessWithMessage(fileExists, successMessage)
				.WithTitle("Check File Exists")
				;

			return result;
		}
	}
}