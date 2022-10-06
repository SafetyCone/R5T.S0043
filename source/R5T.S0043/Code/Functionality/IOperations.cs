using System;

using Microsoft.Extensions.Logging;

using R5T.T0132;
using R5T.T0146;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IOperations : IFunctionalityMarker
	{
		public void WriteResultAndOpenInNotepadPlusPlus(
			Result result,
			string resultFilePath,
			ILogger logger)
        {
			logger.LogInformation($"Writing result output...\n\t{resultFilePath}");

			T0146.Instances.ResultSerializer.Serialize(resultFilePath, result);

			logger.LogInformation($"Wrote result output.\n\t{resultFilePath}");

			logger.LogInformation($"Opening result output in Notepad++...\n\t{resultFilePath}");

			F0033.Instances.NotepadPlusPlusOperator.Open(resultFilePath);

			logger.LogInformation($"Opened result output in Notepad++:\n\t{resultFilePath}");
		}
	}
}