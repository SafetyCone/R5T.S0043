using System;
using System.Linq;

using Microsoft.Extensions.Logging;

using R5T.T0132;


namespace R5T.S0043
{
	[FunctionalityMarker]
	public partial interface IProjectOperations : IFunctionalityMarker
	{
		/// <summary>
		/// Adds package properties using their default values.
		/// </summary>
		public void AddPackageProperties()
		{
			var projectFilePaths = new[]
			{
				@"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0064\source\R5T.T0064\R5T.T0064.csproj",
			};

            foreach (var projectFilePath in projectFilePaths)
            {
				this.AddPackageProperties(projectFilePath);	
            }
		}

		public void AddPackageProperties(string projectFilePath)
		{
			/// Inputs.
			var descriptionOverride =
				String.Empty
				//""
				;
			var licenseOverride =
				String.Empty
				//""
				;
            var packageTags =
				Array.Empty<string>()
				//new string[]
				//{

				//}
				;

			var version = F0020.ProjectOperator.Instance.GetDefaultVersion();
			var authors = new[]
			{
				"DCoats"
			};
			var company = "Rivet";
			var copyrightHolder = company;
			var copyrightText = $"Copyright (c) {copyrightHolder} {F0000.Instances.NowOperator.GetNowLocal().Year}";
			//var packageReadmeFileRelativePath = "Project Plan.md";
			var requireLicenseAcceptance = true;

			/// Run.
			// Start by creating a backup project file.
			Instances.FileSystemOperator.CreateBackupFile(projectFilePath);

			// Now modify the project file.
			//var description = ; // Get from project plan.
			//var repositoryUrl = ;// Get from repository.

			Instances.ProjectFileOperator.InProjectFileContext(
				projectFilePath,
				projectElement =>
				{
					var projectXmlOperator = F0020.Instances.ProjectXmlOperator;

					// Version: Set the version first, so that other elements can find the right element.
					projectXmlOperator.SetVersion(projectElement, version);

					// Authors.
					projectXmlOperator.SetAuthors(projectElement, authors);

					// Company.
					projectXmlOperator.SetCompany(projectElement, company);

					// Copyright.
					projectXmlOperator.SetCopyright(projectElement, copyrightText);

					// Description.
					if(F0000.Instances.StringOperator.HasValue(descriptionOverride))
                    {
						// Use the description override.
						projectXmlOperator.SetDescription(projectElement, descriptionOverride);
                    }
					else
                    {
                        //// Get the description from the project plan file.
                        //var projectPlanFilePath = Instances.ProjectPathsOperator.GetProjectPlanMarkdownFilePath(projectFilePath);
                        var description = "--- ADD DESCRIPTION ---";
                        projectXmlOperator.SetDescription(projectElement, description);
                    }

					// License
					if(F0000.Instances.StringOperator.HasValue(licenseOverride))
                    {
						projectXmlOperator.SetPackageLicenseExpression(projectElement, licenseOverride);
                    }
					else
                    {
						var repositoryDirectoryPath = F0019.GitOperator.Instance.GetRepositoryDirectoryPath(projectFilePath);
                        var licenseFilePath = Instances.RepositoryPathsOperator.GetLicenseFilePath(repositoryDirectoryPath);
						var licenseTextLines = F0000.Instances.FileSystemOperator.ReadText_Lines(licenseFilePath);
						var licenseExpression = Instances.Strings.NoLicenseFoundExpression;
						var firstLicenseTextLine = licenseTextLines.First();
						if(firstLicenseTextLine == Instances.Strings.MitLicenseFirstLine)
                        {
                            projectXmlOperator.SetPackageLicenseExpression(projectElement, Instances.LicenseIdentifiers.MIT);
                        }
                    }

					//// Readme.
					//projectXmlOperator.SetPackageReadmeFile(projectElement, packageReadmeFileRelativePath);

					// Require license acceptance.
					projectXmlOperator.SetPackageRequireLicenseAcceptance(projectElement, requireLicenseAcceptance);

					// Repository URL.
					var repositoryUrl = Instances.GitOperator.GetRepositoryRemoteUrl(projectFilePath);
					projectXmlOperator.SetRepositoryUrl(projectElement, repositoryUrl);

					// Package tags.
					// Only set package tags if there are any.
					if(F0000.EnumerableOperator.Instance.HasAny(packageTags))
                    {
						projectXmlOperator.SetPackageTags(projectElement, packageTags);
                    }
				});
        }

		public void CreateProjectFileOnly()
        {
			/// Inputs.
			var solutionFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0000\source\R5T.F0000.Q000.sln";
			var projectName = "Test";
			var projectType = Instances.ProjectFileTypeStrings.Console;
			var targetFramework = F0020.Instances.TargetFrameworkMonikerStrings.NET_5;


			/// Run.
			Instances.ProjectFileGenerator.Create_UsingSolutionFilePath(
				solutionFilePath,
				projectName,
				targetFramework,
				projectType);
		}

		public void ChangeToNetStandard2_0()
        {
			/// Inputs.
			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.NG0004\source\R5T.NG0004\R5T.NG0004.csproj";
			var pushChangesToRemote = true;


			/// Run.
			using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<ISolutionOperations>>();

			// Save a copy.
			var backupProjectFilePath = Instances.FilePathOperator.GetBackupCopyFilePath(projectFilePath);

			F0000.Instances.FileSystemOperator.CopyFile(
				projectFilePath,
				backupProjectFilePath);

			// Now modify the project file.
			F0020.Instances.ProjectFileOperator.InProjectFileContext(
				projectFilePath,
				projectElement =>
				{
					F0020.Instances.ProjectXmlOperator.SetTargetFramework(projectElement,
						F0020.Instances.TargetFrameworkMonikerStrings.NET_Standard_2_0);
				});

			// Push change to remote.
			var projectIsInRepository = Instances.GitOperator.HasRepository(projectFilePath);
			if (pushChangesToRemote && projectIsInRepository)
			{
				var repositoryDirectoryPath = projectIsInRepository.Result;

				Instances.GitHubOperator.PushAllChanges_NoResult(
					repositoryDirectoryPath,
					Instances.CommitMessages.ChangeProjectToNetStandard2_0,
					logger);
			}
		}

		public void ChangeToNetStandard2_1()
		{
			/// Inputs.
			var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.F0039\source\R5T.F0039\R5T.F0039.csproj";
			var pushChangesToRemote = true;


			/// Run.
			using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<ISolutionOperations>>();

			// Save a copy.
			var backupProjectFilePath = Instances.FilePathOperator.GetBackupCopyFilePath(projectFilePath);

			F0000.Instances.FileSystemOperator.CopyFile(
				projectFilePath,
				backupProjectFilePath);

			// Now modify the project file.
			F0020.Instances.ProjectFileOperator.InProjectFileContext(
				projectFilePath,
				projectElement =>
				{
					F0020.Instances.ProjectXmlOperator.SetTargetFramework(projectElement,
						F0020.Instances.TargetFrameworkMonikerStrings.NET_Standard2_1);
				});

			// Push change to remote.
			var projectIsInRepository = Instances.GitOperator.HasRepository(projectFilePath);
			if (pushChangesToRemote && projectIsInRepository)
			{
				var repositoryDirectoryPath = projectIsInRepository.Result;

				Instances.GitHubOperator.PushAllChanges_NoResult(
					repositoryDirectoryPath,
					Instances.CommitMessages.ChangeProjectToNetStandard2_1,
					logger);
			}
		}

		public void ChangeToNet6_0()
		{
			/// Inputs.
			var projectFilePath = @"C:\Code\DEV\Git\GitHub\davidcoats\D8S.C0002.Private\source\D8S.C0002\D8S.C0002.csproj";
			var pushChangesToRemote = true;


			/// Run.
			using var services = Instances.ServicesOperator.GetServicesContext();

			var logger = services.GetService<ILogger<ISolutionOperations>>();

			// Save a copy.
			var backupProjectFilePath = Instances.FilePathOperator.GetBackupCopyFilePath(projectFilePath);

			F0000.Instances.FileSystemOperator.CopyFile(
				projectFilePath,
				backupProjectFilePath);

			// Now modify the project file.
			F0020.Instances.ProjectFileOperator.InProjectFileContext(
				projectFilePath,
				projectElement =>
				{
					F0020.Instances.ProjectXmlOperator.SetTargetFramework(projectElement,
						F0020.Instances.TargetFrameworkMonikerStrings.NET_6);
				});

			// Push change to remote.
			var projectIsInRepository = Instances.GitOperator.HasRepository(projectFilePath);
			if (pushChangesToRemote && projectIsInRepository)
			{
				var repositoryDirectoryPath = projectIsInRepository.Result;

				Instances.GitHubOperator.PushAllChanges_NoResult(
					repositoryDirectoryPath,
					Instances.CommitMessages.ChangeProjectToNet6,
					logger);
			}
		}
	}
}