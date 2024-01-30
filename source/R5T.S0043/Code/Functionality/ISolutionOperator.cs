using System;

using Microsoft.Extensions.Logging;

using R5T.F0024.F001;
using R5T.F0024.T001;
using R5T.T0132;
using R5T.T0146;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface ISolutionOperator : IFunctionalityMarker,
		F0049.ISolutionOperator
	{
        public Result<string> CreateNew_SolutionFile(
			VisualStudioVersion visualStudioVersion,
			string solutionDirectoryPath,
            string solutionName,
            ILogger logger)
        {
            var solutionFilePath = Instances.SolutionPathsOperator.GetSolutionFilePath(
                solutionDirectoryPath,
                solutionName);

			var createNewSolutionFileResult = this.CreateNew_SolutionFile(
				visualStudioVersion,
				solutionFilePath,
				logger);

			var result = Instances.ResultOperator.Result(createNewSolutionFileResult, solutionFilePath);
			return result;
        }

		public Result CreateNew_SolutionFile(
			VisualStudioVersion visualStudioVersion,
			string solutionFilePath,
			ILogger logger)
		{
			var result = Instances.ResultOperator.Result()
				.WithTitle("Create New Solution File")
				.WithMetadata("Solution file path", solutionFilePath)
				.WithMetadata("Visual Studio version", F0000.Instances.EnumerationOperator.GetStringRepresentation(visualStudioVersion))
				;

			logger.LogInformation($"Creating solution file...{Environment.NewLine}\t{solutionFilePath}");

			// Run safety checks, does a file already exist at the path destination?
			logger.LogInformation("Running safety checks...");

			Result<bool> RunSafetyChecks()
            {
				var result = Instances.ResultOperator.Result<bool>()
					.WithTitle("Running Safety Checks")
					.WithMetadata("Solution file path", solutionFilePath)
					;

				logger.LogInformation($"Checking if solution file already exists...{Environment.NewLine}\t{solutionFilePath}");

				var solutionFileExistsResult = Instances.FileSystemOperator.FileExists_Result(solutionFilePath);

				result.WithChild(solutionFileExistsResult);

				var solutionFileAlreadyExists = solutionFileExistsResult.Value;
				if (solutionFileAlreadyExists)
				{
					logger.LogError($"Solution file already exists.{Environment.NewLine}\t{solutionFilePath}");

					result.WithFailure("Solution file already exists.");
				}
				else
				{
					logger.LogInformation($"Solution file does not already exist.{Environment.NewLine}\t{solutionFilePath}");

					result.WithSuccess("Solution file does not already exist.");
				}

				var safetyChecksPassed = !solutionFileAlreadyExists;

				result.WithValue(safetyChecksPassed);

				return result;
			}

			var safetyChecksResult = RunSafetyChecks();

			result.WithChild(safetyChecksResult);

			var safetyChecksPassed = safetyChecksResult.Value;
			if(safetyChecksPassed)
            {
				logger.LogInformation("Safety checks passed.");

				result.WithSuccess("Safety checks passed.");
			}
			else
            {
				logger.LogError("Safety checks failed.");

				result.WithFailure("Safety checks failed.", safetyChecksResult.Failures);

				// Stop.
				return result;
			}

			// Create new instance.
			logger.LogInformation("Generating solution file...");

			Result CreateNewSolutionFile()
            {
				var result = Instances.ResultOperator.Result()
					.WithTitle("Generate New Solution File")
					.WithMetadata("Visual Studio Version", F0000.Instances.EnumerationOperator.GetStringRepresentation(visualStudioVersion))
					;

				// Generate instance.
				SolutionFile solutionFile;
				try
				{
					logger.LogInformation("Generating solution file instance...");

					solutionFile = Instances.SolutionFileGenerator.CreateNew(visualStudioVersion);

					logger.LogInformation("Generated solution file instance.");

					result.WithSuccess("Generated solution file instance.");
				}
				catch (Exception exception)
                {
					logger.LogError(exception, "Failed to generate solution file instance.");

					result.WithFailure("Failed to generate solution file instance.");

					// Stop.
					return result;
                }

				// Serialize to file path.
				try
				{
					logger.LogInformation($"Writing solution file...{Environment.NewLine}\t{solutionFilePath}");

					F0024.Instances.SolutionFileSerializer.Serialize(
						solutionFilePath,
						solutionFile);

					logger.LogInformation($"Wrote solution file.{Environment.NewLine}\t{solutionFilePath}");

					result.WithSuccess("Wrote solution file.");
				}
				catch (Exception exception)
				{
					logger.LogError(exception, "Failed to write solution file.");

					result.WithFailure("Failed to write solution file.");
				}

				return result;
			}

			var createNewSolutionFileResult = CreateNewSolutionFile();

			result.WithChild(createNewSolutionFileResult);

			if(createNewSolutionFileResult.IsSuccess())
            {
				logger.LogInformation($"Generated solution file.{Environment.NewLine}\t{solutionFilePath}");

				result.WithSuccess("Generated solution file.");
			}
            else
            {
				logger.LogError($"Failed to generate solution file.{Environment.NewLine}\t{solutionFilePath}");

				result.WithFailure("Failed to generate solution file.", createNewSolutionFileResult.Failures);
			}

			return result;
		}
	}
}