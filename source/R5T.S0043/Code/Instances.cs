using System;

using R5T.F0000;
using R5T.F0002;
using R5T.F0020;
using R5T.F0024;
using R5T.F0032;
using R5T.F0042;
using R5T.F0043;
using R5T.F0044;
using R5T.F0046;
using R5T.F0047;
using R5T.F0048;
using R5T.F0052;
using R5T.F0057;
using R5T.F0060;
using R5T.F0061;
using R5T.T0146;
using R5T.Z0000;
using R5T.Z0013;
using R5T.Z0014;


namespace R5T.S0043
{
    public static class Instances
    {
        public static ICharacters Characters { get; } = Z0000.Characters.Instance;
        public static ICommitMessages CommitMessages { get; } = S0043.CommitMessages.Instance;
        public static F0053.ICodeFileGenerator CodeFileGenerator { get; } = F0053.CodeFileGenerator.Instance;
        public static IRepositoryDirectoryNameOperator RepositoryDirectoryNameOperator { get; } = F0057.RepositoryDirectoryNameOperator.Instance;
        public static IDirectoryNames DirectoryNames { get; } = S0043.DirectoryNames.Instance;
        public static IRepositoryDirectoryPathOperator RepositoryDirectoryPathOperator { get; } = F0057.RepositoryDirectoryPathOperator.Instance;
        public static F0042.IDirectoryPaths DirectoryPaths { get; } = F0042.DirectoryPaths.Instance;
        public static IEnumerationHelper EnumerationHelper { get; } = F0002.EnumerationHelper.Instance;
        public static F0002.IExecutablePathOperator ExecutablePathOperator { get; } = F0002.ExecutablePathOperator.Instance;
        public static IFileExtensionOperator FileExtensionOperator { get; } = F0000.FileExtensionOperator.Instance;
        public static IFileExtensions FileExtensions { get; } = S0043.FileExtensions.Instance;
        public static IFileNames FileNames { get; } = S0043.FileNames.Instance;
        public static IFileNameOperator FileNameOperator { get; } = S0043.FileNameOperator.Instance;
        public static F0000.IFileNameOperator FileNameOperator_Base { get; } = F0000.FileNameOperator.Instance;
        public static IFilePathOperator FilePathOperator { get; } = F0002.FilePathOperator.Instance;
        public static IFilePaths FilePaths { get; } = S0043.FilePaths.Instance;
        public static IFileSystemOperator FileSystemOperator { get; } = S0043.FileSystemOperator.Instance;
        public static IGitHubLicenseIdentifiers GitHubLicenseIdentifiers { get; } = S0043.GitHubLicenseIdentifiers.Instance;
        public static IGitHubOperator GitHubOperator { get; } = S0043.GitHubOperator.Instance;
        public static F0041.IGitHubOperator GitHubOperator_Base { get; } = F0041.GitHubOperator.Instance;
        public static IGitHubOwners GitHubOwners { get; } = F0047.GitHubOwners.Instance;
        public static IGitOperator GitOperator { get; } = S0043.GitOperator.Instance;
        public static IJsonOperator JsonOperator { get; } = F0032.JsonOperator.Instance;
        public static ILibraryDescriptionOperator LibraryDescriptionOperator { get; } = F0043.LibraryDescriptionOperator.Instance;
        public static ILibraryNameOperator LibraryNameOperator { get; } = F0043.LibraryNameOperator.Instance;
        public static ILicenseIdentifiers LicenseIdentifiers { get; } = S0043.LicenseIdentifiers.Instance;
        public static F0060.ILocalRepositoryOperator LocalRepositoryOperator { get; } = F0060.LocalRepositoryOperator.Instance;
        public static INameOperator NameOperator { get; } = F0044.NameOperator.Instance;
        public static F0061.IOperations Operations { get; } = F0061.Operations.Instance;
        public static IOrganizationNamespaceTokens OrganizationNamespaceTokens { get; } = Z0013.OrganizationNamespaceTokens.Instance;
        public static F0002.IPathOperator PathOperator { get; } = F0002.PathOperator.Instance;
        public static IProjectDirectoryNameOperator ProjectDirectoryNameOperator { get; } = F0052.ProjectDirectoryNameOperator.Instance;
        public static IProjectFileGenerator ProjectFileGenerator { get; } = S0043.ProjectFileGenerator.Instance;
        public static IProjectFileOperator ProjectFileOperator { get; } = S0043.ProjectFileOperator.Instance;
        public static IProjectNameOperator ProjectNameOperator { get; } = S0043.ProjectNameOperator.Instance;
        public static F0040.IProjectNamespacesOperator ProjectNamespacesOperator { get; } = F0040.ProjectNamespacesOperator.Instance;
        public static IProjectOperations ProjectOperations { get; } = S0043.ProjectOperations.Instance;
        public static F0051.IProjectOperator ProjectOperator { get; } = F0051.ProjectOperator.Instance;
        public static IProjectPathsOperator ProjectPathsOperator { get; } = S0043.ProjectPathsOperator.Instance;
        public static IProjectFileTypeStrings ProjectFileTypeStrings { get; } = S0043.ProjectFileTypeStrings.Instance;
        public static IReasonOperator ReasonOperator { get; } = T0146.ReasonOperator.Instance;
        public static IRemoteRepositoryOperator RemoteRepositoryOperator { get; } = S0043.RemoteRepositoryOperator.Instance;
        public static IRepositoryDescriptionOperator RepositoryDescriptionOperator { get; } = F0046.RepositoryDescriptionOperator.Instance;
        public static IRepositoryDescriptions RepositoryDescriptions { get; } = Z0014.RepositoryDescriptions.Instance;
        public static F0046.IRepositoryNameOperator RepositoryNameOperator { get; } = F0046.RepositoryNameOperator.Instance;
        public static IRepositoryNames RepositoryNames { get; } = Z0014.RepositoryNames.Instance;
        public static IRepositoryOperations RepositoryOperations { get; } = S0043.RepositoryGeneration.Instance;
        public static F0060.IRepositoryOperator RepositoryOperator { get; } = F0060.RepositoryOperator.Instance;
        public static F0042.IRepositoryPathsOperator RepositoryPathsOperator { get; } = F0042.RepositoryPathsOperator.Instance;
        public static IRepositoryTestOperations RepositoryTestOperations { get; } = S0043.RepositoryTestOperations.Instance;
        public static IResultOperator ResultOperator { get; } = T0146.ResultOperator.Instance;
        public static IServicesOperator ServicesOperator { get; } = S0043.ServicesOperator.Instance;
        public static ISolutionFileGenerator SolutionFileGenerator { get; } = F0024.SolutionFileGenerator.Instance;
        public static ISolutionFileOperator SolutionFileOperator { get; } = S0043.SolutionFileOperator.Instance;
        public static ISolutionFolderName SolutionFolderName { get; } = S0043.SolutionFolderName.Instance;
        public static ISolutionNameOperator SolutionNameOperator { get; } = F0048.SolutionNameOperator.Instance;
        public static ISolutionOperations SolutionOperations { get; } = S0043.SolutionOperations.Instance;
        public static ISolutionOperator SolutionOperator { get; } = S0043.SolutionOperator.Instance;
        public static ISolutionPathsOperator SolutionPathsOperator { get; } = S0043.SolutionPathsOperator.Instance;
        public static ISolutionTestOperations SolutionTestOperations { get; } = S0043.SolutionTestOperations.Instance;
        public static IStreamReaderOperator StreamReaderOperator { get; } = F0000.StreamReaderOperator.Instance;
        public static IStrings Strings { get; } = S0043.Strings.Instance;
        public static ITextFileGenerator TextFileGenerator { get; } = S0043.TextFileGenerator.Instance;
        public static ITemplateFilePaths TemplateFilePaths { get; } = F0042.TemplateFilePaths.Instance;
    }
}
